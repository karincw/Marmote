using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace karin
{
    public class SelectBtn : Button, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField, ColorUsage(showAlpha: true)] private Color _hoverColor;

        private bool _isHover;
        private Image _image;

        protected override void Awake()
        {
            base.Awake();
            _image = GetComponent<Image>();
            _isHover = false;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }


        public void ChangeColor(Color color)
        {
            _image.color = _isHover ? color - _hoverColor : color;
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            if (!interactable) return;
            _image.color -= _hoverColor;
            _isHover = true;
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            if (!interactable) return;
            _image.color += _hoverColor;
            _isHover = false;
        }
    }
}

