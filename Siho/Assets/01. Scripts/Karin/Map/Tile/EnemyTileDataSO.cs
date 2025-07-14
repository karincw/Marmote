using Shy;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/karin/TileData/Enemy")]
public class EnemyTileDataSO : TileDataSO
{
    public override void EnterEvent()
    {
        BattleManager.Instance.InitBattle(DataLinkManager.instance.characterData, DataLinkManager.instance.GetEnemy());
    }
}
