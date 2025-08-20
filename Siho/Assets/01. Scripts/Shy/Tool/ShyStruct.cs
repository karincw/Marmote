using System.Collections.Generic;
using UnityEngine;
using Show = System.SerializableAttribute;

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
                MaxHp = value;

                if (value > 0) Hp += value;
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
        public float def;
        public float regen;
        public float trueDmg;
        public float criChance, criDmg;
        public float hitChance, dodgeChange;
        public float addDmg, reduceDmg;
        public float drain;
        [Range(0, 100f)]
        public float counter;
        public float firstAttackBonus;
    }
    #endregion

    public struct Attack
    {
        public AttackResult attackResult;
        public float dmg;
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
    }

    namespace Pooling
    {
        [Show]
        public struct PoolItem
        {
            public PoolType poolType;
            public int count;
            public GameObject prefab;
        }
    }
}