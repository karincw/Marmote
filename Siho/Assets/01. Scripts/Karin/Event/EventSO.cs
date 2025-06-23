using UnityEngine;

namespace karin
{
    public abstract class EventSO : ScriptableObject
    {
        [TextArea(0, 5)] public string eventScript;
        public Sprite eventImage;

        public abstract int GetBranchCount();
        public abstract string GetBranchName(int index);
        public abstract void Play(int index);
    }
}