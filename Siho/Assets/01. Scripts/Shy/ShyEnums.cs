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
        Str,
        Dex,
        Hp,
        Int
    }

    public enum SubStatEnum
    {
        MaxHp, Hp, Def,
        Atk, AtkSpd, Regen,
        CriChance, CriDmg,
        HitChance, DodgeChance,
        AddDmg, RedDmg,
        TrueDmg, Drain, Counter
    }

    public enum CharacteristicEnum
    {
        NotBlood, IsNotBlood,
    }

    public enum SynergyType
    {
        ChangeSubStat
    }

    public enum Calculate
    {
        Plus,
        Minus,
        Multiply,
        Divide,
        Change
    }

    public enum PoolType
    {
        Dice,
        Synergy,
        DmgText
    }

    namespace Event
    {
        public enum BattleEvent
        {
            Run,
            Surprise,
            Talk
        }
    }
}