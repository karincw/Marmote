using System.Collections.Generic;
using UnityEngine;
using Shy.Anime;

namespace Shy.Unit
{
    public abstract class SkillSOBase : ScriptableObject
    {
        [Header("Data")]
        public string skillName;
        [TextArea]
        public string explian;

        public abstract List<SkillEventSO> GetSkills(Character _user);
        public abstract Sprite GetMotionSprite(AnimeType _type, Character _user);
        public abstract SkillMotion GetSkillMotion(Character _user);
    }
}
