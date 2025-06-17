using karin.ui;
using UnityEngine;

namespace karin.worldmap
{
    [CreateAssetMenu(menuName = "SO/karin/tiles/EventTileDataSO")]
    public class EventTileDataSO : TileDataSO
    {

        public override void Play()
        {
            if (tileType == TileType.ChangeStage)
            {
                WorldMapManager.Instance.SetNextStage();
            }
            else if (tileType == TileType.Event)
            {
                EventManager.Instance.OpenEvent();
            }
        }
    }
}