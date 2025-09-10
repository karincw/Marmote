using TMPro;
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

    private Image _image;
    private TMP_Text _text;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _enableImage = transform.Find("enableImage").gameObject;

        _image = transform.Find("Image").GetComponent<Image>();
        _text = GetComponentInChildren<TMP_Text>();

        _image.sprite = stageData.stageImage;
        _text.text = stageData.stageName;
    }

}
