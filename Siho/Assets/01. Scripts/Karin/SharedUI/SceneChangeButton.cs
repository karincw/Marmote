using System;
using UnityEngine;
using UnityEngine.UI;

namespace karin
{
    [RequireComponent(typeof(Button))]
    public class SceneChangeButton : MonoBehaviour
    {
        private Button _button;
        [SerializeField] private string _targetSceneName;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(SceneChange);
        }
        private void OnDisable()
        {
            _button.onClick.RemoveListener(SceneChange);
        }

        private void SceneChange()
        {
            SceneChanger.Instance.LoadScene(_targetSceneName);
        }
    }
}