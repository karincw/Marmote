using System.Collections.Generic;
using UnityEngine;

namespace Shy
{
    public abstract class SkillEventSO : ScriptableObject
    {
        [Tooltip("None = Dice")]public ActionWay actionWay;
        public Team targetTeam;

        public abstract void UseSkill(Character _user, Character _target);
    }
}
