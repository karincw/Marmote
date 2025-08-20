using UnityEngine;

namespace Shy.Event
{
    [CreateAssetMenu(menuName = "SO/Shy/EventResult/Item")]
    public class Result_Item : EventResultSO
    {
        public EventItemSO item;
        public int value;
        public Calculate calc;
    }
}