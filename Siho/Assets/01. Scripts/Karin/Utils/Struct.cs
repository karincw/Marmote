
using System.Collections.Generic;
using UnityEngine;

public struct TilesData
{
    [SerializeField] private int count;
    [SerializeField] private TileType tileType;

    public List<TileType> GetTiles()
    {
        List<TileType> tiles = new List<TileType>();
        for (int i = 0; i < count; i++)
            tiles.Add(tileType);
        return tiles;
    }
}