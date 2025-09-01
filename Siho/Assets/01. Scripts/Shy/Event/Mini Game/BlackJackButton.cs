using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shy.Event
{
    public class BlackJackButton : MonoBehaviour, IPointerClickHandler
    {
        [Header("Button")]
        [SerializeField] private Image targetGraphic;
        [SerializeField] private Color normalColor = Color.white, pressedColor = new Color(0.78f, 0.78f, 0.78f);
        [SerializeField] private float fadeDuration = 0.1f;

        internal UnityAction onClickEvent;

        private bool useable;
        private GameObject lockImg;

        private void Awake()
        {
            lockImg = transform.Find("Lock").gameObject;
            if (targetGraphic == null) targetGraphic = GetComponent<Image>();
        }

        public void LockChange(bool _useable)
        {
            lockImg.SetActive(!_useable);
            UseableChange(_useable);
        }

        public void UseableChange(bool _useable) => useable = _useable;

        public void OnPointerClick(PointerEventData eventData)
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
