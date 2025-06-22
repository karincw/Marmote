using karin;
using UnityEditor.U2D.Animation;
using UnityEngine;

namespace Shy.Unit
{
    [CreateAssetMenu(menuName = "SO/Shy/Character")]
    public class CharacterSO : ScriptableObject
    {
        public string characterName;
        public CharacterType characterType;

        public Stat stats;

        public SkillSO[] skills = new SkillSO[3];

        [Header("Image")]
        public Sprite sprite;
        public Sprite attackAnime;
        public Sprite skillAnime;
        public Sprite skill2Anime;
        public Sprite hitAnime;

        [Header("UI-Settings")]
        public DiceSO DiceSO;
        public Sprite cardImage;
        [ColorUsage(true)] public Color personalColor;


        public static explicit operator SaveChartacterData(CharacterSO ch)
        {
            if (ch == null) Debug.LogError($"CharacterSO Change Error");
            return new SaveChartacterData()
            {
                type = ch.characterType,
                maxHp = ch.stats.maxHp,
                hp = ch.stats.hp,
                strength = ch.stats.str,
                defence = ch.stats.def,
                diceData = (SaveDiceData)ch.DiceSO,
            };
        }
    }
}
