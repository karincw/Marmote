using Shy.Unit;
using System;
using System.Collections.Generic;

namespace karin
{
    public class DataLinkManager : MonoSingleton<DataLinkManager>
    {
        public int Gem;
        public int Coin;

        public List<EnemySO> EnemyData { get; private set; }

        public void SaveEnemyData(List<EnemySO> enemys)
        {
            EnemyData = enemys;
        }
    }
}
