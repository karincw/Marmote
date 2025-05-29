using karin.Core;
using Shy;
using Shy.Unit;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace karin
{
    public class Save : MonoSingleton<Save> 
    {
        public static string RunSaveFolderPath;
        public static string GameSavePath;
        public int PlayIndex;
        public List<string> SavedRuns;

        private void Awake()
        {
            if (Instance == null) { _instance = this; }
            DontDestroyOnLoad(gameObject);
            PlayIndex = -1;
            RunSaveFolderPath = @$"{Application.persistentDataPath}\RunData";
            GameSavePath = @$"{Application.persistentDataPath}\GameData.txt";

            DirectoryInfo di = new DirectoryInfo(RunSaveFolderPath);
            if (di.Exists == false)
                di.Create();
        }

        [ContextMenu("SaveRunTest")]
        public void SaveRunData()
        {
            RunSaveData data = new RunSaveData();

            DataLinkManager dataLinkManager = DataLinkManager.Instance;

            data.playTime = (int)Time.time - dataLinkManager.runStartTime;
            data.theme = (int)dataLinkManager.GetMapData.stageTheme;
            data.stageIndex = (int)dataLinkManager.GetMapData.stageIndex;
            data.stagePosition = (int)dataLinkManager.GetMapData.positionIndex;
            data.tileData = dataLinkManager.GetMapData.tileData.Select(t => (int)t.tileType).ToArray();
            data.coin = dataLinkManager.Coin.Value;

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

            data.RunIndex = data.GetHashCode();

            FileStream fs = new FileStream(RunSaveFolderPath + $"\\{data.RunIndex.ToString()}.txt", FileMode.OpenOrCreate);
            Encoding encoding = Encoding.UTF8;
            fs.Write(encoding.GetBytes(JsonUtility.ToJson(data)));
            fs.Close();
            SavedRuns.Add(data.RunIndex.ToString());
        }

        public void SaveRunData(RunSaveData beforeData)
        {
            RunSaveData data = beforeData;

            DataLinkManager dataLinkManager = DataLinkManager.Instance;

            data.playTime += (int)Time.time - dataLinkManager.runStartTime;
            data.theme = (int)dataLinkManager.GetMapData.stageTheme;
            data.stageIndex = (int)dataLinkManager.GetMapData.stageIndex;
            data.stagePosition = (int)dataLinkManager.GetMapData.positionIndex;
            data.tileData = dataLinkManager.GetMapData.tileData.Select(t => (int)t.tileType).ToArray();
            data.coin = dataLinkManager.Coin.Value;

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

            FileStream fs = new FileStream(RunSaveFolderPath + $"\\{data.RunIndex.ToString()}.txt", FileMode.Truncate);
            Encoding encoding = Encoding.UTF8;
            fs.Write(encoding.GetBytes(JsonUtility.ToJson(data)));
            fs.Close();
            SavedRuns.Add(data.RunIndex.ToString());
        }

        [ContextMenu("SaveGameTest")]
        public void SaveGameData()
        {
            GameSaveData data = new GameSaveData();

            DataLinkManager dataLinkManager = DataLinkManager.Instance;

            data.gem = dataLinkManager.Gem.Value;
            List<SelectCard> cards = FindObjectsByType<SelectCard>(FindObjectsSortMode.None).OrderBy(c => c.SiblingIndex).ToList();
            data.characterLock = cards.Select(c => c.canPlay).ToArray();
            data.masterVolume = 100;
            data.fXVolume = 100;
            data.effectVolume = 100;
            data.saves = SavedRuns;

            DataManager dataManager = DataManager.Instance;

            FileInfo file = new FileInfo(GameSavePath);
            if (file.Exists)
                file.Delete();

            FileStream fs = new FileStream(GameSavePath, FileMode.CreateNew);
            Encoding encoding = Encoding.UTF8;
            fs.Write(encoding.GetBytes(JsonUtility.ToJson(data)));
            fs.Close();
        }
    }
}