using UnityEngine;

namespace Shy.Event
{
    [CreateAssetMenu(menuName = "SO/Shy/EventEnum/Money")]
    public class Item_Money : EventItemSO
    {
        public Sprite moneyIcon;

        public override Sprite GetIcon() => moneyIcon;

        public override string GetName() => "ÄÚÀÎ";
    }
}
