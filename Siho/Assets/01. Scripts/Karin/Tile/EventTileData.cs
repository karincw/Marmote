using UnityEngine;

namespace karin
{
    [CreateAssetMenu(menuName = "SO/karin/tiles/EventTileDataSO")]
    public class EventTileDataSO : TileDataSO
    {

        public override void Play()
        {
            if (tileType == TileType.ChangeStage)
            {
                WorldMapManager.Instance.MoveNextStage();
            }
            else if (tileType == TileType.Event)
            {
                EventManager.Instance.PlayEvent();
            }
        }
    }
}