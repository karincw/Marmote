using UnityEngine;

namespace Shy.Event
{
    [CreateAssetMenu(menuName = "SO/Shy/EventResult/None")]
    public class EventResultSO : ScriptableObject
    {
        [TextArea()]
        public string message;
    }
}
