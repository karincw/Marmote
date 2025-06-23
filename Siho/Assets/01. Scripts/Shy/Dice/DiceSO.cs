using karin;
using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(menuName = "SO/Shy/Dice/Base")]
    public class DiceSO : ScriptableObject
    {
        public EyeSO[] eyes = new EyeSO[6];
        public Color color = Color.white;

        public EyeSO GetEye(int _eye) => eyes[_eye];

        public static explicit operator SaveDiceData(DiceSO dice)
        {
            return new SaveDiceData()
            {
                eyes = new System.Collections.Generic.List<Pair<int, ActionWay>>()
                {
                    dice.eyes[0],
                    dice.eyes[1],
                    dice.eyes[2],
                    dice.eyes[3],
                    dice.eyes[4],
                    dice.eyes[5],
                }
            };
        }
    }
}
