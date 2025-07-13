using DG.Tweening;
using System;
using UnityEngine;

public class Symbol : MonoBehaviour
{

    [Header("MoveAnimation-Settings")]
    [SerializeField] private float _moveTime;
    [SerializeField] private Ease _moveEase;
    [SerializeField] private float _jumpPower = 1;

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
        Vector3 startPos = transform.position;
        float halfTime = _moveTime / 2;
        Vector3 endPos;
        Vector3 midPos;

        foreach (var tile in tiles)
        {
            endPos = tile.transform.position;
            midPos = ((endPos - startPos) / 2f) + startPos;
            midPos.y += _jumpPower; // 위로 점프

            Vector3 dir = (endPos - startPos).normalized;

            if (dir.x < 0 && dir.y > 0)
                midPos.x += _jumpPower;
            else if (dir.x > 0 && dir.y > 0)
                midPos.x += -_jumpPower;
            else if (dir.x < 0 && dir.y < 0)
                midPos.x += -_jumpPower;
            else if (dir.x > 0 && dir.y < 0)
                midPos.x += +_jumpPower;

            sequence.Append(transform.DOMove(midPos, halfTime).SetEase(_moveEase));
            sequence.Append(transform.DOMove(endPos, halfTime).SetEase(_moveEase));

            sequence.AppendCallback(() =>
            {
                tile.OnThrowEvent();
            });

            startPos = endPos;
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
