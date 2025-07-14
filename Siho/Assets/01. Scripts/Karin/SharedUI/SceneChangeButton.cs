using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SceneChangeButton : MonoBehaviour
{
    [SerializeField, Tooltip("Exit = GameQuit")] private string _name;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(Change);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(Change);
    }

    public void Change()
    {
        if (_name == "Exit")
        {
            Application.Quit();
            return;
        }
        SceneChanger.instance.LoadScene(_name);
    }

}
