using DG.Tweening;
using UnityEngine;

namespace karin
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private float _passingAnimPushValue;
        [SerializeField] private float _passingAnimPushDuration;
        [SerializeField] private AnimationCurve _passingAnimPushEase;

        [SerializeField] private float _passingAnimPullDuration;
        [SerializeField] private Ease _passingAnimPullEase;

        [ContextMenu("PassingAnimation")]
        public void PassingAnimation()
        {
            float originYPos = transform.position.y;
            Sequence seq = DOTween.Sequence()
                .Append(transform.DOLocalMoveY(transform.position.y - _passingAnimPushValue, _passingAnimPushDuration).SetEase(_passingAnimPushEase))
                .Append(transform.DOLocalMoveY(originYPos, _passingAnimPullDuration).SetEase(_passingAnimPullEase));
        }
        public void EnterAnimation()
        {
            Debug.Log("EnterAnimation");
            EnterEvent(); 
        }
        private void EnterEvent()
        {

        }
    }
}