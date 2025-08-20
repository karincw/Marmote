using AYellowpaper.SerializedCollections;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    [Header("Animation-Settings")]
    [SerializeField] private float _mapChangeDelay;
    private WaitForSeconds _mapChangeDelayWait;

    [Header("Pre-Settings")]
    [SerializeField] private List<Tile> _tiles = new();
    [SerializeField] private List<MapDataSO> _mapDatas = new();
    [SerializedDictionary] public SerializedDictionary<TileType, TileDataSO> TypeToDataDictionary;

    [Header("MapData")]
    [SerializeField] private int _currentMapIndex = 0;

    public Money money;

    private int _tileCount;
    public Symbol symbol;
    private int _mapIndex;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;

        symbol = FindFirstObjectByType<Symbol>();
        money = FindFirstObjectByType<Money>();
        _mapChangeDelayWait = new WaitForSeconds(_mapChangeDelay);
        _tileCount = _tiles.Count;
        Symbol.OnMoveEndEvent += HandleMoveEnd;
        Symbol.OnMoveEndEvent += SetTileNumbers;
    }

    private void Start()
    {
        _mapIndex = 0;
        _mapDatas = DataLinkManager.instance.stage.stageData.mapDatas;
        MakeMap();
        SetTileNumbers(0);
    }

    private void OnDestroy()
    {
        Symbol.OnMoveEndEvent -= HandleMoveEnd;
        Symbol.OnMoveEndEvent -= SetTileNumbers;
    }

    public void HandleMoveEnd(int index)
    {
        index %= 40;
        _tiles[index].OnEnterEvent?.Invoke();
    }

    public List<Tile> GetMoveMap(int now, int count)
    {
        now %= 40;
        if (_tileCount < now + count)
        {
            var split = _tileCount - now;
            var result = _tiles.GetRange(now, split);
            result.AddRange(_tiles.GetRange(0, count - split));
            return result;
        }
        return _tiles.GetRange(now, count);
    }

    public void SetTileNumbers(int index)
    {
        index %= 40;
        if (index + 12 > _tileCount)
        {
            int lastI = 0;
            for (int i = -index; i < _tileCount - index; i++)
            {
                _tiles[i + index].SetTileNumber(i);
                lastI = i;
            }
            for (int i = 0; i <= index + 12 - _tileCount; i++)
            {
                _tiles[i].SetTileNumber(++lastI);
            }
            return;
        }
        for (int i = -index; i < _tileCount - index; i++)
        {
            _tiles[i + index].SetTileNumber(i);
        }
    }

    public void MakeMap()
    {
        CameraControler.instance.ZoomOut();
        var tileData = _mapDatas[_mapIndex++].GetMapData();
        MapChangeAnimation(tileData);
    }

    private void MapChangeAnimation(List<TileType> datas)
    {
        int half = Mathf.CeilToInt(_tileCount / 2);
        var leftTiles = _tiles.GetRange(0, half);
        var rightTiles = _tiles.GetRange(half, _tileCount - half);

        var leftChanges = datas.GetRange(0, half);
        var rightChanges = datas.GetRange(half, _tileCount - half);

        rightTiles.Reverse();
        rightChanges.Reverse();

        DOTween.Complete(2);
        StartCoroutine(MapChangeCoroutine(leftTiles, leftChanges));
        StartCoroutine(MapChangeCoroutine(rightTiles, rightChanges, true));
    }

    private IEnumerator MapChangeCoroutine(List<Tile> targetTiles, List<TileType> changes, bool isEnd = false)
    {
        for (int i = 0; i < targetTiles.Count; i++)
        {
            yield return _mapChangeDelayWait;
            targetTiles[i].SetTileAnimation(changes[i]);
        }
        if (isEnd && symbol.sequence != null)
        {

            yield return new WaitForSeconds(1f);
            CameraControler.instance.ZoomIn();
            yield return new WaitForSeconds(0.3f);
            symbol.sequence.Play();
        }
    }
}
