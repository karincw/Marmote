using karin.worldmap;
using Shy;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace karin.Core
{
    public class DataLinkManager : MonoSingleton<DataLinkManager>, IHaveSaveData
    {
        [SerializeField] private MapData mapData;
        [SerializeField] private DataStruct<EnemySO> enemyData;
        public event Action<MapData> OnLoadWorldMap;

        private bool isFirstLoading = true;
        public DataStruct<CharacterSO> GetEnemyData => enemyData;

        [SerializeField] private int DebugSceneIdx = 0;
        [SerializeField] private bool DebugMode = false;

        public DataStruct<EnemySO> GetEnemyData => enemyData;
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += handleSceneLoad;
        }

        private void Start()
        {
            if(DebugMode)
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

        public void WriteEnemyData(DataStruct<EnemySO> data)
        {
            enemyData = data;
        }

        public void GetSaveData(ref SaveData save)
        {
            save.theme         = (int)mapData.stageTheme;
            save.stageIndex    = mapData.stageIndex;
            save.stagePosition = mapData.positionIndex;
            save.tileData      = mapData.tileData.Select(t => (int)t.tileType).ToList();
            save.isBattle      = SceneManager.GetActiveScene().name == "WorldMap" ? 0 : 1;
        }
    }
}
