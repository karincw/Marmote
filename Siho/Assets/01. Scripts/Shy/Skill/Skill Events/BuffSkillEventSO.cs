using UnityEngine;

namespace Shy.Unit
{
    [CreateAssetMenu(menuName = "SO/Shy/SkillEvent/Buff")]
    public class BuffSkillEventSO : SkillEventSO
    {
        public BuffSO buff;
        public int life = 0;
    }
}
