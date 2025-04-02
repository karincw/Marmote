using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(menuName = "SO/Shy/Character")]
    public class CharacterSO : ScriptableObject
    {
        public Stat stats;

        public SkillSO[] skills = new SkillSO[3];
    }
}
