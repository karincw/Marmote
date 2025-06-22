using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Shy.Unit;

namespace Shy.Anime
{
    public class CharacterMotion
    {
        public Tween CharacterDrop(Transform _target, float _t) => _target.DOMoveY(0.5f, _t);

        public Tween PetSpawn(Transform _user, Image _pet, Sprite _petVisual, float _t)
        {
            _pet.transform.SetParent(_user);
            _pet.transform.localPosition = Vector2.zero;
            _pet.sprite = _petVisual;

            return _pet.DOFade(1, _t);
        }

        public Tween CharacterMove(Transform _target, Transform _user, Team _team, bool _isLong, float _t = 0.22f)
        {
            float _dir = ((_team == Team.Enemy) ? -1 : 1), _value = (_isLong ? 7.2f : 2.3f);
            Vector3 _result = _target.position;
            _result += Vector3.right * _dir * _value;

            return _user.DOMove(_result, _t);
        }

        public Tween CharacterMove(Transform _user, Team _team, float _t = 0.2f)
        {
            float _x = ((_team == Team.Enemy) ? 1.5f : -1.5f);
            Vector3 _result = new Vector3(_x, -0.3f, _user.position.z);
            return _user.DOMove(_result, _t);
        }

        public Tween Knockback(Transform _target, Team _team, float _time = 0.35f)
        {
            float _dir = ((_team == Team.Enemy) ? 1 : -1);

            Sequence _seq = DOTween.Sequence();

            _seq.Append(_target.DOMoveX(_dir * 0.8f, _time).SetRelative());
            _seq.Join(_target.DOMoveY(0.5f, _time * 0.5f).SetLoops(2, LoopType.Yoyo).SetRelative());

            return _seq;
        }

        public Tween Knockback(Character[] _targets, float _time = 0.35f)
        {
            Sequence _seq = DOTween.Sequence();

            _seq.AppendInterval(0);

            foreach (var _character in _targets)
            {
                _seq.Join(Knockback(_character.GetVisual(), _character.team, _time));
            }

            return _seq;
        }

        public Tween ShortDash(Transform _user, Team _team, float _t = 0.2f)
        {
            float _value = ((_team == Team.Enemy) ? 0.7f : -0.7f);
            return _user.DOMoveX(_value, _t).SetRelative();
        }

        public Tween CharacterReturn(Transform _target, bool _fade = true, float _t = 0.3f)
        {
            var _seq = DOTween.Sequence();
            var _visual = _target.GetComponent<Image>();

            _seq.AppendInterval(0);

            if(_fade) _seq.Append(_visual.DOFade(0, _t - 0.1f));

            _seq.Join(_target.DOLocalMove(Vector2.zero, _t)
                .OnComplete(()=>_visual.DOFade(1, 0)));

            return _seq;
        }
    }
}
