using DG.Tweening;
using System;
using UnityEngine;

public class Symbol : MonoBehaviour
{

    [Header("MoveAnimation-Settings")]
    [SerializeField] private float _moveTime;
    [SerializeField] private Ease _moveEase;

    public int index;
    public static Action<int> OnMoveEndEvent;
    public Sequence sequence;

    [SerializeField] private Transform _camViewPos;

    private void Awake()
    {
        index = 0;
        sequence = null;
        DicePanel.OnDiceRollEndEvent += Move;
    }

    private void OnDestroy()
    {
        DicePanel.OnDiceRollEndEvent -= Move;
    }

    public void Move(int count)
    {
        sequence = DOTween.Sequence();
        var tiles = MapManager.instance.GetMoveMap(index + 1, count);
        _camViewPos.position = tiles[tiles.Count / 2].transform.position + Vector3.up;
        CameraControler.instance.ZoomIn();
        sequence.AppendInterval(0.2f);
        foreach (var tile in tiles)
        {
            sequence.Append(transform.DOMove(tile.transform.position, _moveTime).SetEase(_moveEase));
            sequence.AppendCallback(() =>
            {
                tile.OnThrowEvent();
            });
        }
        sequence.AppendCallback(() =>
        {
            index += count;
            OnMoveEndEvent?.Invoke(index);
            CameraControler.instance.ZoomOut();
            sequence = null;
        });
    }

}
