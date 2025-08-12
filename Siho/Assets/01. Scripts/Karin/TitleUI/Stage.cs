using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public StageSO stageData;

    public bool canSelect
    {
        set
        {
            _button.interactable = value;
        }
    }
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

}
