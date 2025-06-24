using AYellowpaper.SerializedCollections;
using DG.Tweening;
using Shy.Unit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace karin
{
    public class WorldMapManager : MonoSingleton<WorldMapManager>
    {
        private const int StageThemeChangeInterval = 5;
        private const float MapChangeDelay = 0.05f;

        [Header("Settings")]
        [SerializeField, SerializedDictionary("Theme", "Theme Enemys")] private SerializedDictionary<Theme, SerializeEnemyList> _themeToEnemyList;
        [SerializeField, SerializedDictionary("Type", "Tile")] private SerializedDictionary<TileType, TileDataSO> _typeToTileList;
        [SerializeField] private List<Tile> _tiles;
        [SerializeField] private List<StageDataSO> _stageDatas;

        public int tileCount => _tiles.Count;
        public int stageIndex { get; private set; } = 0;
        public Theme stageTheme;
        public int positionIndex;

        public event Action<int> OnEnterNextStage;

        private Floor _floor;
        private Symbol _symbol;
        public Button rollBtn;

        private WaitForSeconds _mapChangeDelayWait;

        private void Awake()
        {
            _floor = FindFirstObjectByType<Floor>();
            _symbol = FindFirstObjectByType<Symbol>();
            _mapChangeDelayWait = new WaitForSeconds(MapChangeDelay);
            Init();
        }

        private void Init()
        {
            var loadedData = Load.Instance.GetRunData(Save.Instance.slotIndex);
            if (loadedData.HasValue &&loadedData.Value.load)
            { //데이터가 있음
                stageTheme = loadedData.Value.stageTheme;
                stageIndex = loadedData.Value.stageIndex;
                SetStageLoad(loadedData.Value.tileData);
                positionIndex = loadedData.Value.position;
            }
            else
            { //데이터가 없음
                stageTheme = GetRandomTheme();
                HandleNextStage(stageIndex);
                Load.Instance.saveRunDatas[Save.Instance.slotIndex].load = true;
            }
        }

        protected override void Start()
        {
            Save.Instance.AutoSave();
            base.Start();
        }

        private void OnEnable()
        {
            _floor.OnDiceStopEvent += HandleDiceStop;
            OnEnterNextStage += HandleNextStage;
            _symbol.OnMoveEndEvent += HandleMoveEnd;
        }

        private void OnDisable()
        {
            _floor.OnDiceStopEvent -= HandleDiceStop;
            OnEnterNextStage -= HandleNextStage;
            _symbol.OnMoveEndEvent -= HandleMoveEnd;
        }

        private void HandleMoveEnd()
        {
            rollBtn.interactable = true;
        }

        public void MoveNextStage()
        {
            OnEnterNextStage?.Invoke(++stageIndex);
        }

        public List<TileDataSO> GetStageTileData(int index)
        {
            return _stageDatas[index].GetTileDatas()
                .Select(type => _typeToTileList[type])
                .ToList();
        }

        public List<TileType> GetTileData()
        {
            return _tiles.Select(t => t.myTileData.tileType).ToList();
        }

        public List<EnemySO> GetBattleEnemyDatas(int count)
        {
            var list = _themeToEnemyList[stageTheme].list;
            var shuffled = Utils.ShuffleList(list);
            return shuffled.Take(count).ToList();
        }

        public List<Tile> GetTiles(int index, int count)
        {
            return _tiles.GetRange(index, count);
        }

        private void HandleDiceStop(int result)
        {
            _symbol.Move(result);
        }

        private void HandleNextStage(int stageIdx)
        {
            if (ShouldChangeTheme(stageIdx))
                stageTheme = GetRandomTheme();

            var tileData = GetStageTileData(stageIdx);

            int half = Mathf.CeilToInt((tileCount - 1) / 2);
            var leftTiles = _tiles.GetRange(0, half);
            var rightTiles = _tiles.GetRange(half, tileCount - half);

            var leftChanges = tileData.GetRange(0, half);
            var rightChanges = tileData.GetRange(half, tileCount - half);

            rightTiles.Reverse();
            rightChanges.Reverse();

            DOTween.Complete(2); // 중복 애니메이션 방지

            StartCoroutine(TileAnimationCoroutine(leftTiles, leftChanges));
            StartCoroutine(TileAnimationCoroutine(rightTiles, rightChanges));
        }

        private void SetStageLoad(TileType[] tiles)
        {
            var tileData = tiles.Select(type => _typeToTileList[type]).ToList();

            int half = Mathf.CeilToInt((tileCount - 1) / 2);
            var leftTiles = _tiles.GetRange(0, half);
            var rightTiles = _tiles.GetRange(half, tileCount - half);

            var leftChanges = tileData.GetRange(0, half);
            var rightChanges = tileData.GetRange(half, tileCount - half);

            rightTiles.Reverse();
            rightChanges.Reverse();

            DOTween.Complete(2); // 중복 애니메이션 방지

            StartCoroutine(TileAnimationCoroutine(leftTiles, leftChanges));
            StartCoroutine(TileAnimationCoroutine(rightTiles, rightChanges));
        }

        private IEnumerator TileAnimationCoroutine(List<Tile> targetTiles, List<TileDataSO> changes)
        {
            for (int i = 0; i < targetTiles.Count; i++)
            {
                yield return _mapChangeDelayWait;
                targetTiles[i].TileChangeAnimation(changes[i]);
            }
        }

        private bool ShouldChangeTheme(int stageIdx) => stageIdx % StageThemeChangeInterval == 0;

        private Theme GetRandomTheme()
        {
            return (Theme)Random.Range(0, (int)Theme.END);
        }
    }
}
