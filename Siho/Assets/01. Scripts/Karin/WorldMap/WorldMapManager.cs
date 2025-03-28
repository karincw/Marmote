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
        [HideInInspector] public int _tileCount = 0;

        [SerializeField] private int debugValue;

        private Dice _dice;
        private Floor _floor;
        private Symbol _symbol;

        private WaitForSeconds _mapChangeAnimationDelay;

        private void Awake()
        {
            _tileCount = _tiles.Count;
            _dice = FindFirstObjectByType<Dice>();
            _floor = FindFirstObjectByType<Floor>();
            _symbol = FindFirstObjectByType<Symbol>();
            _mapChangeAnimationDelay = new WaitForSeconds(0.05f);
        }

        private void OnEnable()
        {
            _floor.OnDiceStopEvent += HandleDiceStop;
            _symbol.OnEnterNextStage += HandleNextStage;
        }

        private void OnDisable()
        {
            _floor.OnDiceStopEvent -= HandleDiceStop;
            _symbol.OnEnterNextStage -= HandleNextStage;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                RollDice();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                HandleNextStage();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                _symbol.Move(debugValue);
            }

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

        private void HandleNextStage()
        {
            UpgradeWorldMap();
        }

        [ContextMenu("UpgradeWorldMap")]
        private void UpgradeWorldMap()
        {
            int halfPoint = Mathf.CeilToInt((_tileCount - 1) / 2);
            var lefts = _tiles.GetRange(0, halfPoint);
            var rights = _tiles.GetRange(halfPoint, _tileCount - halfPoint);
            rights.Reverse();
            StartCoroutine(TileAnimationCoroutine(lefts));
            StartCoroutine(TileAnimationCoroutine(rights));
        }

        private IEnumerator TileAnimationCoroutine(List<Tile> targetTiles)
        {
            foreach (var tile in targetTiles)
            {
                yield return _mapChangeAnimationDelay;
                tile.TileChangeAnimation(tile.canChange ? (TileType)Random.Range(0, 7) : tile.tileData.tileType);
            }
        }

    }
}