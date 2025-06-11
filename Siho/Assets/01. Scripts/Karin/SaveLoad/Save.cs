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
        private GameSaveData _titleData;

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
            _titleData = new();
        }

        private void OnDisable()
        {
            SaveGameData();
        }

        public void SaveCharacterLockData()
        {
            Debug.Log("SaveCharacterLockData");
            List<SelectCard> cards = FindObjectsByType<SelectCard>(FindObjectsSortMode.None).OrderBy(c => c.SiblingIndex).ToList();
            _titleData.characterLock = cards.Select(c => c.canPlay).ToArray();
        }

        [ContextMenu("SaveRunTest")]
        public void SaveRunData()
        {
            RunSaveData data = new RunSaveData();

            DataLinkManager dataLinkManager = DataLinkManager.Instance;

            data.playTime = (int)Time.time - dataLinkManager.runStartTime;
            data.theme = (int)dataLinkManager.mapData.stageTheme;
            data.stageIndex = (int)dataLinkManager.mapData.stageIndex;
            data.stagePosition = (int)dataLinkManager.mapData.positionIndex;
            data.tileData = dataLinkManager.mapData.tileData.ToArray();
            data.coin = dataLinkManager.Coin.Value;

            DataManager dataManager = DataManager.Instance;
            data.minionCount = 0;
            data.playerMinions = new List<DimensionData>();
            for (int i = 0; i < 3; i++)
            {
                CharacterSO minion = dataManager.minions[i];
                if (minion != null)
                {
                    DimensionData dimenData = new DimensionData();
                    dimenData.value = new int[5] { (int)minion.characterType, minion.stats.hp, minion.stats.str, minion.stats.maxHp, minion.stats.def };
                    data.playerMinions.Add(dimenData);
                    data.minionCount++;
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

            FileStream fs;
            if (PlayIndex == -1)
            {
                //data.RunIndex = SavedRuns.Count;
                data.RunIndex = 0;
                fs = new FileStream(RunSaveFolderPath + @$"\{data.RunIndex.ToString()}.txt", FileMode.OpenOrCreate);
                SavedRuns.Add(data.RunIndex.ToString());
            }
            else
            {
                data.RunIndex = PlayIndex;
                fs = new FileStream(RunSaveFolderPath + $@"\{data.RunIndex.ToString()}.txt", FileMode.Truncate);
            }
            Encoding encoding = Encoding.UTF8;
            fs.Write(encoding.GetBytes(JsonUtility.ToJson(data)));
            PlayIndex = data.RunIndex;
            fs.Close();
        }

        [ContextMenu("SaveGameTest")]
        public void SaveGameData()
        {
            GameSaveData data = new GameSaveData();

            data.gem = DataLinkManager.Instance.Gem.Value;
            data.masterVolume = 100;
            data.fXVolume = 100;
            data.effectVolume = 100;
            data.saves = SavedRuns;
            if (SceneManager.GetActiveScene().name == "Title")
                SaveCharacterLockData();
            data.characterLock = _titleData.characterLock;

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