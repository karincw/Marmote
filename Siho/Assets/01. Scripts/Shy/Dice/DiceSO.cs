using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(menuName = "SO/Shy/Dice/Base")]
    public class DiceSO : ScriptableObject
    {
        public EyeSO[] eyes = new EyeSO[6];
        public Color color = Color.white;

        public EyeSO GetEye(int _eye) => eyes[_eye];
    }
}
