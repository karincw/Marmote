using UnityEngine;

namespace karin.ui
{
    public abstract class EventSO : ScriptableObject
    {
        [Multiline(3)] public string EventChat;
        public Sprite eventSprite;
        public abstract int branchCount { get; }

        public abstract void Play(int index);
    }
}