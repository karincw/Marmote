using DG.Tweening.Plugins.Core.PathCore;
using Shy;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace karin
{
    public class Save : MonoBehaviour
    {
        public static Save Instance;
        public int slotIndex = -1;

        private string _runSaveFolderPath;
        public string GetRunSavePath(string fileName) => $"{_runSaveFolderPath}{fileName}.txt";
        public string GetRunSavePath(int slotId) => GetRunSavePath(slotId.ToString());
        private string _gameSavePath;
        public string GetGameSavePath() => _gameSavePath;

        private void Awake()
        {
            if (Instance != null) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            _runSaveFolderPath = @$"{Application.persistentDataPath}\RunData\";
            _gameSavePath = @$"{Application.persistentDataPath}\GameData.txt";

        }

        private void OnDisable()
        {
            SaveGameData();
        }

        public void ResetGame()
        {
            slotIndex = -1;
        }

        public void SaveRunData(int saveIndex)
        {
            RunData data = default;
            string path = GetRunSavePath(saveIndex);

            RemoveFile(path);

            try
            {
                WorldMapManager worldMapManager = WorldMapManager.Instance;
                data.load = false;
                data.runIndex = saveIndex;
                data.stageIndex = worldMapManager.stageIndex;
                data.stageTheme = worldMapManager.stageTheme;
                data.position = worldMapManager.positionIndex;
                data.tileData = worldMapManager.GetTileData().ToArray();
                data.coin = DataLinkManager.Instance.Coin;
                data.characterType = new SaveChartacterData[3];
                for (int i = 0; i < 3; i++)
                {
                    var currentCharacter = DataManager.Instance.minions[i];
                    if (currentCharacter == null)
                    {
                        SaveChartacterData characterData = new SaveChartacterData();
                        characterData.type = CharacterType.None;
                        data.characterType[i] = characterData;
                        continue;
                    }
                    data.characterType[i] = (SaveChartacterData)currentCharacter;
                }
                FileStream fs = new FileStream(path, FileMode.CreateNew);
                byte[] buffer = new byte[1024];
                Encoding encoding = Encoding.UTF8;
                buffer = encoding.GetBytes(JsonUtility.ToJson(data));
                fs.Write(buffer);
                fs.Close();
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"File Save Error : {ex.Message}");
            }
            finally
            {
                Load.Instance.saveRunDatas[saveIndex].load = false;
            }
        }

        public void AutoSave()
        {
            SaveRunData(slotIndex);
        }

        public void SaveGameData()
        {
            GameData data = default;
            string path = GetGameSavePath();

            data.Gem = DataLinkManager.Instance.Gem;
            StartingPanel sp = FindFirstObjectByType<StartingPanel>();
            if (sp != null)
                data.cardLockData = sp.GetCardLockData();
            else
                data.cardLockData = Load.Instance.gameData.cardLockData;

            FileStream fs;
            if (File.Exists(GetGameSavePath()))
                fs = new FileStream(path, FileMode.Truncate);
            else
                fs = new FileStream(path, FileMode.CreateNew);

            byte[] buffer = new byte[1024];
            Encoding encoding = Encoding.UTF8;
            buffer = encoding.GetBytes(JsonUtility.ToJson(data));
            fs.Write(buffer);
            fs.Close();
        }

        public void RemoveFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    File.Delete(fileName);
                }
                catch (Exception ex)
                {
                    Debug.LogWarning($"File Delete Error : {ex.Message}");
                }
            }
        }
        public void RemoveFile(int fileIndex)
        {
            Load.Instance.saveRunDatas[fileIndex] = default;
            RemoveFile(GetRunSavePath(fileIndex));
        }

    }
}