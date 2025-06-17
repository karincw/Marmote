using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace karin
{
    public class EventManager : MonoSingleton<EventManager>
    {
        [SerializeField] private List<EventSO> _baseEvents;
        [SerializeField, SerializedDictionary] private SerializedDictionary<Theme, SerializeEventList> _themeToEvent;
        private Queue<EventSO> _currentEvents;

        private void Awake()
        {
            SetUpEvent();
        }

        private void SetUpEvent()
        {
            List<EventSO> events = _baseEvents;
            events.AddRange(_themeToEvent[WorldMapManager.Instance.stageTheme].list);
            _currentEvents = new Queue<EventSO>(Utils.ShuffleList(events));
        }

        public void PlayEvent()
        {
            if (_currentEvents.Count <= 0) return;
            EventSO evt = _currentEvents.Dequeue();
        }
    }
}