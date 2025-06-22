using System;
using UnityEngine;
using UnityEngine.UI;

namespace karin
{
    [RequireComponent(typeof(Button))]
    public class GameExitButton : MonoBehaviour
    {
        public static Action OnExitGame;

        [SerializeField] private WarningPanel _warningPanel;
        [SerializeField] private string _warningText = "경고 : 저장하지 않은 데이터는 사라집니다.";
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }
        private void OnEnable()
        {
            _button.onClick.AddListener(OpenWarningPanel);
        }
        private void OnDisable()
        {
            _button.onClick.RemoveListener(OpenWarningPanel);
        }

        private void OpenWarningPanel()
        {
            _warningPanel.Open(_warningText, () =>
            {
                OnExitGame?.Invoke();
                Application.Quit();
            });
        }
    }
}