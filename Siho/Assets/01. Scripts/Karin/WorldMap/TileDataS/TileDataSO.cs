using UnityEngine;

namespace karin.worldmap
{
    public abstract class TileDataSO : ScriptableObject
    {
        public TileType tileType;
        [ColorUsage(true)]public Color    tileColor;

        public abstract void Play();
    }
}