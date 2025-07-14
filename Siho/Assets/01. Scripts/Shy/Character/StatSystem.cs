namespace Shy
{
    public static class StatSystem
    {
        private static float dummy1 = 0;
        private static int dummy2 = 0;

        public static ref float GetSubStatRef(ref SubStat _subStat, SubStatEnum _statEnum)
        {
            switch (_statEnum)
            {
                case SubStatEnum.Atk: return ref _subStat.atk;
                case SubStatEnum.Def: return ref _subStat.def;
                case SubStatEnum.AtkSpd: return ref _subStat.atkSpd;
                case SubStatEnum.MaxHp: return ref _subStat.MaxHp;
                case SubStatEnum.Hp: return ref _subStat.Hp;
                case SubStatEnum.CriChance: return ref _subStat.criChance;
                case SubStatEnum.CriDmg: return ref _subStat.criDmg;
                case SubStatEnum.TrueDmg: return ref _subStat.trueDmg;
                case SubStatEnum.Drain: return ref _subStat.drain;
                default: return ref dummy1;
            }
        }

        public static ref int GetMainStatRef(ref MainStat _mainStat, MainStatEnum _statEnum)
        {
            switch (_statEnum)
            {
                case MainStatEnum.Str: return ref _mainStat.STR;
                case MainStatEnum.Dex: return ref _mainStat.DEX;
                case MainStatEnum.Hp: return ref _mainStat.HP;
                case MainStatEnum.Int: return ref _mainStat.INT;
                default: return ref dummy2;
            }
        }

        public static SubStat ChangeSubStat(MainStat _ms)
        {
            var _ss = new SubStat();

            _ss.maxHp = 5 + _ms.HP * 4 + _ms.STR * 2;
            _ss.hp = _ss.maxHp;
            _ss.atk = 8 + _ms.STR * 1.1f;
            _ss.atkSpd = 0.2f + _ms.DEX * 0.05f;
            _ss.def = 5 + _ms.HP;
            _ss.regen = 0;
            _ss.trueDmg = 0;
            _ss.criChance = 25 + _ms.INT * 0.25f;
            _ss.criDmg = _ms.HP * 0.004f + _ms.STR * 0.008f;
            _ss.hitChance = 50 + _ms.DEX * 3.3f + _ms.INT * 4.2f;
            _ss.dodgeChange = _ms.DEX * 2.5f + _ms.INT * 1.5f;
            _ss.addDmg = 0;
            _ss.reduceDmg = 0;
            _ss.drain = 0;
            _ss.counter = 0;

            return _ss;
        }
    }
}