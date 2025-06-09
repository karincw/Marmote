using System.Collections.Generic;
using UnityEngine;

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

        public override Sprite GetMotionSprite(Character _user) => userAnime;

        public override SkillMotion GetSkillMotion(Character _user) => motion;
    }
}
