using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace karin
{
    public class StartingPanel : FadableUI
    {
        public int SelectCount = 0;
        private readonly int MaxSelect = 3;
        private readonly int MinSelect = 1;

        [SerializeField] private Button _playBtn;

        protected override void Awake()
        {
            base.Awake();
            SelectCount = 0;
        }

        public bool CanAdd()
        {
            return SelectCount < MaxSelect;
        }

        public void Add()
        {
            SelectCount++;

            _playBtn.interactable = true;
        }

        public void Remove()
        {
            SelectCount--;

            if (SelectCount == 0)
                _playBtn.interactable = false;
        }
    }
}