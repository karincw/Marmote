using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace karin.worldmap
{
    public class Symbol : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 0.4f;
        [SerializeField] private Ease _moveEase;

        public int nowIndex = 0;
        private WaitForSeconds _passingDelay;

        private bool moveNext = true;

        private void OnEnable()
        {
            _passingDelay = new WaitForSeconds(_moveSpeed);
            WorldMapManager.Instance.OnEnterNextStage += SetUpNextStage;
        }

        private void OnDisable()
        {
            WorldMapManager.Instance.OnEnterNextStage -= SetUpNextStage;
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
                transform.DOMove(tile.transform.position, _moveSpeed).SetEase(_moveEase)
                    .OnComplete(() => { moveNext = true; });
                yield return new WaitUntil(() => moveNext);
            }
            nowIndex += targetTiles.Count;
            targetTiles[targetTiles.Count - 1].EnterAnimation();
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
