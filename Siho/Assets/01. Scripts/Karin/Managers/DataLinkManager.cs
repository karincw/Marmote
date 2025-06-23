using Shy.Unit;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace karin
{
    public class DataLinkManager : MonoBehaviour
    {
        public static DataLinkManager Instance;
        public int Gem;
        public int Coin;

        public List<EnemySO> EnemyData { get; private set; }

        private void Awake()
        {
            if (Instance != null) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            Gem = Load.Instance.GetGameData().Gem;
        }

        public void SetEnemyData(List<EnemySO> enemys)
        {
            EnemyData = enemys;
        }

        public void setData(RunData data)
        {
            Coin = data.coin;
        }
    }
}
