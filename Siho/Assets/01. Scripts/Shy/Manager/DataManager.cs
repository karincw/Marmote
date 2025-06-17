using AYellowpaper.SerializedCollections;
using karin;
using Shy.Unit;
using System.Collections.Generic;
using UnityEngine;

namespace Shy
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance;
        [SerializeField] public CharacterSO[] minions = new CharacterSO[3];
        public List<DiceSO> dices;
        [SerializeField, SerializedDictionary("Type", "CharacterSO")] private SerializedDictionary<CharacterType, CharacterSO> _typeToCharacterList;

        private void Awake()
        {
            if (Instance != null) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public int InsertMinion(CharacterSO minion)
        {
            for (int i = 0; i < minions.Length; i++)
            {
                if (minions[i] == null)
                {
                    minions[i] = minion;
                    return i;
                }
            }
            return -1;
        }

    }
}
