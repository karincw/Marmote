using Shy;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/karin/TileData/Shop")]
public class ShopTileDataSO : TileDataSO
{
    public override void EnterEvent()
    {
        FindFirstObjectByType<ShopPanel>().Open();
    }
}
