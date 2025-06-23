using Shy.Unit;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace karin
{
    public class InfoView : MonoBehaviour
    {
        [SerializeField] private List<TMP_Text> _skillDescriptions;
        [SerializeField] private List<TMP_Text> _diceDescriptions;

        private CanvasGroup _skillLayout;
        private CanvasGroup _diceLayout;
        private CanvasGroup _curtain;

        private CanvasGroup _opendGroup;

        private void Awake()
        {
            _skillLayout = transform.Find("SkillLayout").GetComponent<CanvasGroup>();
            _diceLayout = transform.Find("DiceLayout").GetComponent<CanvasGroup>();
            _curtain = transform.Find("Curtain").GetComponent<CanvasGroup>();

            SetUp();
        }

        public void ViewSkill(CharacterSO current)
        {
            OpenGroup(_skillLayout);
            for (int i = 0; i < _skillDescriptions.Count; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"{current.name}의 {i+1}번 스킬 정보\n");
                sb.Append(current.skills[i].explian);
                _skillDescriptions[i].text = sb.ToString();
            }
        }

        public void ViewDice(CharacterSO current)
        {
            OpenGroup(_diceLayout);
            for (int i = 0; i < _diceDescriptions.Count; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append($"{current.name}의 {i+1}번째 면 정보\n");
                //sb.Append(current.DiceSO.eyes[i].exlpain);
                _diceDescriptions[i].text = sb.ToString();
            }
        }

        private void OpenGroup(CanvasGroup open)
        {
            Utils.FadeCanvasGroup(_opendGroup, true, 0);
            Utils.FadeCanvasGroup(open, false, 0);
            _opendGroup = open;
        }

        public void SetUp()
        {
            OpenGroup(_curtain);
        }
    }
}