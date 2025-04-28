using System;
using UnityEngine;

namespace Shy
{
    public class Bool
    {
        public static bool IsPetMotion(AttackMotion _motion)
        {
            if (_motion == AttackMotion.PetSelf) return true;
            if (_motion == AttackMotion.PetAndShort) return true;
            if (_motion == AttackMotion.PetAndLong) return true;
            return false;
        }

        internal static bool IsTeamMotion(AttackMotion _motion)
        {
            if (_motion == AttackMotion.PetSelf) return true;
            if (_motion == AttackMotion.Self) return true;
            return false;
        }

        internal static bool IsSummonMotion(AttackMotion _motion)
        {
            if (_motion == AttackMotion.SummonAndShort) return true;
            if (_motion == AttackMotion.SummonAndLong) return true;
            return false;
        }
    }
}