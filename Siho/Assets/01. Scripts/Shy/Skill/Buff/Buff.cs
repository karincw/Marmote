using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shy
{
    public class Buff : MonoBehaviour
    {
        //Data
        private int life = 0;
        private BuffEvent buffEvent;
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

        public void Init(Character _user, BuffEvent _ev, int _life)
        {
            buffEvent = _ev;
            life = _life;
            user = _user;

            tmp.text = life.ToString();

            if (buffEvent == BuffEvent.Brave)
            {
                OnEvent();
                oneTime = true;
            }
        }

        private void OnEvent()
        {
            switch (buffEvent)
            {
                case BuffEvent.Brave:
                    user.BonusStat(StatEnum.Str, 15);
                    break;
                case BuffEvent.Scare:
                    break;
                case BuffEvent.Poison:
                    break;
                case BuffEvent.Burn:
                    break;
            }
        }

        private void DestroyEvent()
        {
            switch (buffEvent)
            {
                case BuffEvent.Brave:
                    user.BonusStat(StatEnum.Str, -15);
                    break;
                case BuffEvent.Scare:
                    break;
                case BuffEvent.Poison:
                    break;
                case BuffEvent.Burn:
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
    }
}
