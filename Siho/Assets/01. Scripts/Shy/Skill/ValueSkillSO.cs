using System;
using System.Collections.Generic;
using System.Data;
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

            //stat을 얻고
            for (int i = 0; i < getStats.Count; i++)
            {
                Character c = getStats[i].target == ActionWay.Self ? _user : _target;
                int v = c.GetStat(getStats[i].stat);

                data = data.Replace(getStats[i].key, v.ToString());
            }

            //구현해놓은 Formula에서 값 설정
            int value = int.Parse(Formula.GetFormula(data));

            if (_user.GetNowStr() != 0) value += Mathf.RoundToInt(value * _user.GetNowStr() * 0.01f);

            _target.OnValueEvent(value, eventType);
        }
    }
}
