using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shy
{
    public class HealthCompo : MonoBehaviour
    {
        [SerializeField] private int maxHp, hp, shield;
        [SerializeField] private Image healthGuage;
        [SerializeField] private TextMeshProUGUI healthValue;
        [SerializeField] private Transform dmgTxtPos;

        internal bool isDie = false;

        private UnityAction hitEvent;

        public void Init(int _hp, UnityAction _action)
        {
            maxHp = _hp;
            hp = maxHp;
            hitEvent = _action;

            UpdateHealth();
        }

        public void OnDamageEvent(int _value)
        {
            if (_value <= 0) return;

            _value -= shield;

            if(_value > 0)
            {
                Pooling.Instance.Use(PoolingType.DmgText, dmgTxtPos).GetComponent<DamageText>().Use(_value.ToString());

                hitEvent?.Invoke();
                hp -= _value;
            }

            if (hp <= 0)
            {
                isDie = true;
                hp = 0;
            }

            UpdateHealth();
        }

        public void OnHealEvent(int _value)
        {
            //회복 불가 디버프가 있다면 체크

            _value = Mathf.Min(_value + hp, maxHp);
            hp = _value;
        }

        public void OnShieldEvent(int _value)
        {
            shield += _value;
        }

        public int GetHealth() => hp;
        public int GetMaxHealth() => maxHp;

        public void UpdateHealth()
        {
            healthGuage.fillAmount = hp / (float)maxHp;
            healthValue.text = hp + " / " + maxHp;
        }
    }
}
