using DG.Tweening;
using UnityEngine;

namespace karin
{
    public class ShakableUI : MonoBehaviour
    {
        private RectTransform _rectTransform;

        protected virtual void Awake()
        {
            _rectTransform = transform as RectTransform;
        }

        public void Shake(float dur, Vector3 PosPower, Vector3 RotPower, Ease easing = Ease.Linear)
        {
            _rectTransform.DOComplete();
            _rectTransform.DOShakeAnchorPos(dur, PosPower).SetEase(easing);
            _rectTransform.DOShakeRotation(dur, RotPower, randomness: 1).SetEase(easing);
        }
    }
}