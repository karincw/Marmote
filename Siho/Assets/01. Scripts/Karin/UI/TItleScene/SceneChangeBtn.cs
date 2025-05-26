using UnityEngine;
using UnityEngine.UI;
namespace karin
{
    [RequireComponent(typeof(Button))]
    public class SceneChangeBtn : MonoBehaviour
    {
        [SerializeField] protected string _targetSceneName;
        protected Button _btn;

        private void Awake()
        {
            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(SceneChange);
        }

        private void OnDestroy()
        {
            _btn.onClick.RemoveListener(SceneChange);
        }

        protected virtual void SceneChange()
        {
            SceneChanger.Instance.LoadScene(_targetSceneName);
        }
    }
}
