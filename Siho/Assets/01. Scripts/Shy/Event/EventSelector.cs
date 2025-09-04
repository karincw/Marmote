using TMPro;
using Shy.Pooling;
using UnityEngine;

namespace Shy.Event
{
    public class EventSelector : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI actionTmp, conditionTmp;
        private EventData currentEvent;
        public bool isUse { internal get; set; }
        private bool isAble;


        public void Init(EventData _event, bool _able)
        {
            currentEvent = _event;
            actionTmp.text = _event.eventExplain;

            var _condition = _event.condition;
            string _cMes = "";

            if(_condition.conditionType != ConditionType.None)
            {
                _cMes = $"{_condition.item.GetName()} {_condition.value} ";

                switch (_condition.conditionType)
                {
                    case ConditionType.More:
                        _cMes += "이상 ";
                        break;

                    case ConditionType.Below:
                        _cMes += "이하 ";
                        break;
                }

                _cMes += "필요";
            }

            conditionTmp.color = _able ? Color.green : Color.red;
            conditionTmp.SetText(_cMes);

            isUse = (_event.eventExplain != null);
            isAble = _able;
        }

        public void Click()
        {
            if (isAble)
            {
                TextEventManager.Instance.OnEvent(currentEvent);
            }
            else
            {
                TextEventManager.Instance.TextEventAlert("불가능한 것 같다.");
            }
        }
    }
}
