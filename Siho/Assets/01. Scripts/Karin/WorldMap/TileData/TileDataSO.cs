using UnityEngine;

namespace karin.worldmap
{
    public abstract class TileDataSO : ScriptableObject
    {
        public TileType tileType;
        [ColorUsage(true)] public Color tileColor;
        public Texture2D iconTexture;

        public abstract void Play();
    }
}