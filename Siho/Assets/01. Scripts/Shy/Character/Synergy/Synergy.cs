using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shy.Pooling
{
    public class Synergy : Pool, IPointerClickHandler
    {
        public SynergySO so;
        private int value = 0;
        private Team userTeam;

        private Image icon;
        private TextMeshProUGUI numTmp;

        private void Awake()
        {
            icon = GetComponent<Image>();
            numTmp = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void Init(KeyValuePair<SynergyType, int> _data, Team _team)
        {
            Init();
            
            so = SOManager.Instance.GetSO(_data.Key);
            userTeam = _team;
            value = _data.Value;

            icon.sprite = so.icon;
            numTmp.gameObject.SetActive(so.showNum);

            UpdateValue();
        }

        public void UpdateValue()
        {
            numTmp.text = value.ToString();
        }

        public void UseSynergy()
        {
            foreach (var _event in so.synergies)
            {
                Target _targetEnum = _event.target;
                Team _targetTeam = Team.All;

                switch (_targetEnum)
                {
                    case Target.Self:
                        _targetTeam = userTeam;
                        break;
                    case Target.Opponent:
                        _targetTeam = userTeam == Team.Player ? Team.Enemy : Team.Player;
                        break;
                }

                var targets = BattleManager.Instance.GetCharacters(_targetTeam);

                switch (_event)
                {
                    case StatEventSO _statEvent:
                        foreach (var item in targets)
                        {
                            item.AddStat(_statEvent.GetData(value), _statEvent.calculate, _statEvent.subStat);
                        }
                        break;

                    case SpecialEventSO _specialEvent:
                        foreach (var item in targets)
                        {
                            item.ChangeCharacteristic(_specialEvent.characteristic);
                        }
                        break;
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SynergyTooltipManager.Instance.Use(this);
        }
    }
}
