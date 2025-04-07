using AYellowpaper.SerializedCollections;
using DG.Tweening;
using karin.Core;
using karin.worldmap.dice;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace karin.worldmap
{
    public class WorldMapManager : MonoSingleton<WorldMapManager>
    {
        [SerializeField] private List<Tile> _tiles;
        [SerializeField] private List<StageDataSO> _stageDatas;
        [Space(5)]
        [SerializeField, SerializedDictionary("Theme", "Theme Enemys")] private SerializedDictionary<Theme, StageEnemyList> _themeToEnemyList;

        [Space(5)]
        public int tileCount = 0;
        public int stageIndex = 0;

        private Dice _dice;
        private Floor _floor;
        private Symbol _symbol;

        private WaitForSeconds _mapChangeAnimationDelay;
        private StageDataSO _currentStage => _stageDatas[stageIndex];
        private Theme _stageTheme;

        public Action<int> OnEnterNextStage;

        private void Awake()
        {
            tileCount = _tiles.Count;
            stageIndex = 0;
            _dice = FindFirstObjectByType<Dice>();
            _floor = FindFirstObjectByType<Floor>();
            _symbol = FindFirstObjectByType<Symbol>();
            _mapChangeAnimationDelay = new WaitForSeconds(0.05f);
            _stageTheme = (Theme)Random.Range(0, 5);
        }

        private void OnEnable()
        {
            _floor.OnDiceStopEvent += HandleDiceStop;
            OnEnterNextStage += HandleNextStage;
            DataManager.Instance.OnLoadWorldMap += HandleLoadMap;
        }

        private void OnDisable()
        {
            _floor.OnDiceStopEvent -= HandleDiceStop;
            OnEnterNextStage -= HandleNextStage;
            DataManager.Instance.OnLoadWorldMap -= HandleLoadMap;
            SaveData();
        }
#if UNITY_EDITOR
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RollDice();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                HandleNextStage(0);
            }
        }
#endif

        public void SetNextStage()
        {
            OnEnterNextStage?.Invoke(++stageIndex);
            Debug.Log($"NextStage : {stageIndex}");
        }

        public void SaveData()
        {
            MapData mapData = new MapData();
            mapData.positionIndex = _symbol.nowIndex;
            mapData.stageIndex = stageIndex;
            mapData.stageTheme = _stageTheme;
            mapData.tileData = _tiles.Select(t => t.myTileData).ToList();
            DataManager.Instance.SaveMap(mapData);
        }

        public List<TileDataSO> GetStageTileData(int index)
        {
            var tiles = _stageDatas[index].GetTileDatas();
            return tiles;
        }

        private void HandleLoadMap(MapData data)
        {
            _symbol.SetTileIndex(data.positionIndex);
            stageIndex = data.stageIndex;
            _stageTheme = data.stageTheme;
            for (int i = 0; i < _tiles.Count; i++)
            {
                _tiles[i].TileChange(data.tileData[i]);
            }
        }

        public List<EnemyDataSO> GetBattleEnemyDatas(int count)
        {
            ShuffleEnemyList(stageIndex);
            return _themeToEnemyList[_stageTheme].enemyList.GetRange(0, count);
        }

        private void ShuffleEnemyList(int stageIndex)
        {
            var shuffledList = _themeToEnemyList[_stageTheme].enemyList.OrderBy(e => Random.value).ToList();
            _themeToEnemyList[_stageTheme] = new StageEnemyList(shuffledList);
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
            if (stageIndex % 5 == 0)
                _stageTheme = (Theme)Random.Range(0, 5);

            var tileData = GetStageTileData(stageIndex);
            int halfPoint = Mathf.CeilToInt((tileCount - 1) / 2);

            var lefts = _tiles.GetRange(0, halfPoint);
            var rights = _tiles.GetRange(halfPoint, tileCount - halfPoint);

            var leftChanges = tileData.GetRange(0, halfPoint);
            var rightChanges = tileData.GetRange(halfPoint, tileCount - halfPoint);

            rights.Reverse();
            rightChanges.Reverse();
            DOTween.Complete(2);

            StartCoroutine(TileAnimationCoroutine(lefts, leftChanges));
            StartCoroutine(TileAnimationCoroutine(rights, rightChanges));
        }

        private IEnumerator TileAnimationCoroutine(List<Tile> targetTiles, List<TileDataSO> changes)
        {
            for (int i = 0; i < targetTiles.Count; i++)
            {
                yield return _mapChangeAnimationDelay;
                targetTiles[i].TileChangeAnimation(changes[i]);
            }
        }

    }
}