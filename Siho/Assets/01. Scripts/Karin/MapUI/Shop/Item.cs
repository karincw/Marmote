using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public ItemDataSO data;

    private Image _image;
    private AnimationButton _button;

    private CanvasGroup _infoGroup;
    private TMP_Text _infoText;

    private void Awake()
    {
        _image = transform.Find("Image").GetComponent<Image>();
        _button = transform.Find("BuyButton").transform.Find("Button").GetComponent<AnimationButton>();
        _infoGroup = transform.Find("Info").GetComponent<CanvasGroup>();
        _infoText = transform.Find("Info").Find("InfoText").GetComponent<TMP_Text>();
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

    public void OpenInfo()
    {
        _infoText.text = data.itemDescription;
        _infoGroup.DOFade(1, 0.3f);
    }

    public void CloseInfo()
    {
        _infoGroup.DOFade(0, 0.3f);
    }

    private void HandleBuy()
    {
        if(MapManager.instance.money.Value < data.price)
        {
            Debug.Log("´Ô µ·¾øÀ½");
            return;
        }

        _button.interactable = false;

        switch (data.ItemType)
        {
            case ItemType.Red_Injecter:
                Shy.PlayerManager.Instance.AddSynergy(Shy.SynergyType.Blood);
                break;
            case ItemType.Blue_Injecter:
                Shy.PlayerManager.Instance.AddSynergy(Shy.SynergyType.Cool);
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