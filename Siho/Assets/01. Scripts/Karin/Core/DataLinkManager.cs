using karin.worldmap;
using Shy.Unit;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace karin.Core
{
    public class DataLinkManager : MonoSingleton<DataLinkManager>
    {
        public ChangingValue<int> Gem = new();
        public ChangingValue<int> Coin = new();

        [SerializeField] private MapData mapData;
        [SerializeField] private DataStruct<EnemySO> enemyData;
        public event Action<MapData> OnLoadWorldMap;

        public DataStruct<EnemySO> GetEnemyData => enemyData;
        public MapData GetMapData => mapData;
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
                        mapData.tileData = WorldMapManager.Instance.GetStageTileData(0);
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

        public void SaveMap(MapData data)
        {
            mapData = data;
        }

        public void SaveEnemyData(DataStruct<EnemySO> data)
        {
            enemyData = data;
        }
    }
}
