using karin.worldmap;
using Shy.Unit;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace karin.Core
{
    public class DataLinkManager : MonoSingleton<DataLinkManager>
    {
        public ChangingValue<int> Gem = new();
        public ChangingValue<int> Coin = new();

        public MapData mapData;
        [SerializeField] private DataStruct<EnemySO> enemyData;
        public event Action<MapData> OnLoadWorldMap;

        public DataStruct<EnemySO> GetEnemyData => enemyData;
        private bool isFirstLoading = true;
        public int runStartTime;

        private void Awake()
        {
            if (Instance == null) { _instance = this; }
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += handleSceneLoad;
        }

        private void handleSceneLoad(Scene scene, LoadSceneMode mode)
        {
            Gem.Update();
            Coin.Update();
            switch (scene.name)
            {
                case "WorldMap":
                    if (isFirstLoading)
                    {
                        isFirstLoading = false;
                        mapData.stageIndex = 0;
                        mapData.positionIndex = 0;
                        mapData.tileData = WorldMapManager.Instance.GetStageTileData(0).Select(t => (int)t.tileType).ToList();
                        mapData.stageTheme = (Theme)Random.Range(0, (int)Theme.END);
                        mapData.events = null;
                        Coin.Value = 0;
                        Debug.Log("货肺款 甘 积己");
                        runStartTime = (int)Time.time;
                    }
                    OnLoadWorldMap?.Invoke(mapData);
                    break;
                case "Battle":
                    break;
            }
        }

        public void SetMapData()
        {
            mapData.stageIndex = WorldMapManager.Instance.stageIndex;
            mapData.tileData = WorldMapManager.Instance.GetStageTileData(0).Select(t => (int)t.tileType).ToList();
            mapData.stageTheme = WorldMapManager.Instance.stageTheme;
        }

        public void SetLoadData(RunSaveData data)
        {
            mapData.stageTheme = (Theme)data.theme;
            mapData.stageIndex = data.stageIndex;
            mapData.positionIndex = data.stagePosition;
            mapData.tileData = data.tileData.ToList();
            Coin.Value = data.coin;
            isFirstLoading = false;
        }

        public void ResetGame()
        {
            isFirstLoading = true;
        }

        public void SaveEnemyData(DataStruct<EnemySO> data)
        {
            enemyData = data;
        }
    }
}
