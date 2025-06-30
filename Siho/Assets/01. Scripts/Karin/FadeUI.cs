using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeUI : MonoBehaviour
{
    [Header("Fade-Settings")]
    [SerializeField] private FadePosition _fadePositionType;
    [SerializeField] private float _fadeTime;
    [SerializeField] private Ease _tweenEase;
    [SerializeField] private bool _startOpen = false;
    private Vector2 _fadePos;
    private bool _isOpen;

    protected CanvasGroup _canvasGroup;
    protected RectTransform _rectTrm;

    protected virtual void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _rectTrm = transform as RectTransform;
        SetUp();
    }

    protected virtual void SetUp()
    {
        Vector2 position = default;
        switch (_fadePositionType)
        {
            case FadePosition.None:
                position = Vector2.zero;
                break;
            case FadePosition.Left:
                position = new Vector2(-Screen.width, 0);
                break;
            case FadePosition.Up:
                position = new Vector2(0, Screen.width);
                break;
            case FadePosition.Down:
                position = new Vector2(0, -Screen.width);
                break;
            case FadePosition.Right:
                position = new Vector2(Screen.width, 0);
                break;
        }
        _fadePos = position;
        _rectTrm.localPosition = _fadePos;
        if (_startOpen)
        {
            _rectTrm.localPosition = Vector2.zero;
            Utils.FadeGroup(_canvasGroup, 0, true);
            _isOpen = true;
            return;
        }
        _isOpen = false;
        Utils.FadeGroup(_canvasGroup, 0, false);
    }

    public void Open()
    {
        if (_isOpen) return;
        _isOpen = true;
        _rectTrm.DOLocalMove(Vector2.zero, _fadeTime).SetEase(_tweenEase);
        Utils.FadeGroup(_canvasGroup, _fadeTime, true, _tweenEase);
    }

    public void Close()
    {
        if (!_isOpen) return;
        _isOpen = false;
        _rectTrm.DOLocalMove(_fadePos, _fadeTime).SetEase(_tweenEase);
        Utils.FadeGroup(_canvasGroup, _fadeTime, false, _tweenEase);
    }

}
