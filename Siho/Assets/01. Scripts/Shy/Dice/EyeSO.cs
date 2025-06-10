using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(menuName ="SO/Shy/Dice/Eye")]
    public class EyeSO : ScriptableObject
    {
        [Range(1, 3)] public int value;
        public ActionWay attackWay;
    }
}
