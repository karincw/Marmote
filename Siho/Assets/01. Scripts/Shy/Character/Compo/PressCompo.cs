using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.Events;

namespace Shy.Info
{
    public class PressCompo : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [HideInInspector] public bool canPress = true, pressing = false;
        private bool openInfo;
        
        public InfoType pressType;
        private float pressStartTime;

        private InfoType infoType;
        private UnityAction pressEvent;

        public void Init(InfoType _infoType, UnityAction _pressEvent)
        {
            infoType = _infoType;
            pressEvent = _pressEvent;
        }

        private void Update()
        {
            if (pressing && !openInfo)
            {
                if (Time.time - pressStartTime >= 1) LongPress();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("1");
            if (!canPress) return;

            pressing = true;
            pressStartTime = Time.time;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!canPress) return;

            ExitPress();
        }

        public void LongPress()
        {
            openInfo = true;
            pressEvent?.Invoke();
        }

        public void ExitPress()
        {
            if (pressing)
            {
                InfoManager.Instance.ClosePanel(infoType);
                pressing = openInfo = false;
            }
        }
    }
}