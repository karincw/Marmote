using Shy.Unit;
using System.Collections.Generic;
using UnityEngine;

namespace karin.worldmap
{
    [CreateAssetMenu(menuName = "SO/karin/tiles/BossTileDataSO")]
    public class BossTileDataSO : TileDataSO
    {
        [SerializeField] private List<EnemySO> _boss;
        public override void Play()
        {
            DataLinkManager.Instance.isBossStage = true;
            DataLinkManager.Instance.SetEnemyData(_boss);
            SceneChanger.Instance.LoadScene("Battle Proto");
        }
    }
}