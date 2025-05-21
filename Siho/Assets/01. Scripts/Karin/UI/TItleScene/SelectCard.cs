using karin.Core;
using Shy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace karin
{
    public class SelectCard : MonoBehaviour
    {
        [SerializeField, ColorUsage(true)] private Color _selectColor;
        [SerializeField, ColorUsage(true)] private Color _disabledColor;
        [SerializeField] private CharacterSO _currentCharacter;
        [SerializeField] private int _sellPrice = 800;
        [SerializeField] private bool debugStart;


        private static readonly int _minSelect = 1;
        private static readonly int _maxSelect = 3;
        public static int SelectCount = 0;

        public bool canPlay
        {
            get => _canPlay;
            set
            {
                _canPlay = value;
                HandleCanPlayChanged();
            }
        }
        private bool _canPlay;

        private Image _cardImage;
        private Image _previewImage;
        private SelectBtn _selectBtn;
        private TMP_Text _btnText;
        private Color _originColor;
        private bool _selected;
        private int _selectIdx;

        private void Awake()
        {
            _cardImage = GetComponent<Image>();
            _previewImage = transform.Find("Preview").GetComponent<Image>();
            _selectBtn = GetComponentInChildren<SelectBtn>();
            _btnText = _selectBtn.GetComponentInChildren<TMP_Text>();
            _originColor = _cardImage.color;
            _selected = false;
            _previewImage.sprite = _currentCharacter.sprite;
            _selectBtn.onClick.AddListener(HandleSelect);
        }

        private void OnDestroy()
        {
            _selectBtn.onClick.RemoveListener(HandleSelect);
        }

        private void Start()
        {
            canPlay = debugStart;
        }

        public void HandleSelect()
        {
            if (_canPlay)
            {
                if (!_selected && !CanSelect()) return;
                _selected = !_selected;
                if (_selected)
                {
                    _cardImage.color = _selectColor;
                    _selectBtn.ChangeColor(_selectColor);
                    _selectIdx = DataManager.Instance.InsertMinion(_currentCharacter);
                    SelectCount++;
                }
                else
                {
                    _cardImage.color = _originColor;
                    _selectBtn.ChangeColor(Color.white);
                    DataManager.Instance.minions[_selectIdx] = null;
                    SelectCount--;
                }
            }
            else
            {
                if (DataLinkManager.Instance.Gem >= _sellPrice)
                {
                    DataLinkManager.Instance.Gem -= _sellPrice;
                    canPlay = true;
                }
                else
                {
                    Debug.Log("µ·¾ø¾î");
                }
            }
        }

        public void HandleCanPlayChanged()
        {
            if (_canPlay)
            {
                _previewImage.color = Color.white;
                _btnText.text = "Select";
            }
            else
            {
                _previewImage.color = _disabledColor;
                _btnText.text = $"{_sellPrice}Gem";
            }
        }

        private bool CanSelect()
        {
            int current = SelectCount + 1;
            if (_maxSelect >= current && current >= _minSelect)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}