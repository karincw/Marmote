using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(menuName = "SO/Shy/Dice/Base")]
    public class DiceSO : ScriptableObject
    {
        public EyeSO[] eyes = new EyeSO[6];
    }
}
