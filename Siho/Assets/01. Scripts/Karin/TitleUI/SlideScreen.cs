using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlideScreen : MonoBehaviour
{
    [SerializeField] private List<CanvasGroup> _screenList;
    [SerializeField] private List<Button> _screenBtns;
    [SerializeField] private int _startIndex;
    private int _nowIndex;
    private float _screenWidth;
    private RectTransform _screenLayoutTrm;

    [Header("Tween-Settings")]
    [SerializeField] private float _tweenTime;
    [SerializeField] private Ease _tweenEase = Ease.OutQuint;

    private void Awake()
    {
        _screenWidth = Screen.width;
        _screenLayoutTrm = transform.Find("ScreenLayout") as RectTransform;
        SetUp();
        _screenLayoutTrm.localPosition = new Vector2(-_screenWidth * _startIndex, 0);
        _nowIndex = _startIndex;
    }

    private void SetUp()
    {
        for (int i = 0; i < _screenList.Count; i++)
        {
            _screenList[i].transform.localPosition = Vector2.right * _screenWidth * i;
            int index = i;
            _screenBtns[i].onClick.AddListener(() =>
            {
                Move(index);
            });
        }
    }

    public void Move(int index)
    {
        if (index == _nowIndex) return;
        foreach (var screen in _screenList)
        {
            if (_screenList[index] == screen)
            {
                screen.interactable = true;
                screen.blocksRaycasts = true;
                continue;
            }
            screen.interactable = false;
            screen.blocksRaycasts = false;
        }
        _screenLayoutTrm.DOLocalMoveX(-_screenWidth * index, _tweenTime).SetEase(_tweenEase);
        _nowIndex = index;
    }
}
