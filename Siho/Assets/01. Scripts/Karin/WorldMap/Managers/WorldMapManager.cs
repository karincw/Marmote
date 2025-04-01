using DG.Tweening;
using karin.Core;
using karin.worldmap.dice;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace karin.worldmap
{
    public class WorldMapManager : MonoSingleton<WorldMapManager>
    {
        [SerializeField] private List<Tile> _tiles;
        [SerializeField] private List<StageEnemyList> _stageToEnemyList;

        public int tileCount = 0;
        public int stageIndex = 0;

        private Dice _dice;
        private Floor _floor;
        private Symbol _symbol;

        private WaitForSeconds _mapChangeAnimationDelay;

        private void Awake()
        {
            tileCount = _tiles.Count;
            stageIndex = 0;
            _dice = FindFirstObjectByType<Dice>();
            _floor = FindFirstObjectByType<Floor>();
            _symbol = FindFirstObjectByType<Symbol>();
            _mapChangeAnimationDelay = new WaitForSeconds(0.05f);
        }

        private void OnEnable()
        {
            _floor.OnDiceStopEvent += HandleDiceStop;
            _symbol.OnEnterNextStage += HandleNextStage;
            DataManager.Instance.OnLoadWorldMap += HandleLoadMap;
        }

        private void OnDisable()
        {
            _floor.OnDiceStopEvent -= HandleDiceStop;
            _symbol.OnEnterNextStage -= HandleNextStage;
            DataManager.Instance.OnLoadWorldMap -= HandleLoadMap;
            SaveData();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RollDice();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                HandleNextStage(1);
            }
        }

        public void SaveData()
        {
            MapData mapData = new MapData();
            mapData.positionIndex = _symbol.nowIndex;
            mapData.stageIndex = stageIndex;
            mapData.tileData = _tiles.Select(t => t.myTileData).ToList();
            DataManager.Instance.SaveMap(mapData);
        }

        private void HandleLoadMap(MapData data)
        {
            _symbol.SetTileIndex(data.positionIndex);
            stageIndex = data.stageIndex;
            for (int i = 0; i < _tiles.Count; i++)
            {
                _tiles[i].TileChange(data.tileData[i]);
            }
        }

        public List<EnemyDataSO> GetBattleEnemyDatas()
        {
            ShuffleEnemyList(stageIndex);
            return _stageToEnemyList[stageIndex].enemyList.GetRange(0, 3);
        }

        private void ShuffleEnemyList(int stageIndex)
        {
            var shuffledList = _stageToEnemyList[stageIndex].enemyList.OrderBy(e => Random.value).ToList();
            _stageToEnemyList[stageIndex] = new StageEnemyList(shuffledList);
        }

        public List<Tile> GetTiles(int index, int count)
        {
            return _tiles.GetRange(index, count);
        }

        public void RollDice()
        {
            _dice.DiceRoll();
            _floor.resultOut = false;
        }

        private void HandleDiceStop(int result)
        {
            _symbol.Move(result);
        }

        [ContextMenu("UpgradeWorldMap")]
        private void HandleNextStage(int stageIndex)
        {
            int halfPoint = Mathf.CeilToInt((tileCount - 1) / 2);
            var lefts = _tiles.GetRange(0, halfPoint);
            var rights = _tiles.GetRange(halfPoint, tileCount - halfPoint);
            rights.Reverse();
            DOTween.Complete(2);
            StartCoroutine(TileAnimationCoroutine(lefts));
            StartCoroutine(TileAnimationCoroutine(rights));
        }

        private IEnumerator TileAnimationCoroutine(List<Tile> targetTiles)
        {
            foreach (var tile in targetTiles)
            {
                yield return _mapChangeAnimationDelay;
                tile.TileChangeAnimation();
            }
        }

    }
}