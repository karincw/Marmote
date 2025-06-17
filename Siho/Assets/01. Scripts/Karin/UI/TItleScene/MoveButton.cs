using UnityEngine;
using UnityEngine.UI;
namespace karin
{
    public class MoveButton : MonoBehaviour
    {
        public string targetSceneName;
        protected Button _btn;

        protected virtual void Awake()
        {
            _btn = GetComponent<Button>();
            _btn.onClick.AddListener(SceneChange);
        }

        protected void OnDestroy()
        {
            _btn.onClick.RemoveListener(SceneChange);
        }

        public virtual void SceneChange()
        {
            SceneChanger.Instance.LoadScene(targetSceneName);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
