using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    [Header("Pre-Settings")]
    [SerializeField] private List<Tile> _tiles = new();
    [SerializeField] private List<MapDataSO> _mapDatas = new();
    [SerializedDictionary] private SerializedDictionary<TileType, TileDataSO> _typeToDataDictionary;

    [Header("MapData")]
    [SerializeField] private int _currentMapIndex = 0;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }

    private void Start()
    {
        MakeMap();
    }

    public void MakeMap()
    {
        var tileData = _mapDatas[0].GetMapData();
        var datas = tileData.Select(t => _typeToDataDictionary[t]).ToList();
    }

    public void AddMap()
    {

    }

}
