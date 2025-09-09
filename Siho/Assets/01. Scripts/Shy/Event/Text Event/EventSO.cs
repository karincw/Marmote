using UnityEngine;

namespace Shy.Event
{
    [CreateAssetMenu(menuName = "SO/Shy/EventData")]
    public class EventSO : ScriptableObject
    {
        [TextArea()]
        public string explain;

        public EventData[] events = new EventData[3];
    }
}