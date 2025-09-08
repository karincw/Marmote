using UnityEngine;

[CreateAssetMenu(menuName = "SO/karin/TileData/End")]
public class EndTileDataSO : TileDataSO
{
    public override void ThrowEvent()
    {
        base.ThrowEvent();
        FindFirstObjectByType<EndingPanel>().Open(true);
    }
}
