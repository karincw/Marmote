using UnityEngine;

namespace Shy.Unit
{
    public abstract class SkillEventSO : ScriptableObject
    {
        [Tooltip("None = Dice")] public ActionWay actionWay;
        public TargetWay targetTeam;
    }
}
