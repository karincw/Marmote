using karin.ui;
using karin.worldmap;
using Shy.Unit;
using System;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
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
        public  double runStartTime;

        private void Awake()
        {
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
                        mapData.stageTheme = (Theme)Random.Range(0, 6);
                        mapData.events = null;
                        Coin.Value = 0;
                        Debug.Log("货肺款 甘 积己");
                        runStartTime = Time.time;
                    }
                    OnLoadWorldMap?.Invoke(mapData);
                    break;
                case "Battle":
                    break;
            }
        }

        private void Update()
        {
#if UNITY_EDITOR
            if(Input.GetKeyDown(KeyCode.F1))
            {
                Gem.Value += 800;
            }
            if (Input.GetKeyDown(KeyCode.F2))
            {
                Gem.Value -= 800;
            }
#endif
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
