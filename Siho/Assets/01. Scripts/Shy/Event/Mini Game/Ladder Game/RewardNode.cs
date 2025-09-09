using System;
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

        internal override void Init(bool _isLinkeNode, LadderNode _downNode)
        {
            base.Init(_isLinkeNode, _downNode);
            rewardIcon = transform.Find("Reward Icon").GetComponent<Image>();
            rewardValue = rewardIcon.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void Init(EventItemSO _item, int _value)
        {
            rewardIcon.sprite = _item.GetIcon();
            rewardValue.SetText(_value <= 1 ? "" : _value.ToString());
        }
        internal void Init(Sprite _sprite)
        {
            rewardIcon.sprite = _sprite;
            rewardValue.SetText("");
        }

        public override void AnimateDown(UnityAction _action)
        {
            base.AnimateDown(_action);
            Debug.Log("Reward Node µ¿ÀÛ -> " + gameObject.name);
        }
    }
}