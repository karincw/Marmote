using UnityEngine;

namespace Shy
{
    [CreateAssetMenu(menuName = "SO/Shy/Skill")]
    public class SkillSO : ScriptableObject
    {
        public ActionWay way;
        public ActionType type;

        [Range(1, 5)]public int attackCnt = 1;
        public string formula;



        public int GetValue()
        {
            //int resultValue = value;
            return 0;
        }
    }
}
