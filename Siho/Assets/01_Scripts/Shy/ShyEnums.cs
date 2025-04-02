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

    public enum ActionWay
    {
        None,
        Self,
        Opposite,//맞은편
        Select, //지정
        Random, //무작위
        All //전체
    }

    public enum StatEnum
    {
        MaxHp,
        Hp,
        Str,
        Def
    }

    public enum PoolingObject
    {

    }
}
