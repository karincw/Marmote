using karin.Core;
using System.Collections.Generic;
using UnityEngine;

namespace karin.worldmap
{
    [CreateAssetMenu(menuName = "SO/karin/tiles/BattleTileDataSO")]
    public class BattleTileDataSO : TileDataSO
    {
        public override void Play()
        {
            var enemys = WorldMapManager.Instance.GetBattleEnemyDatas(Random.Range(2, 4));
            EnemyData eData = new EnemyData();
            eData.enemyList = enemys;
            DataManager.Instance.WriteEnemyData(eData);
            //æ¿ ¿Ãµø
        }

    }
}