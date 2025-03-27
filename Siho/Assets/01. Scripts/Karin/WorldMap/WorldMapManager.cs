using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace karin
{
    public class WorldMapManager : MonoSingleton<WorldMapManager>
    {
        [SerializeField] private List<Tile> _tiles;
        [HideInInspector] public int _tileCount = 0;

        private void Awake()
        {
            _tileCount = _tiles.Count;
        }

        public List<Tile> GetTiles(int index, int count)
        {
            return _tiles.GetRange(index, count);
        }

    }
}