using TMPro;
using UnityEngine;
using UnityEngine.Events;
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
    [SerializeField] private UnityEvent _pressedEvent;

    private Image _image;
    private TMP_Text _text;

    protected override void Awake()
    {
        base.Awake();
        _text = GetComponentInChildren<TMP_Text>();
        _image = GetComponent<Image>();
    }

    public void SetText(string text)
    {
        _text.text = text;
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
        _pressedEvent?.Invoke();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _image.color = _disabledColor;
    }
    protected override void OnEnable()
    {
        _image.sprite = _baseImage;
        _image.color = _baseColor;
        base.OnEnable();
    }
}
