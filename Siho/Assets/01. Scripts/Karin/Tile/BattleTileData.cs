using UnityEngine;

namespace karin.worldmap
{
    [CreateAssetMenu(menuName = "SO/karin/tiles/BattleTileDataSO")]
    public class BattleTileDataSO : TileDataSO
    {
        [SerializeField, Range(1, 3)] private int enemyCount = 1;
        public override void Play()
        {
            if(tileType == TileType.Boss)
            {
                DataLinkManager.Instance.isBossStage = true;
            }
            var enemys = WorldMapManager.Instance.GetBattleEnemyDatas(enemyCount);
            DataLinkManager.Instance.SetEnemyData(enemys);
            SceneChanger.Instance.LoadScene("Battle Proto");
        }
    }
}