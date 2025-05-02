using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "SO/Shy/Enemy")]
    public class EnemySO : ScriptableObject
    {
        public CharacterSO data;
        public int level = 1;
        public LevelByDice[] dices;

        public DiceSO[] GetDices()
        {
            for (int i = 0; i < dices.Length; i++)
            {
                if (level == dices[i].level) return dices[i].dices;
            }
            Debug.LogError("None Level");
            return null;
        }
    }
}
