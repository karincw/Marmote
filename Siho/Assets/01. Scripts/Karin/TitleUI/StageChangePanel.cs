using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageChangePanel : MonoBehaviour
{
    [SerializeField] private Button _left;
    [SerializeField] private Button _right;
    [SerializeField] private RectTransform _contentsTrm;

    private List<Stage> _contents = new();

    private readonly int _moveInterval = 559;
    private int _currentIdx;

    private CanvasGroup _canvasGroup;
    private PlayScreen _playScreen;

    private void Awake()
    {
        _playScreen = GetComponentInParent<PlayScreen>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _contentsTrm.GetComponentsInChildren<Stage>(_contents);
        _currentIdx = 0;
        _left.onClick.AddListener(MoveLeft);
        _right.onClick.AddListener(MoveRight);
        Utils.FadeGroup(_canvasGroup, 0, false);
    }

    private void Start()
    {
        DataLinkManager.instance.stage = _contents[0];
    }

    public void SetIndex(int idx)
    {
        _contentsTrm.anchoredPosition = new Vector2(_moveInterval * idx, 0);
        _contents[_currentIdx].transform.DOScale(0.5f, 0.5f);
        _currentIdx = idx;
        _contents[_currentIdx].transform.DOScale(1, 0.5f);
    }

    private void MoveLeft()
    {
        if (_currentIdx == 0) return;
        _contentsTrm.DOAnchorPos(_contentsTrm.anchoredPosition + new Vector2(_moveInterval, 0), 0.5f);
        _contents[_currentIdx].transform.DOScale(0.5f, 0.5f);
        _currentIdx--;
        _contents[_currentIdx].transform.DOScale(1, 0.5f);
    }
    private void MoveRight()
    {
        if (_currentIdx == _contents.Count - 1) return;
        _contentsTrm.DOAnchorPos(_contentsTrm.anchoredPosition - new Vector2(_moveInterval, 0), 0.5f);
        _contents[_currentIdx].transform.DOScale(0.5f, 0.5f);
        _currentIdx++;
        _contents[_currentIdx].transform.DOScale(1, 0.5f);
    }

    public void Select()
    {
        _playScreen.SetStage(_contents[_currentIdx].stageData);
        DataLinkManager.instance.stage = _contents[_currentIdx];
        _playScreen.OpenMain();
    }
}
