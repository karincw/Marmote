using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/karin/mapData")]
public class MapDataSO : ScriptableObject
{
    [SerializeField] private List<TilesData> _tiles;
    private static readonly int MapMaxCount = 40;

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

        return mapData.OrderBy(t => Random.value).ToList();
    }
}
