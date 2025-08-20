using UnityEngine;

namespace Shy.Event
{
    [CreateAssetMenu(menuName = "SO/Shy/EventEnum/Synergy")]
    public class Item_Synergy : EventItemSO
    {
        public SynergySO item;

        public override string GetName() => item.synergyName;
    }
}