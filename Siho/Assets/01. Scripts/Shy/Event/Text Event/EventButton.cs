using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shy.Event
{
    public class EventButton : Button
    {
        [Header("Event")]
        [SerializeField] private Transform textBox;
        [SerializeField] private TextMeshProUGUI actionTmp, conditionTmp;

        private EventData currentEvent;
        private UnityAction sucEvent, failEvent;

        private bool isAbleButton = false;

        public void Init(UnityAction<EventData> _sucEvent, UnityAction<string> _failEvent)
        {
            sucEvent = () => _sucEvent?.Invoke(currentEvent);
            failEvent = () => _failEvent?.Invoke("불가능한 것 같다.");

            onClickEvent = () => useable = false;
            onClickEvent += Play;
        }

        private void Play() => (isAbleButton ? sucEvent : failEvent)?.Invoke();

        public void IniteEvent(EventData _event, bool _able)
        {
            targetGraphic.sprite = clickSprite;

            currentEvent = _event;
            actionTmp.text = _event.eventExplain;

            var _condition = _event.condition;
            var _conditionType = _condition.conditionType;
            string _cMes = "";

            if (_condition.conditionType != ConditionType.None)
            {
                _cMes = $"{_condition.item.GetName()} {_condition.value} ";

                if (_conditionType == ConditionType.More) _cMes += "이상";
                else if (_conditionType == ConditionType.Below) _cMes += "이하";

                _cMes += "필요";
            }

            conditionTmp.color = _able ? Color.green : Color.red;
            conditionTmp.SetText(_cMes);

            isAbleButton = _able;
        }

        public void CheckAble()
        {
            if (!isAbleButton) return;

            targetGraphic.sprite = defaultSprite;
            targetGraphic.color = Color.white;

            textBox.localPosition -= new Vector3(0, 10, 0);

            useable = true;
        }

        public void OffButton()
        {
            targetGraphic.sprite = clickSprite;
            targetGraphic.color = pressedColor;

            textBox.localPosition += new Vector3(0, 10, 0);
            useable = false;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            if (!useable) return;
            Debug.Log("Click Event");
            base.OnPointerClick(eventData);
        }
    }
}
