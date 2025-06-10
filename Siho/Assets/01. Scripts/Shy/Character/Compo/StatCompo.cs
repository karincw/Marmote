using Shy;
using System;
using UnityEngine;

public class StatCompo
{
    private Stat baseStat, bonusStat;
    [Range(0, 100)] private int reductionDmg = 0, additionalDmg = 0;

    public StatCompo(Stat _stats)
    {
        baseStat = _stats;
    }

    private int GetStatValue(StatEnum _statEnum, Stat _stats, bool _isBase)
    {
        return _statEnum switch
        {
            StatEnum.MaxHp => _stats.maxHp,
            StatEnum.Str => _stats.str,
            StatEnum.Def => _stats.def,
            StatEnum.AdditionalDmg => _isBase ? 0 : additionalDmg,
            StatEnum.ReductionDmg => _isBase ? 0 : reductionDmg,
            _ => 0
        };
    }

    public int GetBaseStat(StatEnum _enum) => GetStatValue(_enum, baseStat, true);
    public int GetBonusStat(StatEnum _enum) => GetStatValue(_enum, bonusStat, false);
    public int GetApplyStat(StatEnum _enum) => GetBaseStat(_enum) + GetBonusStat(_enum);

    public void UpdateBonusStat(StatEnum _stat, int _value)
    {
        switch (_stat)
        {
            case StatEnum.MaxHp: 
                bonusStat.maxHp += _value; 
                break;

            case StatEnum.Str:
                bonusStat.str += _value;
                break;

            case StatEnum.Def:
                bonusStat.def += _value;
                break;

            case StatEnum.AdditionalDmg:
                additionalDmg += _value;
                break;

            case StatEnum.ReductionDmg:
                reductionDmg += _value;
                break;
        }
    }

}
