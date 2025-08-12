using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Shy.Event
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance;

        private UnityEvent<int> currentEvent;

        [Header("Text Event Variables")]
        [SerializeField] private CanvasGroup eventPanel;
        [SerializeField] private TextMeshProUGUI textEventTmp;
        [SerializeField] private EventSelector[] selectors;

        [Header("Battle Event Variables")]
        [SerializeField] private RollingDice dice;
        [SerializeField] private BattleEventUi[] bEventUis = new BattleEventUi[3];
        [SerializeField] private TextMeshProUGUI bEventMess;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            eventPanel.gameObject.SetActive(false);

            UnityEvent<BattleEvent, int> _uBE = new();
            UnityEvent<int> _diceEvent = new();

            _uBE.AddListener((BattleEvent _e, int _i) => HideEventUis());
            _uBE.AddListener((BattleEvent _e, int _i) =>
            {
                _diceEvent.AddListener((int _v) => 
                GameMakeTool.Instance.Delay(()=>
                BattleManager.Instance.UserBattleEvent(_e, _i, _v), 1.5f));
                DiceRoll(_diceEvent);
            });

            foreach (var _event in bEventUis) _event.clickEvent = _uBE;
        }

        #region Dice
        private void DiceRoll(UnityEvent<int> _event)
        {
            currentEvent = _event;
            dice.gameObject.SetActive(true);

            dice.Roll();
        }

        public void ReturnDiceResult(int _n) => currentEvent?.Invoke(_n);

        private void HideDice() => dice.gameObject.SetActive(false);
        #endregion

        #region EventUi
        public void SetBattleEvent()
        {
            HideDice();

            foreach (var _ui in bEventUis)
            {
                _ui.SetPercent(BattleManager.Instance.GetCharacters());
                _ui.gameObject.SetActive(true);
            }
        }

        private void HideEventUis()
        {
            foreach (var _ui in bEventUis) _ui.gameObject.SetActive(false);
        }

        public void HideAllPanel()
        {
            HideDice();
            HideEventUis();
            HideMessage();
        }

        public void HideMessage() => bEventMess.gameObject.SetActive(false);

        internal void ShowMessage(string _v)
        {
            HideAllPanel();

            bEventMess.SetText(_v);
            bEventMess.gameObject.SetActive(true);
        }
        #endregion

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
                    selectors[i].Init(new EventData());
                }
                else
                {
                    selectors[i].Init(_eventSO.events[i]);
                }
            }

            GameMakeTool.Instance.DOFadeCanvasGroup(eventPanel, 0.5f, () =>
            {
                StartCoroutine(SetMessageDelay(_eventSO.explain, () => SelectorsVisible(true)));
            });
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

            if (result is StatResultSO _statResult)
            {
                PlayerManager.Instance.AddStat(_statResult.mainStat, _statResult.value);
            }
            else if (result is SynergyResultSO _synergyResult)
            {
                Data.GameData.playerData.synergies.Add(_synergyResult.so);
            }
            else if (result is BattleResultSO)
            {

            }

            SelectorsVisible(false);
            StartCoroutine(SetMessageDelay(result.message, () => StartCoroutine(EndEvent())));
        }

        private IEnumerator SetMessageDelay(string _mes, UnityAction _endAction)
        {
            string _message = "";
            textEventTmp.text = _message;

            yield return new WaitForSeconds(1);

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
            yield return new WaitForSeconds(3.5f);
            eventPanel.gameObject.SetActive(false);
        }
        #endregion
    }
}
