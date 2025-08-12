using UnityEngine;

namespace Shy.Event
{
    [CreateAssetMenu(fileName = "BattleEventSO", menuName = "SO/Shy/Battle/Event")]
    public class BattleEventSO : ScriptableObject
    {
        public BattleEvent eventType;
        [TextArea()] public string successMes, failMes;
    }
}