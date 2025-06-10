using Shy.Unit;
using UnityEngine;
using Show = System.SerializableAttribute;

namespace Shy
{
    [Show]
    public struct Stat
    {
        public int maxHp;
        [HideInInspector] public int hp;
        public int str;
        public int def;

        public int this[StatEnum stat]
        {
            get
            {
                switch (stat)
                {
                    case StatEnum.MaxHp:
                        return maxHp;
                    case StatEnum.Hp:
                        return hp;
                    case StatEnum.Str:
                        return str;
                    case StatEnum.Def:
                        return def;
                    default:
                        return default;
                }
            }
            set
            {
                switch (stat)
                {
                    case StatEnum.MaxHp:
                        maxHp = value;
                        break;
                    case StatEnum.Hp:
                        hp = value;
                        break;
                    case StatEnum.Str:
                        str = value;
                        break;
                    case StatEnum.Def:
                        def = value;
                        break;
                }
            }
        }
    }

    [Show]
    public struct GetStat
    {
        public string key;
        public StatEnum stat;
        public bool self;
    }

    [Show]
    public struct GetStack
    {
        public string key;
        public BuffType buff;
    }

    [Show]
    public struct PoolItem
    {
        public GameObject obj;
        public int spawnCnt;
        public PoolingType type;
    }

    [Show]
    public struct LevelByDice
    {
        public int level;
        public DiceSO[] dices;
    }

    public struct DiceData
    {
        public Character user;
        public ActionWay actionWay;
        public Team team;
        public int skillNum;
    }

    public struct TargetData
    {
        public Character user;
        public ActionWay actionWay;
        public TargetWay targetTeam;

        public TargetData(DiceData _d, SkillEventSO _s)
        {
            user = _d.user;
            targetTeam = _s.targetTeam;


            if (_s.actionWay == ActionWay.None) actionWay = _d.actionWay;
            else actionWay = _s.actionWay;
        }
    }

    namespace Info
    {
        public struct CharacterInfo : IInfoData
        {
            public CharacterSO so;
            public Character character;

            public CharacterInfo(Character _ch, CharacterSO _d)
            {
                character = _ch;
                so = _d;
            }
        }

        public struct DiceInfo : IInfoData
        {
            public void SetData(Transform _panel)
            {
            }
        }

        public struct BuffInfo : IInfoData
        {
            public void SetData(Transform _panel)
            {
            }
        }
    }
}