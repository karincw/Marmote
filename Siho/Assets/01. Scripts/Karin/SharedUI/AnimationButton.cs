using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AnimationButton : Button
{
    [SerializeField] private Sprite _baseImage;
    [SerializeField, ColorUsage(true)] private Color _baseColor;
    [SerializeField, ColorUsage(true)] private Color _pressedColor;
    [SerializeField] private Sprite _pressedImage;
    [SerializeField, ColorUsage(true)] private Color _disabledColor;

    private Image _image;

    protected override void Awake()
    {
        base.Awake();
        _image = GetComponent<Image>();
        _image.sprite = _baseImage;
        _image.color = _baseColor;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        _image.sprite = _baseImage;
        _image.color = _baseColor;
        base.OnPointerUp(eventData);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);

        _image.color = _pressedColor;
        _image.sprite = _pressedImage;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _image.color = _disabledColor;
    }
}
