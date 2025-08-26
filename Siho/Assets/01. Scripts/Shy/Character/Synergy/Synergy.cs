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
                Team _opponentTeam = userTeam == Team.Player ? Team.Enemy : Team.Player;
                Target _targetEnum = _event.target;

                if (_event is StatEventSO _statEvent)
                {
                    if (_targetEnum != Target.Self)
                    {
                        var _target = BattleManager.Instance.GetCharacter(_opponentTeam);
                        _target.AddStat(_statEvent.GetData(value), _statEvent.calculate, _statEvent.subStat);
                    }

                    if (_targetEnum != Target.Opponent)
                    {
                        var _target = BattleManager.Instance.GetCharacter(userTeam);
                        _target.AddStat(_statEvent.GetData(value), _statEvent.calculate, _statEvent.subStat);
                    }
                }
                else if (_event is SpecialEventSO _specialEvent)
                {
                    if (_specialEvent.GetData(value) == false) return;

                    if (_targetEnum != Target.Self)
                    {
                        var _target = BattleManager.Instance.GetCharacter(_opponentTeam);
                        _target.ChangeCharacteristic(_specialEvent.characteristic);
                    }

                    if (_targetEnum != Target.Opponent)
                    {
                        var _target = BattleManager.Instance.GetCharacter(userTeam);
                        _target.ChangeCharacteristic(_specialEvent.characteristic);
                    }
                }
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SynergyTooltipManager.Instance.Use(this);
        }
    }
}
