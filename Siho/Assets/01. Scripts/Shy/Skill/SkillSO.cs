using System.Collections.Generic;
using UnityEngine;

namespace Shy.Unit
{
    [CreateAssetMenu(menuName = "SO/Shy/Skill/Events")]
    public class SkillSO : ScriptableObject
    {
        [Header("Info")]
        public string skillName;
        public Sprite summon;
        public Sprite summonAnime;
        public AttackMotion motion;
        [TextArea]
        public string explian;

        public List<SkillEventSO> skills;
    }
}
