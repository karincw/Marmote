using UnityEngine;

namespace karin
{
    [CreateAssetMenu(menuName = "SO/karin/tiles/TileDataSO")]
    public class TileDataSO : ScriptableObject
    {
        public TileType tileType;
        [ColorUsage(true)] public Color tileColor;
        public Texture2D iconTexture;

        public virtual void Play()
        {
            switch (tileType)
            {
                case TileType.None:
                    break;
                default:
                    Debug.LogError("asd");
                    break;
            }
        }
    }
}