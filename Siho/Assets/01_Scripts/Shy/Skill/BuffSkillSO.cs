using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(menuName = "SO/Shy/Skill/Buff")]
    public class BuffSkillSO : SkillEventSO
    {
        public BuffEvent type;
        public int cnt = 0;

        public override void UseSkill(Character _user, Character _target)
        {
            Buff buff = Pooling.Instance.Use(PoolingType.Buff).GetComponent<Buff>();
            buff.Init(type, cnt);

            _target.buffs.Add(buff);
        }
    }
}
