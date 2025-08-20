using UnityEngine;

namespace Shy.Event
{
    [CreateAssetMenu(menuName = "SO/Shy/EventEnum/Stat")]
    public class Item_Stat : EventItemSO
    {
        [SerializeField] private string statName;
        public MainStatEnum statType;

        public override string GetName() => statName;
    }
}