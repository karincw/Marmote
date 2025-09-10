using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shy.Event.LadderGame
{
    public class RewardNode : LadderNode
    {
        private Image rewardIcon;
        private TextMeshProUGUI rewardValue;

        private LadderReward reward;

        internal override void Init(bool _isLinkeNode, LadderNode _downNode)
        {
            base.Init(_isLinkeNode, _downNode);
            rewardIcon = transform.Find("Reward Icon").GetComponent<Image>();
            rewardValue = rewardIcon.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void Init(LadderReward _reward)
        {
            reward = _reward;

            rewardIcon.sprite = _reward.item.GetIcon();
            rewardValue.SetText(_reward.value <= 1 ? "" : _reward.value.ToString());
        }

        internal void Init(Sprite _sprite)
        {
            reward = new();

            rewardIcon.sprite = _sprite;
            rewardValue.SetText("");
        }

        public override void AnimateDown(UnityAction _action)
        {
            UnityAction _ac = Reward;
            _ac += _action;
            base.AnimateDown(_ac);
        }

        private void Reward()
        {
            Debug.Log("Get Reward -> " + reward.item);

            if (reward.item is Item_Stat _stat)
            {
                PlayerManager.Instance.AddStat(_stat.statType, reward.value);
            }
            else if (reward.item is Item_Synergy _synergy)
            {
                PlayerManager.Instance.AddSynergy(_synergy.item.synergyType, reward.value);
            }
            else if (reward.item is Item_Money _money)
            {
                //Money Ãß°¡ reward.value;
            }
        }
    }
}