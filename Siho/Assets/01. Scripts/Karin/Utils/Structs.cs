using Shy;
using Shy.Unit;
using System.Collections.Generic;
using UnityEngine;

namespace karin
{
    [System.Serializable]
    public struct TileChangeData
    {
        public TileType ChangeTile;
        public int changeCount;
    }

    [System.Serializable]
    public struct SerializeEnemyList
    {
        public List<EnemySO> list;
    }

    [System.Serializable]
    public struct SerializeEventList
    {
        public List<EventSO> list;
    }

    [System.Serializable]
    public struct SaveChartacterData
    {
        public CharacterType type;
        public int maxHp;
        public int hp;
        public int strength;
        public int defence;
        public SaveDiceData diceData;
    }

    [System.Serializable]
    public struct Pair<T1, T2>
    {
        public T1 first;
        public T2 Second;
    }

    [System.Serializable]
    public struct EventStatBranchData
    {
        public string branchName;
        public bool usedByRandomCharacterIndex;
        [Range(0, 2)] public int characterIndex;
        public Stat statModify;
        [TextArea(0, 5)] public string feedbackScript;
    }
    [System.Serializable]
    public struct EventDiceBranchData
    {
        public string branchName;
        public bool usedByRandomCharacterIndex;
        [Range(0, 2)] public int characterIndex;
        public bool usedByRandomEyeIndex;
        [Range(0, 5)] public int eyeIndex;
        public ActionWay wayModify;
        [TextArea(0, 5)] public string feedbackScript;
    }

    [System.Serializable]
    public struct SaveDiceData
    {
        public List<Pair<int, ActionWay>> eyes;
    }

    [System.Serializable]
    public struct GameData
    {
        public bool load;
        ///잼
        public int Gem;
        ///카드 해금
        public List<bool> cardLockData;
    }

    [System.Serializable]
    public struct RunData
    {
        public bool load;
        public int runIndex;
        ///스테이지 인덱스
        public int stageIndex;
        public Theme stageTheme;
        ///위치 정보
        public int position;
        ///타일 정보
        public TileType[] tileData;
        ///코인
        public int coin;
        ///캐릭터
        public SaveChartacterData[] characterType;
    }

}