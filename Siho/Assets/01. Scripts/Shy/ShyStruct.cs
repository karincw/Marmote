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

    public struct Skill
    {
        //user
        Character user;
        SkillSO useSkill;
    }
}