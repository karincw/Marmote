using UnityEngine;
using DG.Tweening;
using Unity.Cinemachine;
using System.Collections;
using UnityEngine.UI;
using Shy.Unit;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

namespace Shy
{
    public class SkillMotionManager : MonoBehaviour
    {
        public static SkillMotionManager Instance;

        [SerializeField] private CinemachineCamera mainCam;
        [SerializeField] private Image battleView, pet;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);

            battleView.color = new Color(0, 0, 0, 0);
            battleView.gameObject.SetActive(false);
            pet.color = new Color(0,0,0,0);
        }

        private void SkillFinish(List<Character> _ch, Character _user)
        {
            _user.SkillFin();
            for (int i = 0; i < _ch.Count; i++) _ch[i].SkillFin();

            StartCoroutine(BattleManager.Instance.NextAction());
        }

        public void UseSkill(Character _user, SkillMotion _motion, List<SkillEvent> _events)
        {
            #region Set
            Team userTeam = _user.team;
            Team targetTeam = (userTeam == Team.Player) ? Team.Enemy : Team.Player;

            bool isPet = Bool.IsPetMotion(_motion), isTeam = Bool.IsTeamMotion(_motion);
            bool isSummon = Bool.IsSummonMotion(_motion);

            float time = 0.3f;

            battleView.gameObject.SetActive(true);
            Sequence seq = DOTween.Sequence();

            HashSet<Character> targetHash = new HashSet<Character>();
            foreach (var _event in _events)
            {
                foreach (Character _ch in _event.GetTargets()) targetHash.Add(_ch);
            }

            List<Character> targets = targetHash.ToList();
            #endregion

            #region Skill Begin Event
            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].transform.SetParent(battleView.transform);
            }
            _user.transform.SetParent(battleView.transform);

            seq.Append(battleView.DOFade(0.7f, 0.2f).OnComplete(()=>BattleManager.Instance.HealthUiVisible(false)));

            // Cam
            if(_motion != SkillMotion.All)
            {
                seq.Join(CamMove(isTeam ? userTeam : targetTeam, time).OnStart(() => StartCoroutine(CameraZoom(50, time))));
                if (!isTeam) seq.Join(CamRotate(0.8f, targetTeam, time));
            }
            
            //CharacterMove
            if (!isPet && !isTeam && !isSummon)
            {
                seq.Insert(0.1f, CharacterMove(_user.GetVisual(), time - 0.1f));
            }
            else if (isPet || isSummon)
            {
                pet.transform.SetParent(_user.transform);
                pet.transform.SetAsFirstSibling();
                pet.transform.rotation = Quaternion.Euler(0, targetTeam == Team.Enemy ? 180 : 0, 0);
                //pet.sprite = _skill.summon;
                
                seq.Prepend(DOTween.To(() => 0f, x => { }, 1f, 0.1f).OnStart(() => _user.visualAction?.Invoke()));
                pet.color = Color.white;
                pet.transform.localPosition = Vector3.zero;

                if (isPet)
                {
                    seq.Insert(0.3f, CharacterMove(pet.transform, time - 0.1f));
                }
                else if(isSummon)
                {
                    pet.transform.position = new Vector3(0, 10, pet.transform.position.z);
                    seq.Insert(0.2f, CharacterDrop(pet.transform, time));
                }
            }
            
            seq.Append(DOTween.To(() => 0f, x => { }, 0.3f, 0.03f).OnComplete(()=> 
            {
                if (isPet || isSummon)
                {
                    //pet.sprite = _skill.summonAnime;

                    if (!isTeam && _motion != SkillMotion.SummonAndLong) seq.Join(ShortDash(pet.transform, 0.1f, userTeam));
                }

                foreach (var _event in _events) _event.UseEvent();
            }));
            #endregion

            #region Skill Fin Event
            time = 0.2f;

            seq.AppendInterval(0.5f);
            seq.Append(CamRotate(0, Team.None, time));
            seq.Join(CamMove(Team.None, time).OnStart(()=>StartCoroutine(CameraZoom(60, 0.2f))));
            seq.Join(battleView.DOFade(0, 0.15f).OnComplete(()=>
            {
                battleView.gameObject.SetActive(false);
                BattleManager.Instance.HealthUiVisible(true);
            }));

            if(!isPet && !isSummon)
            {
                seq.Join(CharacterReturn(_user.GetVisual(), time + 0.15f).OnStart(() => SkillFinish(targets, _user)));
            }
            else
            {
                seq.Join(CharacterReturn(pet.transform, time + 0.15f).OnStart(() => SkillFinish(targets, _user)));
                seq.Join(pet.DOFade(0, time).OnComplete(()=>pet.transform.SetParent(null)));
            }
            #endregion
        }

        private Tween CharacterDrop(Transform _target, float _t) => _target.DOMoveY(0.5f, _t);

        private Tween CharacterMove(Transform _target, float _t) => _target.DOMove(new Vector3(0, 0.5f, _target.position.z), _t);

        private Tween ShortDash(Transform _target, float _t, Team _team) => _target.DOLocalMoveX(_target.localPosition.x + (_team == Team.Player ? 100 : -100), _t);

        private Tween CharacterReturn(Transform _target, float _t) => _target.DOLocalMove(Vector2.zero, _t);

        #region Camera
        private IEnumerator CameraZoom(float _lastValue, float _t)
        {
            float zoomValue = (_lastValue - mainCam.Lens.FieldOfView) * 0.05f, time = _t * 0.05f;
            
            for (int i = 0; i < 20; i++)
            {
                yield return new WaitForSeconds(time);
                mainCam.Lens.FieldOfView += zoomValue;
            }
        }

        private Tween CamRotate(float _value, Team _target, float _t)
        {
            if (_target == Team.Player) _value = -_value;
            Tween t = mainCam.transform.DOLocalRotate(new Vector3( 0, 0, _value), 0.75f);
            return t;
        }

        private Tween CamMove(Team _target, float _t)
        {
            Vector2 pos = Vector2.zero;
            if (_target == Team.Enemy) pos.x = 5f;
            else if (_target == Team.Player) pos.x = -5f;
            
            if (_target != Team.None) pos.y = 0.5f;

            return mainCam.transform.DOMove(pos, 0.75f);
        }
        #endregion
    }
}
