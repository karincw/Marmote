namespace Shy.Anime
{
    public class AnimeBool
    {
        public static bool IsPetMotion(SkillMotion _motion)
        {
            if (_motion == SkillMotion.PetSelf) return true;
            if (_motion == SkillMotion.PetAndShort) return true;
            if (_motion == SkillMotion.PetAndLong) return true;
            return false;
        }

        internal static bool IsTeamMotion(SkillMotion _motion)
        {
            if (_motion == SkillMotion.PetSelf) return true;
            if (_motion == SkillMotion.Self) return true;
            return false;
        }

        internal static bool IsSummonMotion(SkillMotion _motion)
        {
            if (_motion == SkillMotion.SummonAndShort) return true;
            if (_motion == SkillMotion.SummonAndLong) return true;
            return false;
        }
    }
}