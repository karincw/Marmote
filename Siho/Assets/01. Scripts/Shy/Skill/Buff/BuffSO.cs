using UnityEngine;

namespace Shy.Unit
{
    [CreateAssetMenu(fileName = "BuffSO", menuName = "SO/Shy/Buff")]
    public class BuffSO : ScriptableObject
    {
        public string itemName;
        [TextArea] public string explain;
        public BuffType buffType;
        public Sprite sprite;
        public BuffUseCondition useCondition;
        public BuffRemoveCondition removeCondition;
    }
}