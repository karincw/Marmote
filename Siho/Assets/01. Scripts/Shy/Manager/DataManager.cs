using AYellowpaper.SerializedCollections;
using karin;
using Shy.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shy
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance;
        [SerializeField] public CharacterSO[] minions = new CharacterSO[3];
        [SerializeField, SerializedDictionary("Type", "CharacterSO")] private SerializedDictionary<CharacterType, CharacterSO> _typeToCharacterList;
        public int GetMinionCount => minions.Where(t => t != null).Count();

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
                    minions[i] = Instantiate(minion);
                    minions[i].Init();
                    return i;
                }
            }
            return -1;
        }

        public void setData(RunData data)
        {
            for (int i = 0; i < minions.Length; i++)
            {
                if (data.characterType[i].type == CharacterType.None)
                    minions[i] = null;
                else
                {
                    var ch = data.characterType[i];
                    minions[i] = _typeToCharacterList[ch.type];
                    minions[i].stats.maxHp = ch.maxHp;
                    minions[i].stats.hp = ch.hp;
                    minions[i].stats.str = ch.strength;
                    minions[i].stats.def = ch.defence;
                }
            }
        }
    }
}
