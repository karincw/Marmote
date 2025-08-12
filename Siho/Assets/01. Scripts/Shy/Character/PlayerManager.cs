using Shy.Data;
using UnityEngine;

namespace Shy
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;
        
        private void Awake()
        {
            if (Instance != null) { Destroy(gameObject); return; }
            Instance = this;
        }

        public void AddSynergy(SynergyType _type)
        {
            GameData.playerData.synergies.Add(SOManager.Instance.GetSO(_type));
        }

        public void AddStat(MainStatEnum _type, int _value)
        {
            StatSystem.GetMainStatRef(ref GameData.playerData.mainStat, _type) += _value;
        }
    }
}