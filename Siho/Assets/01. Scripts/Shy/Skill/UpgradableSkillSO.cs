using System.Collections.Generic;
using UnityEngine;

namespace Shy.Unit
{
    [CreateAssetMenu(menuName = "SO/Shy/Skill/Upgradble")]
    public class UpgradableSkillSO : SkillSOBase
    {
        [Header("Upgradable")]
        [SerializeField] private UpgradeCondition upgradeCondition;
        public List<NormalSkillSO> so;
        [SerializeField] private List<int> values;

        public override Sprite GetMotionSprite(Character _user) => so[GetLv(_user)].GetMotionSprite(null);

        public override List<SkillEventSO> GetSkills(Character _user) => so[GetLv(_user)].GetSkills(null);

        public override SkillMotion GetSkillMotion(Character _user) => so[GetLv(_user)].GetSkillMotion(null);

        public int GetLv(Character _user)
        {
            int maxLv = so.Count;

            switch (upgradeCondition)
            {
                case UpgradeCondition.SelfStack:

                    break;

                case UpgradeCondition.SelfHp:

                    break;
            }

            return 0;
        }

    }
}
