using System.Collections.Generic;
using UnityEngine;

namespace Shy
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance;

        private void Awake()
        {
            if (Instance != null) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        [SerializeField] public CharacterSO[] minions = new CharacterSO[3];
        [SerializeField] public List<DiceSO> dices;
    }
}
