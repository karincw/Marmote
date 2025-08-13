using System.Collections.Generic;
using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(menuName = "SO/Shy/Synergy/Synergy")]
    public class SynergySO : ScriptableObject
    {
        public string synergyName;
        public SynergyType synergyType;
        [TextArea()]
        public string explain;
        public Sprite icon;
        public bool showNum = false;

        public List<SynergyEventSO> synergies;
    }
}
