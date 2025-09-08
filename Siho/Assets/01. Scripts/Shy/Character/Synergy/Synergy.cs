using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

namespace Shy.Pooling
{
    public class Synergy : Pool, IPointerDownHandler, IPointerUpHandler
    {
        public SynergySO so;
        private int value = 0;
        private Team userTeam;

        private Image icon;
        private TextMeshProUGUI numTmp;

        private Dictionary<SynergyEventSO, Character[]> synergyData;

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
            UseSynergy();
        }

        public void Add(int i = 1)
        {
            if (so.maxLevel == value)
            {
                Debug.Log("Already Max Value");
                return;
            }

            if (so.maxLevel < value + i)
                value = so.maxLevel;
            else 
                value += i;

            if (synergyData.Count != 0) RemoveSynergy();

            UseSynergy();
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

        #region Use
        private void UseSynergy()
        {
            synergyData = new();

            foreach (var _event in so.synergies)
            {
                Target _targetEnum = _event.target;
                Team _targetTeam = Team.All;

                if (_targetEnum == Target.Self) _targetTeam = userTeam;
                if (_targetEnum == Target.Opponent) _targetTeam = userTeam == Team.Player ? Team.Enemy : Team.Player;

                var _targets = BattleManager.Instance.GetCharacters(_targetTeam);
                synergyData.Add(_event, _targets);

                switch (_event)
                {
                    case StatEventSO _statEvent:
                        foreach (var item in _targets)
                            item.ChangeStat(_statEvent.GetData(value), _statEvent.calculate, _statEvent.subStat);
                        break;

                    case SpecialEventSO _specialEvent:
                        foreach (var item in _targets)
                            item.ChangeCharacteristic(_specialEvent.characteristic, _specialEvent.GetData(value));
                        break;
                }
            }
        }

        private Calculate CalculateReflect(Calculate _calc)
        {
            switch (_calc)
            {
                case Calculate.Plus:        return Calculate.Minus;
                case Calculate.Minus:       return Calculate.Plus;
                case Calculate.Multiply:    return Calculate.Divide;
                case Calculate.Divide:      return Calculate.Multiply;
                case Calculate.Percent:     return Calculate.ReflectPercent;
            }

            Debug.LogError("Fail Calculate Reflect");
            return Calculate.Minus;
        }

        public void RemoveSynergy()
        {
            foreach (var _event in so.synergies)
            {
                switch (_event)
                {
                    case StatEventSO _statEvent:
                        foreach (var item in synergyData[_event])
                            item.ChangeStat(_statEvent.GetData(value), CalculateReflect(_statEvent.calculate), _statEvent.subStat);
                        break;

                    case SpecialEventSO _specialEvent:
                        foreach (var item in synergyData[_event])
                            item.ChangeCharacteristic(_specialEvent.characteristic, !_specialEvent.GetData(value));
                        break;
                }
            }
        }
        #endregion

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
