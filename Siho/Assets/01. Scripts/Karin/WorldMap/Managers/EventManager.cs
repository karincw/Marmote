using karin.worldmap;
using Shy;
using System.Collections.Generic;
using UnityEngine;

namespace karin.ui
{
    public class EventManager : MonoSingleton<EventManager>
    {
        private IEvent _statUpEvent;
        private Queue<EventSO> _events;

        [SerializeField] private List<EventSO> _baseEventList;
        [SerializeField] private List<DataStruct<EventSO>> _stageToEventList;

        private void Awake()
        {
            _statUpEvent = FindFirstObjectByType<StatUpEvent>();
            WorldMapManager.Instance.OnEnterNextStage += HandleStageChange;
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
            eventList.AddRange(_stageToEventList[index].list);
            Utils.ShuffleList<EventSO>(eventList);
            eventList.ForEach( t => _events.Enqueue(t));
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