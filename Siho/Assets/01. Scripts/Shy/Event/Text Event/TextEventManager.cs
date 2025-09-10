using Shy.Pooling;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Shy.Event
{
    public class TextEventManager : MonoBehaviour
    {
        [Header("Text Event Variables")]
        [SerializeField] private CanvasGroup eventPanel;
        [SerializeField] private TextMeshProUGUI textEventTmp, resultTmp;
        [SerializeField] private Color goodColor, badColor;
        [SerializeField] private EventButton[] buttons;
        [SerializeField] private Transform alertPos;

        private void Start()
        {
            eventPanel.gameObject.SetActive(false);

            foreach (var item in buttons) item.Init(OnEvent, AlertEvent);
        }

        public void Init(EventSO _eventSO)
        {
            textEventTmp.text = "";
            eventPanel.gameObject.SetActive(true);
            eventPanel.alpha = 0;
            resultTmp.SetText("");

            for (int i = 0; i < buttons.Length; i++)
            {
                if(_eventSO.events.Length <= i)
                    buttons[i].IniteEvent(new EventData(), false);
                else
                    buttons[i].IniteEvent(_eventSO.events[i], EventAbleChecker.AbleCheck(_eventSO.events[i].condition));
            }

            SequnceTool.Instance.FadeInCanvasGroup(eventPanel, 0.5f, () =>
            {
                StartCoroutine(SetMessageDelay(_eventSO.explain, () =>
                {
                    foreach (var item in buttons)
                        item.CheckAble();
                }, textEventTmp));
            });
        }

        internal void AlertEvent(string _mes)
        {
            var _alert = PoolingManager.Instance.Pop(PoolType.Alert) as Alert;
            _alert.transform.SetParent(alertPos);
            _alert.ShowMessage(_mes);
        }

        public void OnEvent(EventData _eventData)
        {
            foreach (var item in buttons) item.OffButton();

            var _result = _eventData.GetResult().resultSO;
            string _message = _result.message, _resultMes = "";

            if (_result is Result_Item _item)
            {
                if (_item.item is Item_Stat _stat)
                {
                    bool _minus = _item.calc == Calculate.Minus;
                    resultTmp.color = _minus ? badColor : goodColor;

                    _resultMes = $" {(_minus ? "-" : "+")} {_stat.GetName()} {_item.value}";

                    PlayerManager.Instance.ChangeStat(_stat.statType, _item.value, _item.calc);
                }
                else if (_item.item is Item_Synergy _synergy)
                {
                    resultTmp.color = _synergy.badSynergy ? badColor : goodColor;

                    _resultMes = $"{_synergy.GetName()} {_item.value} È¹µæ";

                    PlayerManager.Instance.AddSynergy(_synergy.item.synergyType, _item.value);
                }
                else if (_item.item is Item_Money _money)
                {
                    bool _minus = _item.value < 0;
                    resultTmp.color = _minus ? badColor : goodColor;

                    _resultMes = $"{(_minus ? "-" : "+")} {_money.GetName()} {Mathf.Abs(_item.value)}";

                    MapManager.instance.money.Value += _item.value;
                }
            }

            StartCoroutine(SetMessageDelay(_message, () => 
            StartCoroutine(SetMessageDelay(_resultMes, () => 
            StartCoroutine(EndEvent()), resultTmp)), textEventTmp));
        }

        private IEnumerator SetMessageDelay(string _mes, UnityAction _endAction, TextMeshProUGUI _tmp)
        {
            string _message = "";
            _tmp.text = _message;

            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < _mes.Length; i++)
            {
                _message += _mes[i];
                _tmp.text = _message;
                yield return new WaitForSeconds(0.05f);
            }

            _endAction?.Invoke();
        }

        private IEnumerator EndEvent()
        {
            yield return new WaitForSeconds(2.5f);
            eventPanel.gameObject.SetActive(false);
        }
    }
}
