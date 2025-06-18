using System;
using UnityEngine;
using UnityEngine.UI;

namespace karin
{
    [RequireComponent(typeof(Button))]
    public class GameExitButton : MonoBehaviour
    {
        private Button _button;
        public static Action OnExitGame;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(() =>
            {
                OnExitGame?.Invoke();
                Application.Quit();
            });
        }
    }
}