using System.Collections.Generic;
using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(menuName = "SO/Shy/Skill/Events")]
    public class SkillSO : ScriptableObject
    {
        public List<SkillEventSO> skills;
    }
}
