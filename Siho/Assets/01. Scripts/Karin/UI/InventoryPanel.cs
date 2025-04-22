using DG.Tweening;
using UnityEngine;
namespace karin.ui
{
    public class InventoryPanel : MonoBehaviour
    {
        [SerializeField] private Ease _openCurve;
        [SerializeField] private Ease _closeCurve;
        [SerializeField] private float _animationTime = 1f;

        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private Vector2 originPos;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = transform as RectTransform;
            originPos = _rectTransform.localPosition;
            Close();
        }

        public void Open()
        {
            _canvasGroup.DOFade(1, _animationTime);
            _rectTransform.DOLocalMoveX(originPos.x, _animationTime).SetEase(_openCurve).OnComplete(() =>
            {
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
            });
        }

        public void Close()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.DOFade(0, _animationTime);
            _rectTransform.DOLocalMoveX(originPos.x - 1080, _animationTime).SetEase(_closeCurve).OnComplete(() =>
            {
                _canvasGroup.blocksRaycasts = false;
            });
        }
    }
}
