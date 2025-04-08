using System.Collections.Generic;
using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(menuName = "SO/Shy/Skill/Events")]
    public class SkillSO : ScriptableObject
    {
        [Header("Info")]
        public string skillName;
        [TextArea]
        public string explian;

        public List<SkillEventSO> skills;
    }
}
