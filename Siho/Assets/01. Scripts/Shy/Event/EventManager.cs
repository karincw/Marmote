using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Shy.Event
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance;

        private UnityEvent<int> currentEvent;

        [SerializeField] private GameObject eventPanel;

        [Tooltip("Battle Event Variables")]
        [SerializeField] private RollingDice dice;
        [SerializeField] private BattleEventUi[] eventUis = new BattleEventUi[3];
        [SerializeField] private TextMeshProUGUI message;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            UnityEvent<BattleEvent, int> _uBE = new();
            _uBE.AddListener(BattleManager.Instance.UserBattleEvent);
            _uBE.AddListener((BattleEvent _e, int _i) => HideEventUis());

            foreach (var _event in eventUis) _event.clickEvent = _uBE;
        }

        #region Battle
        #region Dice
        public void DiceRoll(UnityEvent<int> _event)
        {
            currentEvent = _event;
            dice.gameObject.SetActive(true);
            dice.Roll();
        }

        public void ReturnDiceResult(int _n)
        {
            currentEvent?.Invoke(_n);
        }

        public void HideDice()
        {
            dice.gameObject.SetActive(false);
        }
        #endregion

        #region EventUi
        public void SetBattleEvent(int _dValue)
        {
            HideDice();

            foreach (var _ui in eventUis)
            {
                _ui.SetPercent(BattleManager.Instance.GetCharacters(), _dValue);
                _ui.gameObject.SetActive(true);
            }
        }

        private void HideEventUis()
        {
            foreach (var _ui in eventUis) _ui.gameObject.SetActive(false);
        }
        #endregion

        public void HideAllPanel()
        {
            HideDice();
            HideEventUis();
            HideMessage();
        }

        public void HideMessage() => message.gameObject.SetActive(false);

        internal void ShowMessage(string _v)
        {
            HideAllPanel();

            message.SetText(_v);
            message.gameObject.SetActive(true);
        }
        #endregion

        #region Event

        #endregion
    }
}
