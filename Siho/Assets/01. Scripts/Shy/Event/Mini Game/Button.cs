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
        [SerializeField] protected Color normalColor = Color.white, pressedColor = new Color(0.78f, 0.78f, 0.78f);
        [SerializeField] protected float fadeDuration = 0.1f;

        internal UnityAction onClickEvent;

        protected virtual void Awake()
        {
            if (targetGraphic == null) targetGraphic = GetComponent<Image>();
        }

        public bool useable { protected get; set; }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (!useable) return;

            targetGraphic.color = pressedColor;
            SequnceTool.Instance.Delay(() =>
            {
                targetGraphic.color = normalColor;
                onClickEvent?.Invoke();
            }, fadeDuration);
        }
    }
}