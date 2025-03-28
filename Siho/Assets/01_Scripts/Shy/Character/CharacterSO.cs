using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(menuName = "SO/Shy/Character")]
    public class CharacterSO : ScriptableObject
    {
        [Header("Stat")]
        public int hp;
        public int atk;
        public int spd;

        [Header("Other")]
        public SkillSO[] skills = new SkillSO[6];
    }
}
