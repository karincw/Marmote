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
                case SubStatEnum.CriChance: return ref _subStat.criChance;
                case SubStatEnum.CriDmg: return ref _subStat.criDmg;
                case SubStatEnum.TrueDmg: return ref _subStat.trueDmg;
                case SubStatEnum.Drain: return ref _subStat.drain;
                case SubStatEnum.Regen: return ref _subStat.regen;
                case SubStatEnum.HitChance: return ref _subStat.hitChance;
                case SubStatEnum.DodgeChance: return ref _subStat.dodgeChance;
                case SubStatEnum.AddDmg: return ref _subStat.addDmg;
                case SubStatEnum.RedDmg: return ref _subStat.reduceDmg;
                case SubStatEnum.Counter: return ref _subStat.counter;
                case SubStatEnum.FirstAtkBonus: return ref _subStat.surpiseAttackBonus;

                case SubStatEnum.MaxHp: case SubStatEnum.Hp:
                    UnityEngine.Debug.LogError("Error Cant use MaxHp or Hp by GetSubStat Ref");
                    return ref dummy1;

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

        public static void ChangeStat(ref SubStat _subStat, float _newValue, Calculate _calc, SubStatEnum _statEnum)
        {
            if (_statEnum == SubStatEnum.MaxHp) 
                _subStat.maxHp = Calculator.GetValue(_subStat.maxHp, _newValue, _calc);
            else
            {
                ref float v = ref GetSubStatRef(ref _subStat, _statEnum);
                v = Calculator.GetValue(v, _newValue, _calc);
            }
        }

        public static SubStat ChangeSubStat(MainStat _ms)
        {
            var _ss = new SubStat();

            _ss.maxHp = 10 + _ms.HP * 4 + _ms.STR * 2;
            _ss.hp = _ss.maxHp;
            _ss.atk = 8 + _ms.STR * 1.1f;
            _ss.atkSpd = 0.2f + _ms.DEX * 0.05f;
            _ss.def = 5 + _ms.HP * 0.7f + _ms.INT * 0.4f;
            _ss.criChance = 20 + _ms.INT * 0.5f + _ms.DEX * 0.4f;
            _ss.criDmg = 130 + _ms.HP * 0.4f + _ms.STR * 0.85f;
            _ss.hitChance = 70 + _ms.INT * 4.5f;
            _ss.dodgeChance = 15 + _ms.DEX * 2.8f;

            return _ss;
        }
    }
}