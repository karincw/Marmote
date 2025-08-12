using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/karin/mapData")]
public class MapDataSO : ScriptableObject
{
    [SerializeField] private List<TilesData> _tiles;
    private static readonly int MapMaxCount = 36;
    public bool isEnd;

    public List<TileType> GetMapData()
    {
        List<TileType> mapData = new List<TileType>();
        foreach (var data in _tiles)
        {
            mapData.AddRange(data.GetTiles());
        }
        while (mapData.Count < MapMaxCount)
        {
            mapData.Add(TileType.None);
        }
        mapData = mapData.OrderBy(t => Random.value).ToList();
        if (isEnd)
            mapData.Insert(0, TileType.End);
        else
            mapData.Insert(0, TileType.NextStage);
        mapData.Insert(10, TileType.Event1);
        mapData.Insert(20, TileType.Shop);
        mapData.Insert(30, TileType.Event1);

        return mapData;
    }
}
