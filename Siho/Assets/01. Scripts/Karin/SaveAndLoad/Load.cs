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

        public SaveRunData[] saveRunDatas = new SaveRunData[3];

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

        public void ResetGame()
        {
            saveRunDatas = new SaveRunData[3];
        }

        private bool LoadRunData(int loadIndex)
        {
            Debug.LogWarning($"Load Run Data > path:{GetRunLoadPath(loadIndex)}");
            SaveRunData data = default;
            string path = GetRunLoadPath(loadIndex);
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                byte[] readBuffer = new byte[1024];
                fs.Read(readBuffer);
                fs.Close();
                Encoding encoding = Encoding.UTF8;
                data = JsonUtility.FromJson<SaveRunData>(encoding.GetString(readBuffer));
            }
            catch (Exception ex)
            {
                Debug.LogError($"Open FIle Error : {ex.Message}");
                return false;
            }
            saveRunDatas[loadIndex] = data;
            saveRunDatas[loadIndex].load = true;
            return true;
        }

        public SaveRunData? GetRunData(int loadIndex)
        {
            if (!saveRunDatas[loadIndex].load)
            {
                if(LoadRunData(loadIndex))
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

        public void LoadGameData()
        {
            Debug.LogWarning($"Load Run Data > path:{GetGameLoadPath()}");
            Debug.LogWarning($"데이터 로드 막아둠");


        }
        public void LoadAllRunData()
        {
            for (int i = 0; i < 3; i++)
            {
                LoadRunData(i);
            }
        }
    }
}