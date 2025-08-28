using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SetData);
        SceneManager.sceneLoaded += HandleSceneLoaded;
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(SetData);
        SceneManager.sceneLoaded -= HandleSceneLoaded;
    }

    private void HandleSceneLoaded(Scene scene, LoadSceneMode loadMode)
    {
        if (scene.name == "Title")
        {
            _button.onClick.AddListener(() => SceneChanger.instance.LoadScene("Map"));
        }
    }

    private void SetData()
    {
        DataLinkManager.instance.SetCharacterData();
    }
}
