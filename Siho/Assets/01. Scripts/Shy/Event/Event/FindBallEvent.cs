using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindBallEvent : EventPanel
{

    [SerializeField] private float _shuffleTime;
    [SerializeField] private float _shuffleCount;

    [SerializeField] private List<Transform> _cupTrms;
    [SerializeField] private Transform _ballTrm;
    [SerializeField] private int _ballPositionIndex;

    [SerializeField] private List<HorizontalLayoutGroup> _layoutGroups;
    

    protected override void Awake()
    {
        base.Awake();

        _ballPositionIndex = Random.Range(0, _cupTrms.Count);
        _ballTrm.SetSiblingIndex(_ballPositionIndex);
    }

    private void Start()
    {

    }

    public void Shuffle()
    {
        _layoutGroups.ForEach(g => g.enabled = false);
    }

    public void Find(int index)
    {

    }

    [ContextMenu("o")]
    public void OpenCup()
    {
        foreach (var c in _cupTrms)
        {
            c.DOLocalMoveY(c.transform.position.y + 50, 0.5f);
        }
    }
    [ContextMenu("c")]
    public void CloseCup()
    {
        foreach (var c in _cupTrms)
        {
            c.DOLocalMoveY(c.transform.position.y - 50, 0.5f);
        }
    }

    public void ShuffleOneStep()
    {

    }

    public void ShuffleCoroutine()
    {

    }

}
