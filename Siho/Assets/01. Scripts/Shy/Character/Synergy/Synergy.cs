using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shy.Pooling
{
    public class Synergy : Pool, IPointerDownHandler, IPointerUpHandler
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

        public void Add(int i = 1)
        {
            if (so.maxLevel < value + i)
                value = so.maxLevel;
            else 
                value += i;

            UpdateValue();
        }

        #region String
        private void UpdateValue()
        {
            numTmp.text = value.ToString();
        }

        public string GetDataValue()
        {
            string s = "";

            foreach (var _event in so.synergies)
            {
                if (_event is StatEventSO _statEvent)
                    s += _statEvent.valueSign.Replace("n", _statEvent.GetData(value).ToString()) + " / ";
            }

            if (s != "") s = s.Remove(s.Length - 2);
            return s;
        }
        #endregion

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
                            item.AddStat(_statEvent.GetData(value), _statEvent.calculate, _statEvent.subStat);
                        break;

                    case SpecialEventSO _specialEvent:
                        foreach (var item in targets)
                            item.ChangeCharacteristic(_specialEvent.characteristic, _specialEvent.GetData(value));
                        break;
                }
            }
        }

        #region Click
        public void OnPointerDown(PointerEventData eventData)
        {
            BattleEventManager.Instance.OpenTooltip(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            BattleEventManager.Instance.CloseTooltip(this);
        }
        #endregion
    }
}
