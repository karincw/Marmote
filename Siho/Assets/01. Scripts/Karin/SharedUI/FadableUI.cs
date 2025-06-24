using DG.Tweening;
using UnityEngine;

namespace karin
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadableUI : MonoBehaviour
    {
        protected CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeTime = 0.4f;

        protected float _fadePosY = 0;
        protected RectTransform rt;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            rt = transform as RectTransform;
            _fadePosY = Camera.main.pixelHeight;
            rt.localPosition = new Vector2(0, _fadePosY);
        }

        protected virtual void Start()
        {
            Utils.FadeCanvasGroup(_canvasGroup, true, 0);
        }

        public virtual void Open()
        {
            rt.DOLocalMoveY(0, _fadeTime).SetEase(Ease.Linear);
            Utils.FadeCanvasGroup(_canvasGroup, false, _fadeTime);
        }

        public virtual void Close()
        {
            rt.DOLocalMoveY(_fadePosY, _fadeTime).SetEase(Ease.Linear);
            Utils.FadeCanvasGroup(_canvasGroup, true, _fadeTime);
        }
    }
}