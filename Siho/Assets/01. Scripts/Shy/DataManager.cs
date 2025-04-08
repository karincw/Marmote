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

        [SerializeField] private CharacterSO[] minions = new CharacterSO[3];
        [SerializeField] private List<DiceSO> dices;

        //Get
        public CharacterSO[] GetMinions() => minions;
        public List<DiceSO> GetDices() => dices;
    }
}
