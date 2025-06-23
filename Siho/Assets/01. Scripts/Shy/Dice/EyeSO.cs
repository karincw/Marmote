using karin;
using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(menuName ="SO/Shy/Dice/Eye")]
    public class EyeSO : ScriptableObject
    {
        [Range(1, 3)] public int value;
        public ActionWay attackWay;

        public static implicit operator Pair<int, ActionWay>(EyeSO eye)
        {
            return new Pair<int, ActionWay>() {first = eye.value, Second = eye.attackWay };
        }
    }
}
