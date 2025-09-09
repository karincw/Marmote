using UnityEngine;

namespace Shy.Event
{
    [CreateAssetMenu(menuName = "SO/Shy/EventEnum/Stat")]
    public class Item_Stat : EventItemSO
    {
        [SerializeField] private string statName;
        public MainStatEnum statType;
        public Sprite statIcon;

        public override string GetName() => statName;

        public override Sprite GetIcon() => statIcon;
    }
}