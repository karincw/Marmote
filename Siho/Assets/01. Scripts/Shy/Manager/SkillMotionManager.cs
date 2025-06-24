using UnityEngine;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine.UI;
using Shy.Unit;
using System.Collections.Generic;
using System.Linq;
using Shy.DarkPanel;

namespace Shy.Anime
{
    public class SkillMotionManager : MonoBehaviour
    {
        public static SkillMotionManager Instance;

        [SerializeField] private Image pet;
        [SerializeField] private Transform petParent;

        public CameraMotion camMotion;
        private CharacterMotion chMotion;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);

            chMotion = new CharacterMotion();
            PetOff();
        }

        private void SkillFinish(Character _ch, Character _user)
        {
            _user.SkillFin();
            _ch.SkillFin();

            PetOff();

            BattleManager.Instance.HealthUiVisible(true);
            StartCoroutine(BattleManager.Instance.NextAction());
        }

        private void SkillFinish(List<Character> _ch, Character _user)
        {
            _user.SkillFin();
            for (int i = 0; i < _ch.Count; i++) _ch[i].SkillFin();

            PetOff();

            BattleManager.Instance.HealthUiVisible(true);
            StartCoroutine(BattleManager.Instance.NextAction());
        }

        private void PetOff()
        {
            pet.transform.SetParent(petParent);
            pet.color = new Color(1, 1, 1, 0);
        }

        public void UseSkill(AnimeData _animeData)
        {
            HashSet<Character> targetHash = new HashSet<Character>();

            foreach (var _event in _animeData.events)
            {
                foreach (Character _ch in _event.GetTargets())
                {
                    targetHash.Add(_ch);
                }
            }
            List<Character> _targets = targetHash.ToList();
            _targets.Remove(_animeData.user);
            int _targetCnt = _targets.Count;

            switch (_animeData.GetSkillMotion())
            {
                case SkillMotion.AttackNear:
                case SkillMotion.AttackLong:
                    if (_targetCnt >= 1) SingleAttackAnime(_animeData, _targets[0]);
                    else if (_targetCnt > 1) { }
                    else Debug.LogError("Error : no Target");
                    break;

                case SkillMotion.PetAttackNear:
                case SkillMotion.PetAttackLong:
                    if (_targetCnt >= 1) SingleAttackPetAnime(_animeData, _targets[0]);
                    else if (_targetCnt > 1) { }
                    else Debug.LogError("Error : no Target");
                    break;


                case SkillMotion.TeamBySelf:
                    break;
                case SkillMotion.TeamByPet:

                    break;
                case SkillMotion.EveryOne:
                    break;
            }
        }

        private void TeamEffectPet(AnimeData _animeData, List<Character> _targets)
        {
            Sequence _seq = DOTween.Sequence(), _subSeq, _camSeq = DOTween.Sequence();
            Team _team = _targets[0].team;
            var _user = _animeData.user;

            //_camSeq.AppendCallback();
        }

        private void SingleAttackPetAnime(AnimeData _animeData, Character _target)
        {
            Sequence _seq = DOTween.Sequence(), _subSeq, _camSeq = DOTween.Sequence();
            Team _team = _target.team;
            var _user = _animeData.user;
            bool _isLong = _animeData.GetSkillMotion() == SkillMotion.PetAttackLong;

            _camSeq.AppendCallback(() => camMotion.CamZoom(40, 0.45f));
            _camSeq.Append(camMotion.CamMove(_user.transform, _team, 0.45f));

            _camSeq.AppendCallback(() => camMotion.CamZoom(30, 1.1f));
            _camSeq.Append(camMotion.CamMove(_target.transform, _team,  _isLong, 1.1f));
            _camSeq.Join(camMotion.CamRotate(1.8f, _team, 1.1f).SetEase(Ease.OutCubic));

            _camSeq.Append(camMotion.CamReturn());
            _camSeq.Join(camMotion.CamRotate(0, _team, 0.5f));
            _camSeq.JoinCallback(() => camMotion.CamZoom());

            //Begin
            _seq.AppendInterval(0.25f);
            _seq.AppendCallback(() => 
            {
                BattleManager.Instance.HealthUiVisible(false);
                DarkManager.Instance.PanelOpen(_user, _target);
                _user.visualAction?.Invoke();
            });
            _seq.Append(chMotion.PetSpawn(_user.transform, pet, _animeData.GetMotion(AnimeType.SummonVisual), 0.25f));
            _seq.AppendInterval(0.1f);
            _seq.Append(chMotion.CharacterMove(_target.GetVisual(), pet.transform, _team, _isLong, 0.4f));
            _seq.AppendInterval(0.2f);

            _subSeq = DOTween.Sequence();
            _subSeq.AppendCallback(() =>
            {
                pet.sprite = _animeData.GetMotion(AnimeType.SummonAnime);

                foreach (var _event in _animeData.events) _event.UseEvent();
            });
            _subSeq.AppendInterval(0.07f);
            _subSeq.Append(chMotion.Knockback(_target.GetVisual(), _team));

            _seq.Append(_subSeq);
            _seq.Join(chMotion.ShortDash(pet.transform, _team));
            _seq.AppendInterval(0.2f);

            //End
            _seq.Append(chMotion.CharacterReturn(pet.transform));
            _seq.AppendInterval(0.1f);
            _seq.AppendCallback(() => DarkManager.Instance.PanelOff());
            _seq.Append(chMotion.CharacterReturn(_target.GetVisual(), false, 0));
            _seq.JoinCallback(() => SkillFinish(_target, _user));
        }

        private void SingleAttackAnime(AnimeData _animeData, Character _target)
        {
            Sequence _seq = DOTween.Sequence(), _subSeq, _camSeq = DOTween.Sequence();
            Team _team = _target.team;
            var _user = _animeData.user;
            bool _isLong = _animeData.GetSkillMotion() == SkillMotion.PetAttackLong;

            _camSeq.AppendCallback(() => camMotion.CamZoom(30, 0.85f));
            _camSeq.Append(camMotion.CamMove(_target.transform, _team, _isLong, 1.02f)); //0~1.02s
            _camSeq.Join(camMotion.CamRotate(1.8f, _team, 1.02f).SetEase(Ease.OutCubic));

            _camSeq.Append(camMotion.CamReturn()); //1.02~1.57s
            _camSeq.Join(camMotion.CamRotate(0, _team, 0.5f)); //1.02~1.52s
            _camSeq.JoinCallback(() => camMotion.CamZoom());

            //Begin
            _seq.AppendInterval(0.1f); //0~0.1s
            _seq.AppendCallback(() => BattleManager.Instance.HealthUiVisible(false));
            _seq.AppendCallback(() => DarkManager.Instance.PanelOpen(_user, _target));
            _seq.Append(chMotion.CharacterMove(_target.transform, _user.GetVisual(), _team, _isLong)); //0.1~0.32s
            _seq.AppendInterval(0.3f); //0.32~0.62s

            _subSeq = DOTween.Sequence();
            _subSeq.AppendCallback(() =>
            {
                _user.visualAction?.Invoke();
                foreach (var _event in _animeData.events) _event.UseEvent();
            });
            _subSeq.AppendInterval(0.07f);
            _subSeq.Append(chMotion.Knockback(_target.GetVisual(), _team));

            _seq.Append(_subSeq);
            _seq.Join(chMotion.ShortDash(_user.GetVisual(), _team));
            _seq.AppendInterval(0.2f); 

            //End
            _seq.Append(chMotion.CharacterReturn(_user.GetVisual()));
            _seq.AppendInterval(0.1f);
            _seq.AppendCallback(() => DarkManager.Instance.PanelOff());
            _seq.Append(chMotion.CharacterReturn(_target.GetVisual(), false, 0));
            _seq.JoinCallback(() => SkillFinish(_target, _user));
        }
    }
}

