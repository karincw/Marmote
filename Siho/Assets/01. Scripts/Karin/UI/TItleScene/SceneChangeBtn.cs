using UnityEngine;
using UnityEngine.UI;
namespace karin
{
    [RequireComponent(typeof(Button))]
    public class SceneChangeBtn : MonoBehaviour
    {
        [SerializeField] private string _targetSceneName;
        private Button _btn;

        private void Awake()
        {
            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(SceneChange);
        }

        private void OnDestroy()
        {
            _btn.onClick.RemoveListener(SceneChange);
        }

        private void SceneChange()
        {
            SceneChanger.Instance.LoadScene(_targetSceneName);
        }
    }
}
