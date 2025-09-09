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

    private int _moveInterval;
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

        _moveInterval = Screen.width;

        RectTransform crtr = (_contentsTrm as RectTransform);
        crtr.sizeDelta = new Vector2(Screen.width * 3, crtr.sizeDelta.y);
    }

    private void Start()
    {
        Debug.Log("Start");
        DataLinkManager.instance.stage = _contents[0].stageData;

        for (int i = 0; i < _contents.Count; i++)
        {
            _contents[i].canSelect = DataLinkManager.instance.stageData.stageEnable[i];
        }
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
        _contentsTrm.DOComplete();
        _contentsTrm.DOAnchorPos(_contentsTrm.anchoredPosition + new Vector2(_moveInterval, 0), 0.5f);
        _contents[_currentIdx].transform.DOScale(0.5f, 0.5f);
        _currentIdx--;
        _contents[_currentIdx].transform.DOScale(1, 0.5f);
    }
    private void MoveRight()
    {
        if (_currentIdx == _contents.Count - 1) return;
        _contentsTrm.DOComplete();
        _contentsTrm.DOAnchorPos(_contentsTrm.anchoredPosition - new Vector2(_moveInterval, 0), 0.5f);
        _contents[_currentIdx].transform.DOScale(0.5f, 0.5f);
        _currentIdx++;
        _contents[_currentIdx].transform.DOScale(1, 0.5f);
    }

    public void Select()
    {
        _playScreen.SetStage(_contents[_currentIdx].stageData);
        DataLinkManager.instance.stage = _contents[_currentIdx].stageData;
        _playScreen.OpenMain();
    }
}
