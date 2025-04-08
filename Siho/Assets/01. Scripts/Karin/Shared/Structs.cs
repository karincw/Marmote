
using karin.worldmap;
using Shy;
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
        public List<CharacterSO> enemyList; 

        public StageEnemyList(List<CharacterSO> _enemyList)
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
        public List<CharacterSO> enemyList;
    }
}