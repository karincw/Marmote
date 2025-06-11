using karin.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using EnemySO = Shy.Unit.EnemySO;

namespace karin.worldmap
{
    [CreateAssetMenu(menuName = "SO/karin/tiles/BattleTileDataSO")]
    public class BattleTileDataSO : TileDataSO
    {
        public override void Play()
        {
            var enemys = WorldMapManager.Instance.GetBattleEnemyDatas(Random.Range(1, 4));
            DataStruct<EnemySO> eData = new DataStruct<EnemySO>();
            eData.list = enemys;
            DataLinkManager.Instance.SaveEnemyData(eData);
            SceneChanger.Instance.LoadScene("Battle Proto");
        }

    }
}