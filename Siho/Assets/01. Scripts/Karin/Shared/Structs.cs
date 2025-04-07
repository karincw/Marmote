
using karin.worldmap;
using System.Collections.Generic;

namespace karin
{
    [System.Serializable]
    public struct TileChangeData
    {
        public TileDataSO ChangeTile;
        public int changeCount;
    }

    [System.Serializable]
    public struct StageEnemyList
    {
        public List<EnemyDataSO> enemyList; 

        public StageEnemyList(List<EnemyDataSO> _enemyList)
        {
            enemyList = _enemyList;
        }
    }

    [System.Serializable]
    public struct MapData
    {
        public int stageIndex;
        public int positionIndex;
        public List<TileDataSO> tileData;
        public Theme stageTheme;
    }

    [System.Serializable]
    public struct EnemyData
    {

    }
}