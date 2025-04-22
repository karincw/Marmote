
using karin.worldmap;
using Shy;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        
    }

}