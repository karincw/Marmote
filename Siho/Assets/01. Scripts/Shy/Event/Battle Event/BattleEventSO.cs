using UnityEngine;

namespace Shy.Event
{
    [CreateAssetMenu(fileName = "BattleEventSO", menuName = "SO/Shy/Battle/Event")]
    public class BattleEventSO : ScriptableObject
    {
        public BattleEventType eventType;
        [TextArea()] public string successMes, failMes;
    }
}