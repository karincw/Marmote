using System.Collections.Generic;

namespace Shy.Unit
{
    public abstract class SkillEvent
    {
        public TargetData targetData;
        protected List<Character> targets;

        public abstract void UseEvent();
        public List<Character> GetTargets()
        {
            targets = BattleManager.Instance.GetTargets(targetData);
            return targets;
        }
    }

    public class ValueEvent : SkillEvent
    {
        private ValueEventSO eventSO;

        public ValueEvent(TargetData _t, ValueEventSO _v)
        {
            targetData = _t;
            eventSO = _v;
        }

        public override void UseEvent()
        {
            foreach (Character _target in targets)
            {
                int _value = eventSO.GetValue(targetData.user, _target);
                _target.OnValueEvent(_value, eventSO.eventType, false);
            }
        }
    }

    public class BuffEvent : SkillEvent
    {
        public BuffSO buffData;
        public int value;

        public BuffEvent(TargetData _t, int _v, BuffSO _b)
        {
            targetData = _t;
            value = _v;
            buffData = _b;
        }

        public override void UseEvent()
        {
            foreach (Character _target in targets) _target.OnBuffEvent(this);
        }
    }
}
