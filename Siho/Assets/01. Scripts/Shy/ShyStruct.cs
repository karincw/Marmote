using System.Collections.Generic;
using UnityEngine;
using Show = System.SerializableAttribute;

namespace Shy
{
    [Show]
    public struct MainStat
    {
        public int STR;
        public int DEX;
        public int HP;
        public int INT;
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
                if(Hp > MaxHp) Hp = MaxHp;
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
        internal float Hp, MaxHp;
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
    }

    public struct Characteristic
    {
        public bool notBlood, isNotBlood;

    }

    public struct Attack
    {
        public AttackResult attackResult;
        public float dmg;
    }

    [Show]
    public struct SynergyEffect
    {
        public int level;
        public List<SynergyEventSO> synergyEvents;
    }

    namespace Event
    {
        [Show]
        public struct EventData
        {
            public string eventExplain;
            public Sprite sprite;
            public EventResultSO result;
            [Range(0, 100f)]
            public float successChance;
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