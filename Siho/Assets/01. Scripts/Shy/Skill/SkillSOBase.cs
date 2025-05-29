using System.Collections.Generic;
using UnityEngine;

namespace Shy.Unit
{
    public abstract class SkillSOBase : ScriptableObject
    {
        [Header("Data")]
        public string skillName;
        [TextArea]
        public string explian;
        public AttackMotion motion;

        public abstract SkillEventSO GetSkill(int _v);
        public abstract List<SkillEventSO> GetSkills();
        public abstract Sprite GetSkillMotion();
    }
}
