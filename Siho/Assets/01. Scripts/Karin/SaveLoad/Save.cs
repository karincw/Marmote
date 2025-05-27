using karin.Core;
using karin.worldmap;
using Shy;
using Shy.Unit;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

// PlayTime : 32156
// Theme : 1
// StageIndex : 1
// StagePosition : 1
// TileData : [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]
// Coin : 100
// 
// P1 : [0, 10, 0, 0, 0]
// P2 : [0, 10, 0, 0, 0]
// P3 : [0, 10, 0, 0, 0]
// [Type, Health, Strength, MaxHealth, Defence]
// 
// IsBattle : 1
// E1 : [0, 10, 0, 0, 0]
// E2 : [0, 10, 0, 0, 0]
// E3 : [0, 10, 0, 0, 0]
// [Type, Health, Strength, MaxHealth, Defence]
// 
// DiceCount : 3
// D1 : [1, 1, 2, 2, 3, 3]
// D2 : [1, 1, 2, 2, 3, 3]
// D3 : [1, 1, 2, 2, 3, 3]

namespace karin
{
    public class Save : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }

        [ContextMenu("SaveTest")]
        public void SaveData()
        {
            RunSaveData data = new RunSaveData();

            DataLinkManager dataLinkManager = DataLinkManager.Instance;

            data.playTime = Time.time - dataLinkManager.runStartTime;
            data.theme = (int)dataLinkManager.GetMapData.stageTheme;
            data.stageIndex = (int)dataLinkManager.GetMapData.stageIndex;
            data.stagePosition = (int)dataLinkManager.GetMapData.positionIndex;
            data.tileData = dataLinkManager.GetMapData.tileData.Select(t => (int)t.tileType).ToArray();
            data.coin = dataLinkManager.Coin.Value;
            data.gem = dataLinkManager.Gem.Value;

            DataManager dataManager = DataManager.Instance;

            data.playerMinions = new List<DimensionData>();
            for (int i = 0; i < 3; i++)
            {
                CharacterSO minion = dataManager.minions[i];
                if (minion != null)
                {
                    DimensionData dimenData = new DimensionData();
                    dimenData.value = new int[5] { (int)minion.characterType, minion.stats.hp, minion.stats.str, minion.stats.maxHp, minion.stats.def };
                    data.playerMinions.Add(dimenData);
                }
            }

            data.isBattle = SceneManager.GetActiveScene().name != "WorldMap";

            data.diceCount = dataManager.dices.Count;
            data.diceDatas = new List<DimensionData>();

            for (int i = 0; i < dataManager.dices.Count; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    DimensionData dimenData = new DimensionData();
                    dimenData.value = new int[2] { dataManager.dices[i].eyes[j].value, (int)dataManager.dices[i].eyes[j].attackWay };
                    data.diceDatas.Add(dimenData);
                }
            }
            Debug.Log(JsonUtility.ToJson(data));
        }



        [System.Serializable]
        public struct RunSaveData
        {
            public double playTime;
            public int theme;
            public int stageIndex;
            public int stagePosition;
            public int[] tileData;
            public int coin;
            public int gem;
            public List<DimensionData> playerMinions;
            public bool isBattle;
            public int diceCount;
            public List<DimensionData> diceDatas;
        }

        [System.Serializable]
        public struct DimensionData
        {
            public int[] value;
        }

        public struct GameSaveData
        {
            public int gem;
            public int[] characterLock;
            public float masterVolume;
            public float fXVolume;
            public float effectVolume;
            public float gameSpeed;
        }
    }
}