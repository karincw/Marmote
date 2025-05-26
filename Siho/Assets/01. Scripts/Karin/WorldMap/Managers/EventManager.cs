using AYellowpaper.SerializedCollections;
using karin.worldmap;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace karin.ui
{
    public class EventManager : MonoSingleton<EventManager>
    {
        private IEvent _statUpEvent;
        private Queue<EventSO> _events;

        [SerializeField] private List<EventSO> _baseEventList;
        [Space(5), SerializeField, SerializedDictionary("Theme", "ThemeEvent")]
        private SerializedDictionary<Theme, List<EventSO>> _stageToEventDictionary;

        private void Awake()
        {
            _statUpEvent = FindFirstObjectByType<StatUpEvent>();
            WorldMapManager.Instance.OnEnterNextStage += HandleStageChange;
            _events = new Queue<EventSO>();
            HandleStageChange(0);
        }

        private void OnDestroy()
        {
            WorldMapManager.Instance.OnEnterNextStage -= HandleStageChange;
        }

        private void HandleStageChange(int index)
        {
            _events.Clear();
            List<EventSO> eventList = new List<EventSO>();
            eventList.AddRange(_baseEventList);
            eventList.AddRange(_stageToEventDictionary[WorldMapManager.Instance.stageTheme]);
            eventList.OrderBy(e => Random.value);
            eventList.ForEach(t => _events.Enqueue(t));

            Debug.Log($"events : {_events.Count}");
        }

        [ContextMenu("TestStatUp")]
        public void StatUpEvent()
        {
            _statUpEvent.OpenEvent(_events.Dequeue());
        }
    }

    public interface IEvent
    {
        public void OpenEvent(EventSO eventData);
    }
}