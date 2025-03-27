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

        private int _nowMapIdx = 0;
        private WaitForSeconds _passingDelay;

        [SerializeField] private int debugValue;

        private bool moveNext = true;

        public event Action OnEnterNextStage;

        private void OnEnable()
        {
            _passingDelay = new WaitForSeconds(_moveSpeed);
            OnEnterNextStage += SetUpNextStage;
        }

        private void OnDisable()
        {
            OnEnterNextStage -= SetUpNextStage;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Move(debugValue);
            }
        }

        public void Move(int value)
        {
            WorldMapManager wmm = WorldMapManager.Instance;
            int adjustedValue = Mathf.Clamp(value, 1, wmm._tileCount - _nowMapIdx);
            List<Tile> moveTiles = wmm.GetTiles(_nowMapIdx + 1, adjustedValue);
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
                    .OnComplete(() =>
                    {
                        moveNext = true;
                    });
                yield return new WaitUntil(() => moveNext);
            }
            targetTiles[targetTiles.Count - 1].EnterAnimation();
            _nowMapIdx += targetTiles.Count;
            Debug.Log(_nowMapIdx);
            if (_nowMapIdx == 36)
            {
                Debug.Log("StageEnd");
                OnEnterNextStage?.Invoke();
            }
        }

        public void SetUpNextStage()
        {
            _nowMapIdx = 0;
        }

    }
}
