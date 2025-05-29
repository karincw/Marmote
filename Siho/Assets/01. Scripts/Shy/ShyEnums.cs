namespace Shy
{
    public enum Team
    {
        None,
        Player,
        Enemy
    }

    public enum EventType
    {
        AttackEvent,
        ShieldEvent,
        HealEvent,
        BuffEvent
    }

    public enum BuffType
    {
        Brave = 0,
        Bleeding,
        Bondage,
        Crumbs,
        Gingerbread,
        Music,
    }

    public enum UpgradeType
    {
        Stack,
        Count,
        Hp
    }

    public enum StatEnum
    {
        MaxHp,
        Hp,
        Str,
        Def
    }

    public enum PoolingType
    {
       Buff,
       DmgText,
       end
    }

    public enum ActionWay
    {
        None,
        Self,
        Opposite,//맞은편
        Random, //무작위
        All, //전체
        LessHp,
        MoreHp,
        Already,
        Fast,
        Slow,
    }

    public enum AttackMotion
    {
        Near = 0,
        Long,
        All,
        Self,
        PetSelf,
        PetAndShort,
        PetAndLong,
        SummonAndShort,
        SummonAndLong,
        DropAttack,
    }
}
