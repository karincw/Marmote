using karin.worldmap;
using UnityEngine;
using UnityEngine.UI;

namespace karin.ui
{
    public class RollingDiceBtn : MonoBehaviour
    {
        private Button _button;
        private Symbol _symbol;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _symbol = FindFirstObjectByType<Symbol>();
            _symbol.OnMoveEndEvent += HandleDiceStop;
            _button.onClick.AddListener(() => _button.interactable = false);
        }

        private void OnDestroy()
        {
            _symbol.OnMoveEndEvent -= HandleDiceStop;
            _button.onClick.RemoveAllListeners();
        }

        private void HandleDiceStop()
        {
            _button.interactable = true;
        }

    }
}