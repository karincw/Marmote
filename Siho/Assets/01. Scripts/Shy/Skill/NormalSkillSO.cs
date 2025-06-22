using System.Collections.Generic;
using UnityEngine;
using Shy.Anime;

namespace Shy.Unit
{
    [CreateAssetMenu(menuName = "SO/Shy/Skill/Normal")]
    public class NormalSkillSO : SkillSOBase
    {
        [SerializeField] private List<SkillEventSO> skills = new List<SkillEventSO>();

        [Header("User")]
        public SkillMotion motion;
        public Sprite userAnime;
        public Sprite teamEffect;

        [Header("Opponent")]
        public Sprite opponentEffect;

        [Header("Summon")]
        public Sprite summon;
        public Sprite summonAnime;

        public override List<SkillEventSO> GetSkills(Character _user) => skills;

        public override Sprite GetMotionSprite(AnimeType _type, Character _user = null)
        {
            switch (_type)
            {
                case AnimeType.UserAnime:       return userAnime;
                case AnimeType.TeamEffect:      return teamEffect;
                case AnimeType.OpponentEffect:  return opponentEffect;
                case AnimeType.SummonVisual:    return summon;
                case AnimeType.SummonAnime:     return summonAnime;
            }
            return null;
        }

        public override SkillMotion GetSkillMotion(Character _user) => motion;
    }
}
