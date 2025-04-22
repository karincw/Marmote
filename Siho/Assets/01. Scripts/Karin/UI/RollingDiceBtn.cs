using karin.worldmap;
using UnityEngine;
using UnityEngine.UI;
namespace karin.ui
{
    public class RollingDiceBtn : MonoBehaviour
    {
        private Button _button;
        private Floor _floor;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _floor = FindFirstObjectByType<Floor>();
            _floor.OnDiceStopEvent += HandleDiceStop;
            _button.onClick.AddListener(() => _button.interactable = false);
        }

        private void OnDestroy()
        {
            _floor.OnDiceStopEvent -= HandleDiceStop;
            _button.onClick.RemoveAllListeners();
        }

        private void HandleDiceStop(int cumber)
        {
            _button.interactable = true;
        }

    }
}