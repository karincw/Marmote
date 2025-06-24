using UnityEngine;
using System.Collections.Generic;
using Shy.Unit;

namespace Shy
{
    public class BuffManager : MonoBehaviour
    {
        public static BuffManager Instance;

        public List<BuffSO> buffs;
        private Dictionary<BuffType, BuffSO> buffDictionary = new Dictionary<BuffType, BuffSO>();

        private void Awake()
        {
            if (Instance != null) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            foreach (var _buff in buffs)
            {
                buffDictionary.Add(_buff.buffType, _buff);
            }
        }

        private bool GetRand(int _per) => _per > Random.Range(0, 100);

        public BuffSO GetBuff(BuffType _buffType) => buffDictionary[_buffType];

        public void OnBuffEvent(BuffUseCondition _c, BuffUI _b, Character _t) => OnBuffEvent(_c, _b.GetBuffType(), _t, _b.GetCount());

        public void OnBuffEvent(BuffUseCondition _condition, BuffType _buffType, Character _target, int _value)
        {
            BuffUseCondition condition = GetBuff(_buffType).useCondition;

            if (_condition != condition) return;

            switch (condition)
            {
                case BuffUseCondition.OnStart:
                    OnBeginEvent(_buffType, _target, _value);
                    break;
                case BuffUseCondition.Update:
                    OnUpdateEvent(_buffType, _target, _value);
                    break;
                case BuffUseCondition.OnEnd:
                    OnEndEvent(_buffType, _target, _value);
                    break;
                case BuffUseCondition.OnAttack:
                    OnAttackEvent(_buffType, _target, _value);
                    break;
                case BuffUseCondition.OnHit:
                    OnHitEvent(_buffType, _target, _value);
                    break;
            }

            _target.HealthVisibleEvent(true);
        }

        #region Time
        private void OnBeginEvent(BuffType _buffType, Character _target, int _value)
        {
            switch (_buffType)
            {
                case BuffType.Brave:
                    _target.SetBonusStat(StatEnum.AdditionalDmg, 20);
                    break;
                case BuffType.Shield:
                    _target.SetBonusStat(StatEnum.ReductionDmg, 10);
                    break;
                case BuffType.Gingerbread:
                    //피감량
                    break;
            }
        }

        private void OnUpdateEvent(BuffType _buffType, Character _target, int _value)
        {
            switch (_buffType)
            {
                case BuffType.Bleeding:
                    float _lessHp = _target.GetNowStat(StatEnum.MaxHp) - _target.GetNowStat(StatEnum.Hp);
                    _target.OnValueEvent(Mathf.RoundToInt(_lessHp * 0.1f), EventType.AttackEvent, 100);
                    break;


                case BuffType.Crumbs:
                    
                    break;

                case BuffType.Burn:
                    _target.OnValueEvent(_value * 2, EventType.AttackEvent, 100);
                    break;
            }


        }

        private void OnEndEvent(BuffType _buffType, Character _target, int _value)
        {
            switch (_buffType)
            {
                case BuffType.Brave:
                    _target.SetBonusStat(StatEnum.AdditionalDmg, -20);
                    break;
                case BuffType.Gingerbread:
                    //피감량 감소
                    break;
                case BuffType.Shield:
                    _target.SetBonusStat(StatEnum.ReductionDmg, -10);
                    break;
                case BuffType.Bondage:
                    break;
                case BuffType.Music:
                    break;
                case BuffType.Confusion:
                    break;
            }
        }
        #endregion

        #region Attack
        private void OnAttackEvent(BuffType _buffType, Character _target, int _value)
        {
            switch (_buffType)
            {
            }
        }

        private void OnHitEvent(BuffType _buffType, Character _target, int _value)
        {
            switch (_buffType)
            {
                case BuffType.Gingerbread:
                    if(GetRand(25 * _value))
                        _target.OnBuffEvent(1, BuffType.Gingerbread);
                    break;
            }
        }
        #endregion
    }
}
