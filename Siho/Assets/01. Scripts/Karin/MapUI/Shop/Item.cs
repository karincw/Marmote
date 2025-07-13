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

        Debug.Log("아이템 구매시 로직 추가");
    }
}