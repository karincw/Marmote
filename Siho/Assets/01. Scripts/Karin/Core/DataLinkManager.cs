using karin.worldmap;
using Shy;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace karin.Core
{
    public class DataLinkManager : MonoSingleton<DataLinkManager>
    {
        [SerializeField] private MapData mapData;
        [SerializeField] private DataStruct<CharacterSO> enemyData;
        public event Action<MapData> OnLoadWorldMap;

        [SerializeField] private int DebugSceneIdx = 0;
        private bool isFirstLoading = true;
        public DataStruct<CharacterSO> GetEnemyData => enemyData;
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += handleSceneLoad;
        }

        private void Start()
        {
            SceneManager.LoadScene(DebugSceneIdx);
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                SceneManager.LoadScene(DebugSceneIdx);
            }
        }

        public void SaveMap(MapData data)
        {
            mapData = data;
        }

        public void WriteEnemyData(DataStruct<CharacterSO> data)
        {
            enemyData = data;
        }

    }
}
