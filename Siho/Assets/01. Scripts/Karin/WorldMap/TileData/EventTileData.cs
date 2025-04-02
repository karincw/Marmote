using UnityEngine;

namespace karin.worldmap
{
    [CreateAssetMenu(menuName = "SO/karin/tiles/EventTileDataSO")]
    public class EventTileDataSO : TileDataSO
    {

        public override void Play()
        {
            Debug.Log($"Event");
        }
    }
}