using UnityEngine;
using DG.Tweening;
using Unity.Cinemachine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace Shy
{
    public class SkillManager : MonoBehaviour
    {
        public static SkillManager Instance;

        [SerializeField] private CinemachineCamera mainCam;
        [SerializeField] private Image pet;
        [SerializeField] private AttackMotion testMotion = AttackMotion.Near;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);

            pet.color = new Color(0,0,0,0);
        }

        public void UseSkill(Character _user, SkillSO _skill)
        {
            testMotion = _skill.motion;

            Team userTeam = _user.team;
            Team targetTeam = (userTeam == Team.Player) ? Team.Enemy : Team.Player;

            bool isPet = Bool.IsPetMotion(_skill.motion);

            float time = 0.4f;

            Sequence seq = DOTween.Sequence();

            // Cam
            seq.Append(CamRotate(1f, targetTeam, time));
            seq.Join(CamMove(targetTeam, time).OnStart(()=> StartCoroutine(CameraZoom(50, time))));


            //CharacterMove
            if (!isPet)
                seq.Insert(0.1f, CharacterMove(_user.GetVisual(), time - 0.1f));
            else
            {
                pet.transform.parent = _user.transform;
                pet.transform.SetAsFirstSibling();
                pet.transform.rotation = Quaternion.Euler(0, 180, 0);
                pet.transform.position = _user.transform.position;
                pet.sprite = _skill.summon;
                pet.color = Color.white;

                seq.Prepend(DOTween.To(() => 0f, x => { }, 1f, 0.1f).OnStart(()=>_user.visualAction?.Invoke()));
                seq.Insert(0.3f, CharacterMove(pet.transform, time - 0.1f));
            }
            
            seq.Append(DOTween.To(() => 0f, x => { }, 1f, 0.03f).OnComplete(()=> {
                if (isPet)
                    seq.Join(ShortDash(pet.transform, 0.1f).OnStart(()=>pet.sprite = _skill.summonAnime));

                _user.skillActions?.Invoke();
            }));

            //Init
            time = 0.2f;

            seq.AppendInterval(0.5f);
            seq.Append(CamRotate(0, Team.None, time));
            seq.Join(CamMove(Team.None, time).OnStart(()=>StartCoroutine(CameraZoom(60, 0.2f))));

            if(isPet == false) seq.Join(CharacterReturn(_user.GetVisual(), time + 0.15f).OnStart(()=>_user.SkillFin()));
            else
            {
                seq.Join(CharacterReturn(pet.transform, time + 0.15f).OnStart(() => _user.SkillFin()));
                seq.Join(pet.DOFade(0, time).OnComplete(()=>pet.transform.parent = null));
            }
        }

        private Tween CharacterMove(Transform _target, float _t)
        {
            return _target.DOMove(new Vector3(0, 13, _target.position.z), _t);
        }

        private Tween ShortDash(Transform _tar, float _t) => _tar.DOLocalMoveX(_tar.localPosition.x + 100, _t);

        private Tween CharacterReturn(Transform _target, float _t) => _target.DOLocalMove(Vector2.zero, _t);

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
            if (_target == Team.Enemy) pos.x = 47.45f;
            if (_target == Team.Player) pos.x = -47.45f;
            if (_target != Team.None) pos.y = 7.5f;

            return mainCam.transform.DOLocalMove(pos, 0.75f);
        }

    }
}
