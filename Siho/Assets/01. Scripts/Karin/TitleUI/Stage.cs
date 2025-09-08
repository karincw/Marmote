using UnityEngine;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public StageSO stageData;

    public bool canSelect
    {
        set
        {
            _enableImage.SetActive(!value);
            _button.interactable = value;
        }
    }
    private Button _button;
    private GameObject _enableImage;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _enableImage = transform.Find("enableImage").gameObject;
    }

}
