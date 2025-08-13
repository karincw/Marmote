using System.Collections.Generic;
using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(menuName = "SO/Shy/Synergy/Characteristic")]
    public class SpecialEventSO : SynergyEventSO
    {
        public CharacteristicEnum characteristic;
        public List<SpecialSynergyValue> effectByLevel;

        public bool GetData(int lv)
        {
            foreach (var item in effectByLevel)
            {
                if (item.level == lv) return item.value;
            }

            Debug.Log("Not Found Synergy Effect\n-> Return false");
            return false;
        }
    }
}