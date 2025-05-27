
using karin.worldmap;
using Shy;
using Shy.Unit;
using System.Collections.Generic;
using UnityEngine;

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
        public List<EnemySO> enemyList;

        public StageEnemyList(List<EnemySO> _enemyList)
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
    public struct DataStruct<T> where T : ScriptableObject
    {
        public List<T> list;
    }

    [System.Serializable]
    public struct StatUpData<T> where T : struct
    {
        [Tooltip("0부터 시작")]
        public int index;
        public string branchName;
        public List<T> branchActions;
    }

    [System.Serializable]
    public struct StatChangeData
    {
        public StatEnum targetStat;
        public int statIncresement;
    }

    [System.Serializable]
    public struct DiceChangeData
    {
        public ActionWay changeWay;
        public int eyeIndex;
    }

}