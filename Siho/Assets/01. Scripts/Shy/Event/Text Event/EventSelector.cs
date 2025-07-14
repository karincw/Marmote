using TMPro;
using UnityEngine;

namespace Shy.Event
{
    public class EventSelector : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI tmp;
        private EventData currentEvent;
        public bool isUse { internal get; set; }


        public void Init(EventData _event)
        {
            currentEvent = _event;
            tmp.text = _event.eventExplain;

            isUse = (_event.eventExplain != null);
        }

        public void Click()
        {
            EventManager.Instance.OnEvent(currentEvent);
        }
    }
}
