using karin.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        protected override void Start()
        {
            base.Start();
            LoadGameData();
        }

        [ContextMenu("LoadRunTest")]
        public RunSaveData? LoadRunData(int loadIdx = 0)
        {
            RunSaveData? data = null;
            try
            {
                FileStream fs = new FileStream(Save.RunSaveFolderPath + @$"\{Save.Instance.SavedRuns[loadIdx]}.txt", FileMode.Open);
                Encoding encoding = Encoding.UTF8;
                byte[] readByte = new byte[1024];
                fs.Read(readByte, 0, 1024);
                string loadData = encoding.GetString(readByte);
                data = JsonUtility.FromJson<RunSaveData>(loadData);
                fs.Close();
            }
            catch (Exception ex)
            {
                Debug.LogError(ex.Message);
            }

            return data;
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
            fs.Close();

            Save.Instance.SavedRuns = data.saves;
            DataLinkManager.Instance.Gem.Value = data.gem;
            List<SelectCard> cards = FindObjectsByType<SelectCard>(FindObjectsSortMode.None).OrderBy(c => c.SiblingIndex).ToList();
            for (int i = 0; i < data.characterLock.Length; i++)
            {
                cards[i].canPlay = data.characterLock[i];
            }
        }
    }
}