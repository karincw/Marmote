using UnityEngine;

namespace Shy
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;
        private CharacterDataSO playerData;
        
        private void Awake()
        {
            if (Instance != null) { Destroy(gameObject); return; }
            Instance = this;
        }

        internal void Init(CharacterDataSO _player)
        {
            playerData = _player.Init();
        }

        public void AddSynergy(SynergyType _type, int _loop = 1)
        {
            for (int i = 0; i < _loop; i++)
            {
                playerData.AddSynergy(_type);
            }
        }

        public void AddStat(MainStatEnum _type, int _value) => ChangeStat(_type, _value, Calculate.Plus);

        public void ChangeStat(MainStatEnum _type, int _value, Calculate _calc)
        {
            ref int stat = ref StatSystem.GetMainStatRef(ref playerData.mainStat, _type);
            stat = Mathf.RoundToInt(Calculator.GetValue(stat, _value, _calc));

            if (stat < 1) stat = 1;
        }

        public int GetStatCount(MainStatEnum _type)
        {
            return StatSystem.GetMainStatRef(ref playerData.mainStat, _type);
        }

        public int GetSynergyCount(SynergyType _type)
        {
            if (playerData.synergies.ContainsKey(_type) == false) return 0;

            return playerData.synergies[_type];
        }

        internal CharacterDataSO GetPlayerData() => playerData;
    }
}