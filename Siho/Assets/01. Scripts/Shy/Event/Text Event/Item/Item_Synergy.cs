using UnityEngine;

namespace Shy.Event
{
    [CreateAssetMenu(menuName = "SO/Shy/EventEnum/Synergy")]
    public class Item_Synergy : EventItemSO
    {
        public SynergySO item;
        public bool badSynergy = false;

        public override string GetName() => item.synergyName;
        public override Sprite GetIcon() => item.icon;
    }
}