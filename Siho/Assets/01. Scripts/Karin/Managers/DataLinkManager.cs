using Shy.Unit;
using System.Collections.Generic;
using UnityEngine;

namespace karin
{
    public class DataLinkManager : MonoBehaviour
    {
        public static DataLinkManager Instance;
        public int Gem;
        public int Coin;

        [SerializeField, Header("Shy Tester")] private bool testMode = false;
        [SerializeField] private List<EnemySO> testEnemyDatas;

        public List<EnemySO> EnemyData { get; private set; }

        private void Awake()
        {
            if (Instance != null) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (testMode) EnemyData = testEnemyDatas;
        }

        public void SaveEnemyData(List<EnemySO> enemys)
        {
            EnemyData = enemys;
        }
    }
}
