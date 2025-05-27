using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shy.Unit
{
    public class Buff : MonoBehaviour
    {
        //Data
        private int life = 0;
        private BuffType buffType;
        private bool oneTime = false;
        private Character user;

        //Visual
        private Image img;
        private TextMeshProUGUI tmp;

        private void Awake()
        {
            img = GetComponent<Image>();
            tmp = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void Init(Character _user, BuffType _ev, int _life)
        {
            buffType = _ev;
            life = _life;
            user = _user;

            tmp.text = life.ToString();

            if (buffType == BuffType.Brave)
            {
                OnEvent();
                oneTime = true;
            }
        }

        private void OnEvent()
        {
            switch (buffType)
            {
                case BuffType.Brave:
                    user.BonusStat(StatEnum.Str, 15);
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

        private void DestroyEvent()
        {
            switch (buffType)
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
            oneTime = false;
            Pooling.Instance.Return(gameObject, PoolingType.Buff);
        }

        public void CountDown()
        {
            tmp.text = (--life).ToString();

            if(life == 0)
            {
                Pop();
            }
            else
            {
                if(!oneTime) OnEvent();
            }
        }

        public int CheckBuff(BuffType _buff)
        {
            if (buffType == _buff) return life;
            return 0;
        }
    }
}
