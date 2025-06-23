using DG.Tweening;
using Shy.Unit;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace karin
{
    public class CharacterView : MonoBehaviour
    {
        private TMP_Text _nameText;
        private Image _portraitImage;
        private TMP_Text _healthText;
        private TMP_Text _strengthText;
        private TMP_Text _defenceText;
        private Button _layout;
        private List<Button> _dices = new();
        private CharacterSO _current;
        private CanvasGroup _curtain;

        private void Awake()
        {
            Transform layoutTrm = transform.Find("Layout");
            Transform PortraitTrm = layoutTrm.transform.Find("Portrait");
            _nameText = PortraitTrm.Find("Name").GetComponent<TMP_Text>();
            _portraitImage = PortraitTrm.Find("Image").GetComponent<Image>();
            _healthText = layoutTrm.Find("Health").GetComponentInChildren<TMP_Text>();
            _strengthText = layoutTrm.Find("Strength").GetComponentInChildren<TMP_Text>();
            _defenceText = layoutTrm.Find("Defence").GetComponentInChildren<TMP_Text>();
            _curtain = transform.Find("Curtain").GetComponent<CanvasGroup>();
            _layout = layoutTrm.GetComponent<Button>();
            layoutTrm.GetComponentsInChildren<Button>(_dices);
            _dices.Remove(_layout);
        }

        private void OnEnable()
        {
            _layout.onClick.AddListener(ViewSkill);
            _dices.ForEach(btn =>
            {
                btn.onClick.AddListener(ViewDice);
            });
        }

        private void OnDisable()
        {
            _layout.onClick.RemoveListener(ViewSkill);
            _dices.ForEach(btn =>
            {
                btn.onClick.RemoveListener(ViewDice);
            });
        }

        public void SetCharacter(CharacterSO ch)
        {
            _current = ch;
        }

        public void SetUp()
        {
            if (_current == null)
            {
                Utils.FadeCanvasGroup(_curtain, false, 0);
                _portraitImage.DOFade(0, 0);
                return;
            }
            Utils.FadeCanvasGroup(_curtain, true, 0);
            _portraitImage.DOFade(1, 0);
            _nameText.text = _current.name.Replace(" (clone)","");
            _portraitImage.sprite = _current.cardImage;
            _healthText.text = _current.stats.maxHp.ToString();
            _strengthText.text = _current.stats.str.ToString();
            _defenceText.text = _current.stats.def.ToString();
        }

        public void ViewSkill()
        {
            InventoryPanel.Instance._infoView.ViewSkill(_current);
        }
        public void ViewDice()
        {
            InventoryPanel.Instance._infoView.ViewDice(_current);
        }
    }
}