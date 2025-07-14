using DG.Tweening;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/karin/TileData/NextStage")]
public class NextStageTileDataSO : TileDataSO
{
    public override void ThrowEvent() 
    {
        MapManager.instance.symbol.sequence.Pause();
        MapManager.instance.MakeMap();
    }
}
