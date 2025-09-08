using UnityEngine;

[CreateAssetMenu(menuName = "SO/karin/ItemData")]
public class ItemDataSO : ScriptableObject
{
    public string itemName;
    [TextArea(2,7)]public string itemDescription;
    public ItemType ItemType;
    public Sprite image;
    public int price;
}
