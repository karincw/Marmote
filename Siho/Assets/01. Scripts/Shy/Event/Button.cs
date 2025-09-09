using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shy.Event
{
    public class Button : MonoBehaviour, IPointerClickHandler
    {
        [Header("Button")]
        [SerializeField] protected Image targetGraphic;
        [SerializeField] protected Sprite defaultSprite, clickSprite;
        [SerializeField] protected Color normalColor = Color.white, pressedColor = new Color(0.78f, 0.78f, 0.78f);
        [SerializeField] protected float fadeDuration = 0.1f;

        public bool useable { protected get; set; }

        internal UnityAction onClickEvent;

        protected virtual void Awake()
        {
            if (targetGraphic == null) targetGraphic = GetComponent<Image>();

            if (defaultSprite == null) defaultSprite = targetGraphic.sprite;
            targetGraphic.sprite = defaultSprite;
            targetGraphic.color = normalColor;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!useable) return;

            targetGraphic.color = pressedColor;
            if (clickSprite != null) targetGraphic.sprite = clickSprite;

            ButtonFade();   
        }

        protected virtual void ButtonFade()
        {
            SequnceTool.Instance.Delay(() =>
            {
                targetGraphic.color = normalColor;
                targetGraphic.sprite = defaultSprite;

                onClickEvent?.Invoke();
            }, fadeDuration);
        }
    }
}