namespace Shy
{
    public enum Team
    {
        All,
        Player,
        Enemy
    }

    public enum Target
    {
        Self,
        Opponent,
        All
    }

    public enum AttackResult
    {
        Normal = 0,
        Critical,
        Dodge,
        Block
    }

    public enum MainStatEnum
    {
        Str, Dex, Hp, Int
    }

    public enum SubStatEnum
    {
        MaxHp, Hp, Def,
        Atk, AtkSpd, Regen,
        CriChance, CriDmg,
        HitChance, DodgeChance,
        AddDmg, RedDmg,
        TrueDmg, Drain, Counter,
        FirstAtkBonus
    }

    public enum CharacteristicEnum
    {
        NotBlood, IsNotBlood,
    }

    public enum SynergyType
    {
        Blood,
        Strong,
        Fear,
        Spine,
        Cool,
        Steel,
        Panic
    }

    public enum Calculate
    {
        Plus, Minus, Multiply,
        Divide, Change, Percent,
        ReflectPercent
    }

    public enum BattleEventMode
    {
        None,
        Event,
        Tooltip,
        Text,
        Dice
    }

    namespace Pooling
    {
        public enum PoolType
        {
            Dice,
            Synergy,
            DmgText,
            Alert
        }
    }

    namespace Event
    {
        public enum BattleEventType
        {
            Run,
            Surprise,
            Talk
        }

        public enum ConditionType
        {
            None,
            Equal,
            More,
            Below
        }
    }
}