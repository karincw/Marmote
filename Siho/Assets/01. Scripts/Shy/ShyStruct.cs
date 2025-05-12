using UnityEngine;
using Show = System.SerializableAttribute;

namespace Shy
{
    [Show]
    public struct Stat
    {
        public int maxHp;
        public int hp;
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
        public ActionWay target;
    }

    [Show]
    public struct GetStack
    {
        public string key;
        public BuffType buff;
    }

    [Show]
    public struct BuffItem
    {
        public BuffType buff;
        public bool canCountDown;
        public Sprite sprite;
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
}
