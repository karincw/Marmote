using Shy.Pooling;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Shy.Event
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance;

        [Header("Text Event Variables")]
        [SerializeField] private CanvasGroup eventPanel;
        [SerializeField] private TextMeshProUGUI textEventTmp;
        [SerializeField] private EventSelector[] selectors;
        [SerializeField] private Transform alertPos;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            eventPanel.gameObject.SetActive(false);
        }

        #region Text Event
        public void InitEvent(EventSO _eventSO)
        {
            SelectorsVisible(false);

            textEventTmp.text = "";
            eventPanel.gameObject.SetActive(true);
            eventPanel.alpha = 0;

            for (int i = 0; i < selectors.Length; i++)
            {
                if(_eventSO.events.Length <= i)
                {
                    selectors[i].Init(new EventData(), false);
                }
                else
                {
                    selectors[i].Init(_eventSO.events[i], EventAbleChecker.AbleCheck(_eventSO.events[i].condition));
                }
            }

            SequnceTool.Instance.FadeInCanvasGroup(eventPanel, 0.5f, () =>
            {
                StartCoroutine(SetMessageDelay(_eventSO.explain, () => SelectorsVisible(true)));
            });
        }

        internal void TEventAlert(string _mes)
        {
            var _alert = PoolingManager.Instance.Pop(PoolType.Alert) as Alert;
            _alert.transform.SetParent(alertPos);
            _alert.ShowMessage(_mes);
        }

        private void SelectorsVisible(bool show)
        {
            foreach (var _selector in selectors)
            {
                _selector.gameObject.SetActive(show ? _selector.isUse : false);
            }
        }

        public void OnEvent(EventData _eventData)
        {
            var result = _eventData.GetResult().resultSO;

            if (result is Result_Item _item)
            {
                if (_item.item is Item_Stat _stat)
                {
                    PlayerManager.Instance.AddStat(_stat.statType, _item.value);
                }
                else if (_item.item is Item_Synergy _synergy)
                {
                    PlayerManager.Instance.AddSynergy(_synergy.item.synergyType, _item.value);
                }
                else if (_item.item is Item_Money _money)
                {
                }
            }
            else if (result is Result_Battle)
            {

            }

            SelectorsVisible(false);
            StartCoroutine(SetMessageDelay(result.message, () => StartCoroutine(EndEvent())));
        }

        private IEnumerator SetMessageDelay(string _mes, UnityAction _endAction)
        {
            string _message = "";
            textEventTmp.text = _message;

            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < _mes.Length; i++)
            {
                _message += _mes[i];
                textEventTmp.text = _message;
                yield return new WaitForSeconds(0.05f);
            }

            _endAction?.Invoke();
        }

        private IEnumerator EndEvent()
        {
            yield return new WaitForSeconds(2.5f);
            eventPanel.gameObject.SetActive(false);
        }
        #endregion
    }
}
