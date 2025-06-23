using UnityEngine;
using UnityEngine.UI;

namespace karin
{
    public class SaveLoadPanel : FadableUI
    {
        [SerializeField] private Button[] _slotButtons;
        [SerializeField] private SaveLoadInfoView _infoView;

        protected override void Awake()
        {
            base.Awake();
            for (int i = 0; i < _slotButtons.Length; i++)
            {
                int capture = i;
                _slotButtons[i].onClick.AddListener(() => HandleOpenDescription(capture));
            }
        }

        private void HandleOpenDescription(int index)
        {
            _infoView.ViewDescription(index);
        }

        public void NewPlayMode()
        {
            _infoView.NewPlayMode();
        }
        public void LoadPlayMode()
        {
            _infoView.LoadPlayMode();
        }
        public void WorldMapMode()
        {
            _infoView.WorldMapMode();
        }
    }
}
