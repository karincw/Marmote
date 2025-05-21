using karin;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Shy
{
    public class DataManager : MonoBehaviour
    {
        public static DataManager Instance;

        [SerializeField] public CharacterSO[] minions = new CharacterSO[3];
        public List<DiceSO> dices;

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

        public void MakeDice()
        {
            dices = new List<DiceSO>();
            minions = minions.ToList().OrderBy(m => m == null ? 1 : 0).ToArray();

            for (int i = 0; i < SelectCard.SelectCount; i++)
            {
                dices.Add(minions[i].startDiceSO);
            }
        }

    }
}
