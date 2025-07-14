using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SetData);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(SetData);
    }

    private void SetData()
    {
        DataLinkManager.instance.SetCharacterData();
    }
}
