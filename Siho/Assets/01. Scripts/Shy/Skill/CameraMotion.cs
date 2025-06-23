using DG.Tweening;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

namespace Shy.Anime
{
    public class CameraMotion : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera cam;

        public void CamZoom(float _lastValue = 60, float _t = 0.5f) => StartCoroutine(CameraZoom(_lastValue, _t));
        private IEnumerator CameraZoom(float _lastValue, float _t)
        {
            float startValue = cam.Lens.FieldOfView, elapsed = 0f;

            while (elapsed < _t)
            {
                elapsed += Time.deltaTime;
                cam.Lens.FieldOfView = Mathf.Lerp(startValue, _lastValue, Mathf.Clamp01(elapsed / _t));
                yield return null;
            }

            cam.Lens.FieldOfView = _lastValue;
        }

        public Tween CamRotate(float _value, Team _target, float _t = 1.8f)
        {
            if (_target == Team.Enemy) _value = -_value;
            Tween t = cam.transform.DOLocalRotate(new Vector3(0, 0, _value), 0.75f);
            return t;
        }

        public Tween CamReturn(float _t = 0.55f) => cam.transform.DOMove(Vector2.zero, _t);

        public Tween CamMove(Transform _user, Team _team, float _t = 0.2f)
        {
            Vector2 _vec = _user.position;
            _vec.x += ((_team == Team.Player) ? -2.85f : 2.85f);

            return cam.transform.DOMove(_vec, _t);
        }

        public Tween CamMove(Transform _target, Team _team, bool _isLong, float _t = 0.4f)
        {
            float _dir = ((_team == Team.Enemy) ? -1 : 1), _value = (_isLong ? 3.7f : 2.2f);
            Vector2 _result = new Vector2(_target.position.x + _dir * _value, _target.position.y - 0.35f);
            
            return cam.transform.DOMove(_result, _t);
        }

        public Tween CamMove(float _x, Team _team, float _t)
        {
            if (_team == Team.Player) _x = -_x;
            return cam.transform.DOMoveX(_x, _t).SetRelative();
        }

        public Tween CamMove(Team _team, float _t = 0.2f)
        {
            float _x = ((_team == Team.Enemy) ? 3.15f : -3.15f);
            Vector2 _result = new Vector2(_x, -0.13f);

            return cam.transform.DOMove(_result, _t);
        }

        
    }
}
