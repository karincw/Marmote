using System.Collections.Generic;
using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(menuName = "SO/Shy/Skill/Value")]
    public class ValueSkillSO : SkillEventSO
    {
        [Header("Data")]
        public EventType eventType;
        public string formula;

        public List<GetStat> getStats;

        public override void UseSkill(Character _user, Character _target)
        {
            string data = formula;

            //statÀ» ¾ò°í
            for (int i = 0; i < getStats.Count; i++)
            {
                Character c = getStats[i].target == ActionWay.Self ? _user : _target;
                int v = c.GetStat(getStats[i].stat);

                data = data.Replace(getStats[i].key, v.ToString());
            }

            Debug.Log(data);

            int value = int.Parse(Formula.GetFormula(data));

            if (_user.bonusAtk != 0) value = Mathf.RoundToInt(value * _user.bonusAtk * 0.01f);

            _target.OnSkillEvent(value, eventType);
        }
    }
}
