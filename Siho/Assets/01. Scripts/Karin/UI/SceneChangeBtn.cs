using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        SceneManager.LoadScene(_targetSceneName);
    }
}
