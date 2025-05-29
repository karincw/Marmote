
using karin.ui;
using karin.worldmap;
using Shy;
using Shy.Unit;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
        public Queue<EventSO> events;
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

    [System.Serializable]
    public struct IntPair
    {
        public int first;
        public int second;
    }

    [System.Serializable]
    public struct RunSaveData
    {
        public int RunIndex;
        public int playTime;
        public int theme;
        public int stageIndex;
        public int stagePosition;
        public int[] tileData;
        public int coin;
        public int diceCount;
        public bool isBattle;
        public List<DimensionData> playerMinions;
        public List<DimensionData> diceDatas;
    }
    [System.Serializable]
    public struct GameSaveData
    {
        public int gem;
        public float masterVolume;
        public float fXVolume;
        public float effectVolume;
        public bool[] characterLock;
        public List<string> saves;
    }

    [System.Serializable]
    public struct DimensionData
    {
        public int[] value;
    }

}