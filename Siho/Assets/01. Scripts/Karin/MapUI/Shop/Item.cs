using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemDataSO data;

    private Image _image;
    private AnimationButton _button;

    private void Awake()
    {
        _image = transform.Find("Image").GetComponent<Image>();
        _button = transform.Find("BuyButton").transform.Find("Button").GetComponent<AnimationButton>();
        _button.onClick.AddListener(HandleBuy);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(HandleBuy);
    }

    public void Init(ItemDataSO itemData)
    {
        data = itemData;
        _button.SetText($"{data.price} Cheese");
        _image.sprite = data.image;
        _button.interactable = true;
    }

    private void HandleBuy()
    {
        _button.interactable = false;

        switch (data.ItemType)
        {
            case ItemType.None:
                break;
            case ItemType.Red_Injecter:
                Debug.Log("blood추가");
                break;
            case ItemType.Blue_Injecter:
                Debug.Log("cool추가");
                break;
            case ItemType.Yellow_Injecter:
                Debug.Log("strong추가");
                break;
            case ItemType.Purple_Injecter:
                Debug.Log("fear추가");
                break;
            case ItemType.Green_Injecter:
                Debug.Log("spine추가");
                break;
            case ItemType.Grey_Injecter:
                Debug.Log("steel추가");
                break;
            default:
                break;
        }
    }
}