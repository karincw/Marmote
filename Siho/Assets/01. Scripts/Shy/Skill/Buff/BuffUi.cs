using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Shy.Unit
{
    public class BuffUI : MonoBehaviour
    {
        //Data
        private int value = 0;
        private BuffSO buff;
        private Character user;

        //Visual
        private Image img;
        private TextMeshProUGUI tmp;

        private void Awake()
        {
            img = GetComponent<Image>();
            tmp = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void Init(Character _user, BuffSO _buff, int _life)
        {
            buff = _buff;
            value = _life;
            user = _user;
            img.sprite = _buff.sprite;

            LifeTextUpdate();

            if (buff.useCondition == BuffUseCondition.OnStart) OnEvent();
        }

        private void LifeTextUpdate()
        {
            if (buff.removeCondition != BuffRemoveCondition.Count) return;

            tmp.text = value.ToString();
            tmp.gameObject.SetActive(true);
        }

        private void OnEvent()
        {
            switch (buff.buffType)
            {
                case BuffType.Brave:
                    user.BonusStat(StatEnum.Str, 15);
                    break;
                case BuffType.Bleeding:
                    break;
                case BuffType.Gingerbread:
                    break;
                case BuffType.Crumbs:
                    break;
                case BuffType.Bondage:
                    break;
                case BuffType.Burn:
                    user.OnValueEvent(value * 2, EventType.AttackEvent, true);
                    break;
                case BuffType.Music:
                    break;
                case BuffType.Confusion:
                    break;
            }
        }

        private void DestroyEvent()
        {
            switch (buff.buffType)
            {
                case BuffType.Brave:
                    user.BonusStat(StatEnum.Str, -15);
                    break;
                case BuffType.Bleeding:
                    break;
                case BuffType.Bondage:
                    break;
                case BuffType.Crumbs:
                    break;
                case BuffType.Gingerbread:
                    break;
            }
        }

        private void Pop()
        {
            DestroyEvent();
            tmp.gameObject.SetActive(false);
            Pooling.Instance.Return(gameObject, PoolingType.Buff);
        }

        public void CountDown()
        {
            if (buff.removeCondition != BuffRemoveCondition.Count) return;

            if (--value == 0)
            {
                Pop();
                return;
            }
            else
            {
                OnEvent();
            }

            LifeTextUpdate();
        }

        public int CheckBuff(BuffType _buff)
        {
            //if (buffType == _buff) return value;
            return 0;
        }

        public bool CheckBuff(BuffSO _buff) => buff == _buff;
    }
}
