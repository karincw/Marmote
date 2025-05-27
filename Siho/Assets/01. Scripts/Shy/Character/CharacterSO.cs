using UnityEngine;

namespace Shy.Unit
{
    [CreateAssetMenu(menuName = "SO/Shy/Character")]
    public class CharacterSO : ScriptableObject
    {
        public string characterName;

        public Stat stats;

        public SkillSO[] skills = new SkillSO[3];

        [Header("Image")]
        public Sprite sprite;
        public Sprite attackAnime;
        public Sprite skillAnime;
        public Sprite skill2Anime;
        public Sprite hitAnime;

        [Header("UI-Settings")]
        public DiceSO startDiceSO;
        public Sprite cardImage;
        [ColorUsage(true)] public Color personalColor;
    }
}
