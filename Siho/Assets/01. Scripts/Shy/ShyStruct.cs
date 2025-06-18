using System.Text;
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

        public static Stat operator +(Stat s1, Stat s2)
        {
            return new Stat
            {
                maxHp = s1.maxHp + s2.maxHp,
                hp = s1.hp + s2.hp,
                str = s1.str + s2.str,
                def = s1.def + s2.def
            };
        }
        public override string ToString() => $"[MaxHP : {maxHp} | Hp : {hp} | Strength : {str} | Defence : {def}]";
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
