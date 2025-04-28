using UnityEngine;

namespace karin.ui
{
    public class EventManager : MonoSingleton<EventManager>
    {
        private IEvent _statUpEvent;
        public EventSO testEvent;

        private void Awake()
        {
            _statUpEvent = FindFirstObjectByType<StatUpEvent>();
        }

        [ContextMenu("TestStatUp")]
        public void StatUpEvent()
        {
            _statUpEvent.OpenEvent(testEvent);
        }
    }

    public interface IEvent
    {
        public void OpenEvent(EventSO eventData);
    }
} 