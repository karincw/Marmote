using System;
using UnityEngine;
using UnityEngine.UI;

namespace Shy
{
    public class HealthCompo : MonoBehaviour
    {
        [SerializeField] private int maxHp;
        [SerializeField] private int hp;
        [SerializeField] private int shield;

        private Image healthGuage;

        public Action dieEvent;

        private void Awake()
        {
            healthGuage = transform.Find("Health Bar").GetChild(0).GetComponent<Image>();
        }

        public void Init(int _hp)
        {
            _hp = maxHp;
            hp = maxHp;
        }

        public void OnDamageEvent(int _value)
        {
            _value -= shield;

            if(_value > 0)
            {
                hp -= _value;
            }

            if (hp <= 0) dieEvent?.Invoke();
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
    }
}
