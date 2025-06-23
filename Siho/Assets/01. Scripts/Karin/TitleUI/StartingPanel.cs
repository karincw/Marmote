using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace karin
{
    public class StartingPanel : FadableUI
    {
        public int SelectCount = 0;
        private readonly int MaxSelect = 3;

        [SerializeField] private Button _playBtn;
        private List<SelectCard> _selectCards;

        protected override void Awake()
        {
            base.Awake();
            SelectCount = 0;
            _selectCards = FindObjectsByType<SelectCard>(FindObjectsSortMode.None).ToList();
            _selectCards = _selectCards.OrderBy(c => c.SiblingIndex).ToList();
        }

        protected override void Start()
        {
            base.Start();
            SetCardLock(Load.Instance.GetGameData().cardLockData);
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

        public List<bool> GetCardLockData()
        {
            return _selectCards.Select(c => c.canPlay).ToList();
        }

        public void SetCardLock(List<bool> locks)
        {
            for (int i = 0; i < locks.Count; i++)
            {
                _selectCards[i].canPlay = locks[i];
            }
        }
    }
}