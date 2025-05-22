using karin.worldmap;
using Shy;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace karin.Core
{
    public class DataLinkManager : MonoSingleton<DataLinkManager>
    {
        public int Gem;
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
            switch (scene.name)
            {
                case "WorldMap":
                    if (isFirstLoading)
                    {
                        isFirstLoading = false;
                        mapData.stageIndex = 0;
                        mapData.positionIndex = 0;
                        mapData.tileData = WorldMapManager.Instance.GetStageTileData(0);
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
