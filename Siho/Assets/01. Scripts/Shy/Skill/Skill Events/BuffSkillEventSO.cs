using UnityEngine;

namespace Shy.Unit
{
    [CreateAssetMenu(menuName = "SO/Shy/SkillEvent/Buff")]
    public class BuffSkillEventSO : SkillEventSO
    {
        public BuffType bufftype;
        public int life = 0;
    }
}
