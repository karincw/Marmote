using Shy;
using System;
using UnityEngine;

public class StatCompo : MonoBehaviour
{
    private int str, def;

    public int bonusAtk = 0;
    public int bonusDef = 0;

    internal void Init(Stat _stats)
    {
        str = _stats.str;
        def = _stats.def;
    }

    internal int GetDef() => def;
    internal int GetStr() => str;
}
