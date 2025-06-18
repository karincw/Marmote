using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace karin
{
    public class EventManager : MonoSingleton<EventManager>
    {
        [SerializeField] private List<EventSO> _alwaysEvent;
        [SerializeField, SerializedDictionary] private SerializedDictionary<Theme, SerializeEventList> _themeToEvent;
        private Queue<EventSO> _currentEvents;

        private EventPanel _eventPanel;

        private void Awake()
        {
            _eventPanel = FindFirstObjectByType<EventPanel>();
            SetUpEvent();
        }

        private void SetUpEvent()
        {
            List<EventSO> events = _alwaysEvent.ToList();
            events.AddRange(_themeToEvent[WorldMapManager.Instance.stageTheme].list);
            _currentEvents = new Queue<EventSO>(Utils.ShuffleList(events));
        }

        [ContextMenu("PlayEvent")]
        public void PlayEvent()
        {
            if (_currentEvents.Count <= 0)
            {
                Debug.LogWarning("더이상 이벤트가 없음");
                return;
            }
            EventSO evt = _currentEvents.Dequeue();
            _eventPanel.SetEvent(evt);
            _eventPanel.Open();
        }

        public void SendFeedback(string message)
        {
            _eventPanel.SetFeedback(message);
        }
    }
}