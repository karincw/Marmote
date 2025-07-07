using AYellowpaper.SerializedCollections;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private int _tileCount;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;

        _mapChangeDelayWait = new WaitForSeconds(_mapChangeDelay);
        _tileCount = _tiles.Count;
    }

    private void Start()
    {
        MakeMap(0);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MakeMap(0);
        }
    }
    public void MakeMap(int index)
    {
        var tileData = _mapDatas[index].GetMapData();
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
        StartCoroutine(MapChangeCoroutine(leftTiles, leftChanges, false));
        StartCoroutine(MapChangeCoroutine(rightTiles, rightChanges, true));
    }

    private IEnumerator MapChangeCoroutine(List<Tile> targetTiles, List<TileType> changes, bool isRight)
    {
        for (int i = 0; i < targetTiles.Count; i++)
        {
            yield return _mapChangeDelayWait;
            targetTiles[i].SetTileAnimation(changes[i], isRight);
        }
    }
}
