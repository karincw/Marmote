using System.Collections.Generic;
using UnityEngine;

namespace Shy.Unit
{
    [CreateAssetMenu(menuName = "SO/Shy/Skill/Normal")]
    public class NormalSkillSO : SkillSOBase
    {
        public List<SkillEventSO> skills = new List<SkillEventSO>(3);

        [Header("User")]
        public Sprite userAnime;
        public Sprite teamEffect;

        [Header("Opponent")]
        public Sprite opponentEffect;

        [Header("Summon")]
        public Sprite summon;
        public Sprite summonAnime;

        public override SkillEventSO GetSkill(int _num) => skills[_num];

        public override Sprite GetSkillMotion() => userAnime;

        public override List<SkillEventSO> GetSkills() => skills;
    }
}
