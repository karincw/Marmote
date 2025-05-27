using Shy.Unit;
using TMPro;
using UnityEngine;
namespace karin.ui
{
    public class SkillDescriptionPanel : MonoBehaviour
    {
        private TMP_Text _desc;
        [SerializeField] private int _index;

        private void Awake()
        {
            _desc = GetComponentInChildren<TMP_Text>();
        }

        public void SetUpDate(CharacterSO data)
        {
            _desc.text = data.skills[_index].explian;
        }
    }
}