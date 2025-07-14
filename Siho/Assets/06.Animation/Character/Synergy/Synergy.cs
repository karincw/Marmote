using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shy.Pooling
{
    public class Synergy : MonoBehaviour
    {
        public SynergySO so;
        private int value = 0;

        private Image outline, icon;
        private TextMeshProUGUI numTmp;
        private Team userTeam;

        private void Awake()
        {
            outline = GetComponent<Image>();
            icon = transform.Find("IconBack").Find("Icon").GetComponent<Image>();
            numTmp = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void Init(SynergySO _so, Team _team)
        {
            so = _so;
            userTeam = _team;

            outline.color = _so.outlineColor;
            icon.sprite = _so.icon;

            numTmp.gameObject.SetActive(_so.showNum);

            transform.localScale = Vector3.one;
            transform.localPosition = Vector3.zero;
        }

        public void SetValue(int _value = 1)
        {
            value += _value;
            numTmp.text = value.ToString();
        }

        public void UseSynergy()
        {
            SynergyEffect effect = new();

            foreach (var _synergy in so.synergies)
            {
                if (_synergy.level == value)
                {
                    effect = _synergy;
                    break;
                }
            }

            if (effect.synergyEvents.Count == 0) return;

            foreach (var _event in effect.synergyEvents)
            {
                Team _opponentTeam = userTeam == Team.Player ? Team.Enemy : Team.Player;
                Target _targetEnum = _event.target;

                if (_event is StatEventSO _statEvent)
                {
                    if (_targetEnum != Target.Self)
                    {
                        var _target = BattleManager.Instance.GetCharacter(_opponentTeam);
                        _target.AddStat(_statEvent.value, Calculate.Plus, _statEvent.subStat);
                    }
                    if (_targetEnum != Target.Opponent)
                    {
                        var _target = BattleManager.Instance.GetCharacter(userTeam);
                        _target.AddStat(_statEvent.value, Calculate.Plus, _statEvent.subStat);
                    }
                }
                else if (_event is SpecialEventSO _specialEvent)
                {
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
    }
}
