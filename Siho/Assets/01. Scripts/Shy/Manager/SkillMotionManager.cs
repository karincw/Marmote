using UnityEngine;
using DG.Tweening;
using Unity.Cinemachine;
using System.Collections;
using UnityEngine.UI;

namespace Shy
{
    public class SkillMotionManager : MonoBehaviour
    {
        public static SkillMotionManager Instance;

        [SerializeField] private CinemachineCamera mainCam;
        [SerializeField] private Image pet;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(this);

            pet.color = new Color(0,0,0,0);
        }

        public void UseSkill(Character _user, SkillSO _skill)
        {
            Team userTeam = _user.team;
            Team targetTeam = (userTeam == Team.Player) ? Team.Enemy : Team.Player;

            bool isPet = Bool.IsPetMotion(_skill.motion), isTeam = Bool.IsTeamMotion(_skill.motion),
                isSummon = Bool.IsSummonMotion(_skill.motion);
            float time = 0.4f;

            Sequence seq = DOTween.Sequence();

            // Cam
            if(_skill.motion != AttackMotion.All)
            {
                seq.Append(CamMove(isTeam ? userTeam : targetTeam, time).OnStart(() => StartCoroutine(CameraZoom(50, time))));
                if (!isTeam) seq.Join(CamRotate(1f, targetTeam, time));
            }
            
            //CharacterMove
            if (!isPet && !isTeam && !isSummon)
                seq.Insert(0.1f, CharacterMove(_user.GetVisual(), time - 0.1f));
            else if (isPet || isSummon)
            {
                pet.transform.parent = _user.transform;
                pet.transform.SetAsFirstSibling();
                pet.transform.rotation = Quaternion.Euler(0, targetTeam == Team.Enemy ? 180 : 0, 0);
                pet.sprite = _skill.summon;
                
                seq.Prepend(DOTween.To(() => 0f, x => { }, 1f, 0.1f).OnStart(() => _user.visualAction?.Invoke()));
                pet.color = Color.white;
                pet.transform.localPosition = Vector3.zero;

                if (isPet)
                {
                    seq.Insert(0.3f, CharacterMove(pet.transform, time - 0.1f));
                }
                else if(isSummon)
                {
                    pet.transform.position = new Vector3(0, 30, pet.transform.position.z);
                    seq.Insert(0.2f, CharacterDrop(pet.transform, time));
                }
            }
            
            seq.Append(DOTween.To(() => 0f, x => { }, 0.3f, 0.03f).OnComplete(()=> 
            {
                if (isPet || isSummon)
                {
                    pet.sprite = _skill.summonAnime;

                    if (!isTeam && _skill.motion != AttackMotion.SummonAndLong) seq.Join(ShortDash(pet.transform, 0.1f));
                }

                _user.skillActions?.Invoke();
            }));

            //Init
            time = 0.2f;

            seq.AppendInterval(0.5f);
            seq.Append(CamRotate(0, Team.None, time));
            seq.Join(CamMove(Team.None, time).OnStart(()=>StartCoroutine(CameraZoom(60, 0.2f))));

            if(!isPet && !isSummon) seq.Join(CharacterReturn(_user.GetVisual(), time + 0.15f).OnStart(()=>_user.SkillFin()));
            else
            {
                seq.Join(CharacterReturn(pet.transform, time + 0.15f).OnStart(() => _user.SkillFin()));
                seq.Join(pet.DOFade(0, time).OnComplete(()=>pet.transform.parent = null));
            }
        }

        private Tween CharacterDrop(Transform _target, float _t) => _target.DOMoveY(13, _t);

        private Tween CharacterMove(Transform _target, float _t) => _target.DOMove(new Vector3(0, 13, _target.position.z), _t);

        private Tween ShortDash(Transform _tar, float _t) => _tar.DOLocalMoveX(_tar.localPosition.x + 100, _t);

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
            if (_target == Team.Enemy) pos.x = 47.45f;
            else if (_target == Team.Player) pos.x = -47.45f;
            else if (_target != Team.None) pos.y = 7.5f;

            return mainCam.transform.DOLocalMove(pos, 0.75f);
        }
        #endregion
    }
}
