using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace karin
{
    public class Load : MonoSingleton<Load>
    {
        private void Awake()
        {
            if (Instance == null) { _instance = this; }
            DontDestroyOnLoad(gameObject);
        }

        [ContextMenu("LoadRunTest")]
        public void LoadRunData()
        {
            FileStream fs = new FileStream(Save.RunSaveFolderPath + @$"\{Save.Instance.SavedRuns[0]}.txt", FileMode.Open);
            Encoding encoding = Encoding.UTF8;
            byte[] readByte = new byte[1024];
            fs.Read(readByte, 0, 1024);
            string loadData = encoding.GetString(readByte);
            RunSaveData data = JsonUtility.FromJson<RunSaveData>(loadData);

            Debug.Log(JsonUtility.ToJson(data));
            fs.Close();
        }

        [ContextMenu("LoadGameTest")]
        public void LoadGameData()
        {
            FileStream fs = new FileStream(Save.GameSavePath, FileMode.Open);
            Encoding encoding = Encoding.UTF8;
            byte[] readByte = new byte[1024];
            fs.Read(readByte, 0, 1024);
            string loadData = encoding.GetString(readByte);
            GameSaveData data = JsonUtility.FromJson<GameSaveData>(loadData);

            Debug.Log(JsonUtility.ToJson(data));
            fs.Close();
        }
    }
}