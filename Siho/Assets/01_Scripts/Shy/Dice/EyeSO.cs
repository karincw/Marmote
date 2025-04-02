using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(menuName ="SO/Shy/Dice/Eye")]
    public class EyeSO : ScriptableObject
    {
        public string eyeName;
        [TextArea]
        public string explain;
        public Sprite icon;

        //Send to User
        [Range(1, 3)] public int value;
        public ActionWay attackWay;
    }
}
