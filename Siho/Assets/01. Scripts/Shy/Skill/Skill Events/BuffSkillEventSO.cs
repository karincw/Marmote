using UnityEngine;

namespace Shy.Unit
{
    [CreateAssetMenu(menuName = "SO/Shy/SkillEvent/Buff")]
    public class BuffSkillEventSO : SkillEventSO
    {
        public BuffType type;
        public int life = 0;

        public override void UseSkill(Character _user, Character _target)
        {
            Buff buff = Pooling.Instance.Use(PoolingType.Buff, _user.buffGroup).GetComponent<Buff>();
            buff.Init(_target, type, life);
            buff.gameObject.SetActive(true);

            _target.buffs.Add(buff);
        }
    }
}
