using System.Collections.Generic;
using UnityEngine;

namespace karin.worldmap
{
    [CreateAssetMenu(menuName = "SO/karin/tiles/BattleTileDataSO")]
    public class BattleTileDataSO : TileDataSO
    {
        public override void Play()
        {
            var enemys = WorldMapManager.Instance.GetBattleEnemyDatas();
            Debug.Log($"{enemys[0]}{enemys[1]}{enemys[2]}");
        }

    }
}