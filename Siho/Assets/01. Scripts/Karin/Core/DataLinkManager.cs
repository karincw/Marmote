using karin.worldmap;
using Shy;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        private bool isFirstLoading = true;

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
                        Coin.Value = 0;
                        Debug.Log("货肺款 甘 积己");
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
