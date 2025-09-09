using Shy.Event;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/karin/TileData/Event")]
public class EventTileDataSO : TileDataSO
{
    public EventSO eventSO;
    public EventType eventType;

    public override void EnterEvent()
    {
        switch (eventType)
        {
            case EventType.Text:
                EventManager.Instance.TextEventInit(eventSO);
                break;
            case EventType.Ladder:
                EventManager.Instance.LadderGameInit();
                break;
            case EventType.BlackJack:
                EventManager.Instance.BlackJackInit();
                break;
            default:
                break;
        }
    }

    public enum EventType
    {
        Text,
        Ladder,
        BlackJack
    }
}

