using System.Collections.Generic;
using UnityEngine;
using Show = System.SerializableAttribute;
using Hide = UnityEngine.HideInInspector;
using UnityEngine.Events;

namespace Shy
{
    #region Stat
    [Show]
    public struct MainStat
    {
        public int STR;
        public int DEX;
        public int HP;
        public int INT;

        public static MainStat operator +(MainStat left, MainStat right)
        => new MainStat(left.STR + right.STR, left.DEX + right.DEX, left.HP + right.HP, left.INT + right.INT);

        public MainStat(int s, int d, int e, int f)
        {
            STR = s;
            DEX = d;
            HP = e;
            INT = f;
        }
    }

    [Show]
    public struct SubStat
    {
        #region Hp
        public float maxHp
        {
            get => MaxHp;
            set
            {
                float gap = value - MaxHp;

                MaxHp = value;

                if(gap > 0) Hp += gap;
                if (Hp > MaxHp) Hp = MaxHp;
            }
        }
        public float hp
        {
            get => Hp;
            set
            {
                Hp = value;

                if (Hp > maxHp) Hp = maxHp;
                if (Hp < 0) Hp = 0;
            }
        }
        private float Hp, MaxHp;
        #endregion
        public float atk;
        [Tooltip("n/s 초당 공격 횟수")]
        public float atkSpd;
        public float def, regen;
        public float trueDmg, criChance, criDmg;
        public float hitChance, dodgeChance;
        public float addDmg, reduceDmg;
        public float drain;
        [Range(0, 100f)]
        public float counter;
        public float surpiseAttackBonus;

        private static float GetValue(float _left, float _right) => _left * _right;

        public static SubStat operator *(SubStat _left, SubStat _right) => new SubStat 
            {
                maxHp = GetValue(_left.maxHp, _right.maxHp), 
                atk = GetValue(_left.atk, _right.atk),
                atkSpd = GetValue(_left.atkSpd, _right.atkSpd),
                def = GetValue(_left.def, _right.def),
                regen = GetValue(_left.regen, _right.regen),
                trueDmg = GetValue(_left.trueDmg, _right.trueDmg),
                criChance = GetValue(_left.criChance, _right.criChance),
                criDmg = GetValue(_left.criDmg, _right.criDmg),
                hitChance = GetValue(_left.hitChance, _right.hitChance),
                dodgeChance = GetValue(_left.dodgeChance, _right.dodgeChance),
                addDmg = GetValue(_left.addDmg, _right.addDmg),
                reduceDmg = GetValue(_left.reduceDmg, _right.reduceDmg),
                drain = GetValue(_left.drain, _right.drain),
                counter = GetValue(_left.counter, _right.counter),
                surpiseAttackBonus = GetValue(_left.surpiseAttackBonus, _right.surpiseAttackBonus)
            };

        public void Reset(int i = 0)
        {
            maxHp = atk = atkSpd = def = regen = trueDmg = criChance =
            criDmg = hitChance = dodgeChance = addDmg = reduceDmg = drain =
            counter = surpiseAttackBonus = i;
        }
    }
    #endregion

    public struct Attack
    {
        public AttackResult attackResult;
        public float dmg;
        public Character target;

        public Attack(AttackResult _r, float _d, Character _t)
        {
            attackResult = _r;
            dmg = _d;
            target = _t;
        }
    }

    #region Synergy & Characteristic
    public struct Characteristic
    {
        public bool notBlood, isNotBlood;
    }

    [Show]
    public struct StatSynergyValue
    {
        public int level;
        public float value;
    }

    [Show]
    public struct SpecialSynergyValue
    {
        public int level;
        public bool value;
    }
    #endregion

    public struct TextEvent
    {
        public UnityAction valueEvent;
        public int conditionValue;
        public UnityAction endEvent;

        public void EqualCheck(int _value)
        {
            if (_value == conditionValue) valueEvent?.Invoke();
        }
    }

    namespace Event
    {
        [Show]
        public struct EventData
        {
            public string eventExplain;
            public Sprite sprite;
            public EventCondition condition;
            public List<RandomResult> results;

            public RandomResult GetResult()
            {
                float value = Random.Range(0, 100f);
                
                for (int i = 0; i < results.Count; i++)
                {
                    value -= results[i].chance;
                    if (value <= 0)
                    {
                        return results[i];
                    }
                }

                Debug.LogError("최종 값이 100이 되지 않습니다. by => " + eventExplain
                    + "\nSo, Return to number 0 result");
                return results[0];
            }
        }

        [Show]
        public struct RandomResult
        {
            public EventResultSO resultSO;
            [Range(0, 100f)]
            public float chance;
        }

        [Show]
        public struct EventCondition
        {
            public EventItemSO item;
            public int value;
            public ConditionType conditionType;
        }

        namespace LadderGame
        {
        }
    }

    namespace Pooling
    {
        [Show]
        public struct PoolItem
        {
            public PoolType poolType;
            public int count;
            public Pool prefab;
        }
    }
}