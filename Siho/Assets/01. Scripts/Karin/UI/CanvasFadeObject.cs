using DG.Tweening;
using UnityEngine;

namespace karin
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasFadeObject : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        [SerializeField] private float fadeTime;

        protected virtual void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual void Open()
        {
            Fade(true);
        }
        public virtual void Close()
        {
            Fade(false);
        }

        protected virtual void Fade(bool inOut)
        {
            _canvasGroup.DOFade(inOut ? 1 : 0, fadeTime);
            _canvasGroup.interactable = inOut;
            _canvasGroup.blocksRaycasts = inOut;
        }
    }

}