using karin.worldmap;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace karin.Core
{
    public class DataManager : MonoSingleton<DataManager>
    {
        [SerializeField] private MapData mapData;
        //[SerializeField] private MapData mapData; //적 데이터 구조체
        public event Action<MapData> OnLoadWorldMap;

        [SerializeField] private int DebugSceneIdx = 0;
        private bool isFirstLoading = true;
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += handleSceneLoad;
        }

        private void handleSceneLoad(Scene scene, LoadSceneMode mode)
        {
            if (scene.name != "WorldMap") return;

            if (isFirstLoading)
            {
                isFirstLoading = false;
                mapData.stageIndex = 0;
                mapData.positionIndex = 0;
                mapData.tileData = WorldMapManager.Instance.GetStageTileData(0);
            }
            OnLoadWorldMap?.Invoke(mapData);
        }

        private void OnEnable()
        {
            Debug.Log("start");
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.F1))
            {
                SceneManager.LoadScene(DebugSceneIdx);
            }
        }

        public void ChangeBattleScene()
        {

        }

        public void SaveMap(MapData data)
        {
            mapData = data;
        }

    }
}
