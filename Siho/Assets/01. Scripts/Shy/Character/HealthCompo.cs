using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

namespace Shy.Unit
{
    public class HealthCompo : MonoBehaviour
    {
        [SerializeField] private int maxHp, hp, shield;
        [SerializeField] private Image healthGuage, effectGuage;
        [SerializeField] private Transform dmgTxtPos;

        internal bool isDie = false;
        internal float cnt;

        private UnityAction hitEvent;

        private float GetHpPer() => hp / (float)maxHp;

        public void Init(int _hp, UnityAction _action)
        {
            maxHp = hp = _hp;
            hitEvent = _action;

            float healthPer = GetHpPer();
            healthGuage.fillAmount = healthPer;
            effectGuage.fillAmount = healthPer;
        }

        public IEnumerator OnDamageEvent(int _value)
        {
            if (_value > 0)
            {
                _value -= shield;

                if (_value > 0)
                {
                    yield return new WaitForSeconds(0.2f * cnt++);

                    Pooling.Instance.Use(PoolingType.DmgText, dmgTxtPos).GetComponent<DamageText>()
                        .Use(_value.ToString());

                    hitEvent?.Invoke();
                    hp -= _value;
                }

                if (hp <= 0)
                {
                    isDie = true;
                    hp = 0;
                }
            }
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
            float healthPer = GetHpPer();
            healthGuage.fillAmount = healthPer;
            effectGuage.DOFillAmount(healthPer, 0.35f);
        }
    }
}
