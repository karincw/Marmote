using System.Collections.Generic;
using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(menuName = "SO/Shy/Synergy/Synergy")]
    public class SynergySO : ScriptableObject
    {
        public string synergyName;
        [TextArea()]
        public string explain;
        public Sprite icon;
        public Color outlineColor = Color.white;
        public bool showNum = false;

        public List<SynergyEffect> synergies;
    }
}
