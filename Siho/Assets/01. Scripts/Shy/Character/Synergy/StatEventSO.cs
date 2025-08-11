using UnityEngine;

namespace Shy
{

    [CreateAssetMenu(menuName = "SO/Shy/Synergy/Stat")]
    public class StatEventSO : SynergyEventSO
    {
        public SubStatEnum subStat;
        public Calculate calculate;
        public float value;
    }
}