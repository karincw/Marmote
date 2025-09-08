using System.Collections.Generic;
using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(menuName = "SO/Shy/Synergy/Stat")]
    public class StatEventSO : SynergyEventSO
    {
        public SubStatEnum subStat;
        public Calculate calculate;
        public List<StatSynergyValue> effectByLevel;
        public string valueSign = "n";

        public virtual float GetData(int lv)
        {
            foreach (var item in effectByLevel)
            {
                if (item.level == lv) return item.value;
            }

            Debug.Log("Not Found Synergy Effect\n-> Return 0");
            return 0;
        }
    }
}