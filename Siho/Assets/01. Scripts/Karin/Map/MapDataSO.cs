using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/karin/mapData")]
public class MapDataSO : ScriptableObject
{
	[SerializeField] private List<TilesData> _tiles;

	public List<TileType> GetMapData()
	{
		List<TileType> mapData = new List<TileType>();
		foreach (var data in _tiles)
		{
			mapData.AddRange(data.GetTiles());
		}
		return mapData.OrderBy(t => Random.value).ToList();
	}
}
