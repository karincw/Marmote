using UnityEngine;

[CreateAssetMenu(menuName = "SO/karin/ItemData")]
public class ItemDataSO : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public ItemType ItemType;
    public Sprite image;
    public int price;
}
