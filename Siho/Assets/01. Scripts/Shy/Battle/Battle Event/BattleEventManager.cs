using UnityEngine;
using Shy.Event;
using TMPro;
using UnityEngine.Events;
using Shy.Pooling;

namespace Shy
{
    public class BattleEventManager : MonoBehaviour
    {
        public static BattleEventManager Instance;
        public BattleEventMode eventMode { private get; set; }

        [Header("Dice")]
        [SerializeField] private EventDice dice;


        [Header("Event")]
        [SerializeField] private BattleEventButton[] eventUiButtons = new BattleEventButton[3];

        private UnityAction beginBattle, endBattle;
        private BattleEventButton currentEvent;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI eventMessage;

        [Header("Tooltip")]
        [SerializeField] private GameObject tooltipBox;
        [SerializeField] private TextMeshProUGUI synergyName, synergyExplain, synergyResult;
        private Synergy currentSynergy;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        #region Uis
        public void HideAllUis()
        {
            foreach (var _ui in eventUiButtons) _ui.gameObject.SetActive(false);
            dice.gameObject.SetActive(false);
            eventMessage.gameObject.SetActive(false);
            CloseTooltip();
        }
        #endregion

        #region Dice
        private void DiceRoll()
        {
            dice.gameObject.SetActive(true);
            dice.Roll();
        }
        #endregion

        #region Tooltip
        public void OpenTooltip(Synergy _synergy)
        {
            if (eventMode != BattleEventMode.None && eventMode != BattleEventMode.Event) return;

            if (currentSynergy != null) return;
            currentSynergy = _synergy;

            HideAllUis();

            synergyName.text = _synergy.so.synergyName;
            synergyExplain.text = _synergy.so.explain;

            var _value = _synergy.GetDataValue();
            synergyResult.text = $"ÇöÀç °ª : {_value}";
            synergyResult.gameObject.SetActive(_value != "");

            tooltipBox.SetActive(true);
        }

        private void CloseTooltip()
        {
            currentSynergy = null;
            tooltipBox.SetActive(false);
        }

        public void CloseTooltip(Synergy _synergy)
        {
            if (eventMode == BattleEventMode.Tooltip)
            {
                if (currentSynergy != _synergy) return;
            }

            CloseTooltip();

            if(eventMode == BattleEventMode.Event)
                foreach (var _bt in eventUiButtons) _bt.gameObject.SetActive(true);
        }
        #endregion

        #region Events
        public void InitEvent(UnityAction _beginBattle, UnityAction _endBattle)
        {
            beginBattle = _beginBattle;
            endBattle = _endBattle;

            dice.diceFinEvent = (int _n) => SequnceTool.Instance.Delay(() => OnBattleEvent(_n), 0.6f);

            foreach (var _event in eventUiButtons)
            {
                _event.clickEvent = CurrentEvent;
            }
        }

        public void BeginEvent()
        {
            eventMode = BattleEventMode.Event;

            foreach (var _bt in eventUiButtons)
            {
                _bt.SetPercent(BattleManager.Instance.GetCharacters());
            }
        }

        private void CurrentEvent(BattleEventButton _bt)
        {
            eventMode = BattleEventMode.Text;

            HideAllUis();

            currentEvent = _bt;
            DiceRoll();
        }

        private void OnBattleEvent(int _n)
        {
            var so = SOManager.Instance.GetSO(currentEvent.eventType);

            if(currentEvent.per < _n)
            {
                ShowTextEvent(so.failMes);
                SequnceTool.Instance.Delay(beginBattle, 1.5f);
                return;
            }

            ShowTextEvent(so.successMes);

            switch (currentEvent.eventType)
            {
                case BattleEventType.Run:
                    SequnceTool.Instance.Delay(endBattle, 0.5f);
                    break;

                case BattleEventType.Surprise:
                    SequnceTool.Instance.Delay(() => BattleManager.Instance.SurpriseAttack(Team.Player), 2f);
                    break;

                case BattleEventType.Talk:
                    BattleManager.Instance.GetCharacter(Team.Enemy).AddSynergy(SynergyType.Panic);
                    SequnceTool.Instance.Delay(beginBattle, 1.5f);
                    break;
            }
        }

        private void ShowTextEvent(string _v)
        {
            HideAllUis();

            eventMessage.SetText(_v);
            eventMessage.gameObject.SetActive(true);
        }
        #endregion
    }
}