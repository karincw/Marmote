using Shy.Info;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        private PressCompo pressCompo;

        #region Get
        public BuffType GetBuffType() => buff.buffType;
        public int GetCount() => value;
        public int GetBuffCount(BuffType _buff)
        {
            if (buff.buffType == _buff) return value;
            return 0;
        }
        #endregion

        public bool CheckBuff(BuffType _buff) => buff.buffType == _buff;

        private void Awake()
        {
            img = GetComponent<Image>();
            tmp = GetComponentInChildren<TextMeshProUGUI>();
            pressCompo = GetComponent<PressCompo>();
        }

        public void Init(Character _user, BuffSO _buff, int _life)
        {
            buff = _buff;
            value = _life;
            user = _user;
            img.sprite = _buff.sprite;

            LifeTextUpdate();

            pressCompo.Init(InfoType.Buff, () =>
            {
                InfoManager.Instance.OpenPanel(InfoType.Buff, new BuffInfo(_buff.buffType, _user.buffGroup));
            });

            BuffManager.Instance.OnBuffEvent(BuffUseCondition.OnStart, buff.buffType, user, value);
        }

        public void Add(int _value)
        {
            value += _value;
        }

        private void LifeTextUpdate()
        {
            if (buff.removeCondition != BuffRemoveCondition.Count) return;

            tmp.text = value.ToString();
            tmp.gameObject.SetActive(true);
        }

        private void Pop()
        {
            BuffManager.Instance.OnBuffEvent(BuffUseCondition.OnEnd, buff.buffType, user, value);
            tmp.gameObject.SetActive(false);
            Pooling.Instance.Return(gameObject, PoolingType.Buff);
        }

        public BuffUI CountDown()
        {
            BuffManager.Instance.OnBuffEvent(BuffUseCondition.Update, buff.buffType, user, value);

            if (buff.removeCondition != BuffRemoveCondition.Count) return null;

            if (--value == 0)
            {
                Pop();
                return this;
            }

            LifeTextUpdate();
            return null;
        }
    }
}
