using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "SO/Shy/Enemy")]
    public class EnemySO : ScriptableObject
    {
        public CharacterSO data;
        public int actValue = 1;
        public DiceSO[] dices;
    }
}
