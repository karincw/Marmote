using System.Collections.Generic;
using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(menuName = "SO/Shy/Skill/Skill")]
    public class SkillEventSO : ScriptableObject
    {
        [Header("Info")]
        public string skillName;
        [TextArea]
        public string explian;

        [Header("Data")]
        public EventType eventType;
        public ActionWay actionWay;
        public string formula;
        public Team targetTeam;

        public List<GetStat> getStats;

        public void UseSkill(Character _user, Character _target)
        {
            string data = formula;

            //stat을 얻고
            for (int i = 0; i < getStats.Count; i++)
            {
                Character c = getStats[i].target == ActionWay.Self ? _user : _target;
                int v = c.GetStat(getStats[i].stat);

                data = data.Replace(getStats[i].key, v.ToString());
            }

            int value = int.Parse(Formula.GetFormula(data));

            _target.OnEvent(value, eventType);
        }
    }
}
