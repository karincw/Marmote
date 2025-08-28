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
        if(MapManager.instance.money.Value < data.price)
        {
            Debug.Log("´Ô µ·¾øÀ¸¼À ¤»¤»");
            return;
        }

        _button.interactable = false;

        switch (data.ItemType)
        {
            case ItemType.None:
                break;
            case ItemType.Red_Injecter:
                Shy.PlayerManager.Instance.AddSynergy(Shy.SynergyType.Blood);
                break;
            case ItemType.Blue_Injecter:
                Shy.PlayerManager.Instance.AddSynergy(Shy.SynergyType.Blood);
                break;
            case ItemType.Yellow_Injecter:
                Shy.PlayerManager.Instance.AddSynergy(Shy.SynergyType.Strong);
                break;
            case ItemType.Purple_Injecter:
                Shy.PlayerManager.Instance.AddSynergy(Shy.SynergyType.Fear);
                break;
            case ItemType.Green_Injecter:
                Shy.PlayerManager.Instance.AddSynergy(Shy.SynergyType.Spine);
                break;
            case ItemType.Grey_Injecter:
                Shy.PlayerManager.Instance.AddSynergy(Shy.SynergyType.Steel);
                break;
            default:
                Debug.LogWarning("È¿°ú¾È³ÖÀ½");
                break;
        }

        MapManager.instance.money.Value -= data.price;

    }
}