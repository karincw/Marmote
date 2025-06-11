using AYellowpaper.SerializedCollections;
using karin;
using Shy.Unit;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.sceneLoaded += HandleSceneLoad;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= HandleSceneLoad;
        }

        private void HandleSceneLoad(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name == "Title")
            {
                minions = new CharacterSO[3];
                dices = new List<DiceSO>();
            }
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

        public void SetLoadData(RunSaveData data)
        {
            minions = new CharacterSO[3];
            for (int i = 0; i < data.minionCount; i++)
            {
                minions[i] = Instantiate(_typeToCharacterList[(CharacterType)data.playerMinions[i].value[0]]);
                minions[i].stats.hp = data.playerMinions[i].value[1];
                minions[i].stats.str = data.playerMinions[i].value[2];
                minions[i].stats.maxHp = data.playerMinions[i].value[3];
                minions[i].stats.def = data.playerMinions[i].value[4];
            }

            dices = new List<DiceSO>();
            for (int i = 0; i < data.diceCount; i++)
            {
                DiceSO newDice = new DiceSO();
                newDice.eyes = new EyeSO[6];
                for (int j = 0; j < 6; j++) 
                {
                    EyeSO newEye = new EyeSO();
                    newEye.value = data.diceDatas[j].value[0];
                    newEye.attackWay = (ActionWay)data.diceDatas[j].value[1];
                    newDice.eyes[j] = newEye;
                }
                dices.Add(newDice);
            }
        }
    }
}
