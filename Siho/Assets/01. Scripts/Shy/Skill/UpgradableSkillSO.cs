using System.Collections.Generic;
using UnityEngine;

namespace Shy.Unit
{
    [CreateAssetMenu(menuName = "SO/Shy/Skill/Upgradble")]
    public class UpgradableSkillSO : SkillSOBase
    {
        [Header("Upgradable")]
        private int level;
        public List<NormalSkillSO> so;
        private UpgradeType upgradeType;

        public override SkillEventSO GetSkill(int _num)
        {
            return null;
        }

        public override List<SkillEventSO> GetSkills()
        {
            return null;
        }

        public override Sprite GetSkillMotion()
        {
            return null;
        }
    }
}

