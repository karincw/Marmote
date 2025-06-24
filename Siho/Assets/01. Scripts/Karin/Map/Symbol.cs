using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace karin
{
    public class Symbol : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 0.4f;
        [SerializeField] private Ease _moveEase;

        public int nowIndex = 0;
        public event Action OnMoveEndEvent;
        private WaitForSeconds _passingDelay;
        private SpriteRenderer _spriteRenderer;

        private bool moveNext = true;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>(); 
        }

        private void OnEnable()
        {
            _passingDelay = new WaitForSeconds(_moveSpeed);
            WorldMapManager.Instance.OnEnterNextStage += SetUpNextStage;
        }

        private void Start()
        {
            SetTileIndex(WorldMapManager.Instance.positionIndex);
        }

        public void Move(int value)
        {
            WorldMapManager wmm = WorldMapManager.Instance;
            int adjustedValue = Mathf.Clamp(value, 1, wmm.tileCount - nowIndex - 1);
            List<Tile> moveTiles = wmm.GetTiles(nowIndex + 1, adjustedValue);

            int breakingIndex = moveTiles.FindIndex(t => t == moveTiles.FirstOrDefault(t => t.IsBreaking));
            if (breakingIndex != -1)
                moveTiles = moveTiles.GetRange(0, breakingIndex + 1).ToList();

            StartCoroutine(PassingAnimationCoroutine(moveTiles));
            StartCoroutine(MoveCoroutine(moveTiles));
        }

        private IEnumerator PassingAnimationCoroutine(List<Tile> targetTiles)
        {
            foreach (var tile in targetTiles)
            {
                yield return _passingDelay;
                tile.PassingAnimation();
            }
        }
        private IEnumerator MoveCoroutine(List<Tile> targetTiles)
        {
            foreach (Tile tile in targetTiles)
            {
                moveNext = false; 
                if(tile.transform.position.x - transform.position.x < 0)
                    _spriteRenderer.flipX = false;
                else
                    _spriteRenderer.flipX = true;
                transform.DOJump(tile.transform.position, 0.6f, 1, _moveSpeed).SetEase(_moveEase)
                //transform.DOMove(tile.transform.position, _moveSpeed).SetEase(_moveEase)
                    .OnComplete(() => { moveNext = true; });
                yield return new WaitUntil(() => moveNext);
            }
            nowIndex += targetTiles.Count;
            WorldMapManager.Instance.positionIndex = nowIndex;
            Save.Instance.AutoSave();
            targetTiles[targetTiles.Count - 1].EnterAnimation();
            OnMoveEndEvent?.Invoke();
        }

        public void SetTileIndex(int index)
        {
            nowIndex = index;
            transform.position = WorldMapManager.Instance.GetTiles(index, 1).First().transform.position;
        }

        public void SetUpNextStage(int stageIndex)
        {
            nowIndex = 0;
            SetTileIndex(nowIndex);
        }

    }
}
