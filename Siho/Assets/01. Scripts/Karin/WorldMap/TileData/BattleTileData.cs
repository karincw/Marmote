using karin.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using EnemySO = Shy.EnemySO;

namespace karin.worldmap
{
    [CreateAssetMenu(menuName = "SO/karin/tiles/BattleTileDataSO")]
    public class BattleTileDataSO : TileDataSO
    {
        public override void Play()
        {
            var enemys = WorldMapManager.Instance.GetBattleEnemyDatas(Random.Range(2, 4));
            DataStruct<EnemySO> eData = new DataStruct<EnemySO>();
            eData.list = enemys;
            DataLinkManager.Instance.WriteEnemyData(eData);
            SceneManager.LoadScene("Battle Proto");
        }

    }
}