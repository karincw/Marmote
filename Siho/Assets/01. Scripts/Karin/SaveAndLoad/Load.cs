using Shy;
using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace karin
{
    public class Load : MonoBehaviour
    {
        public static Load Instance;
        private string _runSaveFolderPath;
        private string GetRunSavePath(string fileName) => $"{_runSaveFolderPath}{fileName}.txt";
        public string GetRunLoadPath(int slotId) => GetRunSavePath(slotId.ToString());
        private string _gameSavePath;
        public string GetGameLoadPath() => _gameSavePath;

        public RunData[] saveRunDatas = new RunData[3];
        public GameData gameData = new GameData();

        private void Awake()
        {
            if (Instance != null) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            _runSaveFolderPath = @$"{Application.persistentDataPath}\RunData\";
            _gameSavePath = @$"{Application.persistentDataPath}\GameData.txt";
            for (int i = 0; i < saveRunDatas.Length; i++)
            {
                saveRunDatas[i].load = false;
            }
        }

        private void Start()
        {
            LoadGameData();
        }

        public void ResetGame()
        {
            saveRunDatas = new RunData[3];
        }

        private bool LoadRunData(int loadIndex)
        {
            RunData data = default;
            string path = GetRunLoadPath(loadIndex);
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                byte[] readBuffer = new byte[1024];
                fs.Read(readBuffer);
                fs.Close();
                Encoding encoding = Encoding.UTF8;
                data = JsonUtility.FromJson<RunData>(encoding.GetString(readBuffer));
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"Open FIle Error {loadIndex} : {ex.Message}");
                return false;
            }
            saveRunDatas[loadIndex] = data;
            saveRunDatas[loadIndex].load = true;
            return true;
        }

        public RunData? GetRunData(int loadIndex)
        {
            if (!saveRunDatas[loadIndex].load)
            {
                if (LoadRunData(loadIndex))
                {
                    return saveRunDatas[loadIndex];
                }
                else return null;
            }
            else
            {
                return saveRunDatas[loadIndex];
            }
        }

        public bool LoadGameData()
        {
            Debug.LogWarning($"Load Run Data > path:{GetGameLoadPath()}");

            GameData data = default;
            string path = GetGameLoadPath();
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                byte[] readBuffer = new byte[1024];
                fs.Read(readBuffer);
                fs.Close();
                Encoding encoding = Encoding.UTF8;
                data = JsonUtility.FromJson<GameData>(encoding.GetString(readBuffer));
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"File Open Error : {ex.Message}");
                return false;
            }
            gameData = data;
            return true;
        }

        public GameData GetGameData()
        {
            if (LoadGameData())
            {
                return gameData;
            }
            return default;
        }

        public void LoadAllRunData()
        {
            for (int i = 0; i < 3; i++)
            {
                LoadRunData(i);
            }
        }

        public void LoadAndApplyGame(int index)
        {
            RunData? d = GetRunData(index);
            if (!d.HasValue) return;
            RunData data = d.Value;

            DataManager.Instance.setData(data);
            DataLinkManager.Instance.setData(data);
        }
    }
}