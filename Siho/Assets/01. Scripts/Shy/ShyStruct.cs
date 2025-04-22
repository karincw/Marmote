using UnityEngine;

namespace Shy
{
    [System.Serializable]
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

    [System.Serializable]
    public struct GetStat
    {
        public string key;
        public StatEnum stat;
        public ActionWay target;
    }

    [System.Serializable]
    public struct PoolItem
    {
        public GameObject obj;
        public int spawnCnt;
        public PoolingType type;
    }
}
