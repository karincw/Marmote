using UnityEngine;

[CreateAssetMenu(menuName = "SO/karin/TileData/Event")]
public class EventTileDataSO : TileDataSO
{
    public EventType eventType;

    public override void EnterEvent() 
    {
        switch (eventType)
        {
            case EventType.PinBall:
                break;
        }
    }
}
