using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Shy.Event
{
    [System.Serializable]
    public class BlackJackCard
    {
        [SerializeField] private TextMeshProUGUI totalTmp;
        internal int cnt { get; private set; }
        internal int value { get; private set; }
        private const int conditionValue = 16;
        public BlackJackDice[] dices;

        private TextEvent textEvent;

        public void Roll(Sprite _sp, int _value)
        {
            value += _value;
            dices[cnt++].Roll(_sp);
        }

        public void Init()
        {
            value = cnt = 0;

            totalTmp.color = Color.black;
            totalTmp.SetText("0");

            foreach (var item in dices)
            {
                item.Init(TextUpdate);
            }
        }

        public void SetEvent(UnityAction _endEvent)
        {
            textEvent.conditionValue = conditionValue;
            textEvent.valueEvent = () => totalTmp.color = Color.red;
            textEvent.endEvent = _endEvent;
        }

        private void TextUpdate()
        {
            SequnceTool.Instance.DOCountDown(totalTmp, value, 0.15f, textEvent);
        }

        public bool OverValueCheck() => value >= conditionValue - 1;
        public bool OverCntCheck() => cnt >= 5;
    }
}