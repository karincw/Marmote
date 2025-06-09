using System.Collections.Generic;
using UnityEngine;

namespace Shy.Unit
{
    [CreateAssetMenu(menuName = "SO/Shy/SkillEvent/Value")]
    public class ValueEventSO : SkillEventSO
    {
        [Header("Data")]
        public EventType eventType;
        [TextArea()]public string formula;
        [Range(0, 100)] public int ignoreDefPer = 0;

        public List<GetStat> getStats;
        public List<GetStack> getStacks;

        public int GetValue(Character _user, Character _target)
        {
            string data = formula;

            //stat을 얻고
            foreach (GetStat stat in getStats)
            {
                Character c = (stat.self) ? _user : _target;
                data = data.Replace(stat.key, c.GetNowStat(stat.stat).ToString());
            }

            foreach (GetStack stack in getStacks)
            {
                int v = _user.GetStackCnt(stack.buff);
                data = data.Replace(stack.key, v.ToString());
            }

            //구현해놓은 Formula에서 값 설정
            float value = float.Parse(Formula.GetFormula(data)), bAtk = _user.GetBonusStat(StatEnum.AdditionalDmg);

            if (eventType == EventType.AttackEvent && bAtk != 0) value += value * bAtk * 0.01f;

            return Mathf.RoundToInt(value);
        }
    }
}
