using karin;
using UnityEngine;

namespace Shy.Unit
{
    [CreateAssetMenu(menuName = "SO/Shy/Character")]
    public class CharacterSO : ScriptableObject
    {
        public string characterName;
        public CharacterType characterType;

        public Stat stats;

        public SkillSOBase[] skills = new SkillSOBase[3];

        [Header("Image")]
        public Sprite sprite;
        public Sprite hitAnime;

        [Header("UI-Settings")]
        public DiceSO startDiceSO;
        public Sprite cardImage;
        [ColorUsage(true)] public Color personalColor;
    }
}
