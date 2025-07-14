using Shy.Event;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/karin/TileData/Event")]
public class EventTileDataSO : TileDataSO
{
    public EventSO eventType;

    public override void EnterEvent() 
    {
        EventManager.Instance.InitEvent(eventType);
    }
}
