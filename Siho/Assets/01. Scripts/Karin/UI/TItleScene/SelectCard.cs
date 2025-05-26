using DG.Tweening;
using karin.Core;
using Shy;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace karin
{
    public class SelectCard : ShakableUI
    {
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

        //Transition
        private Image _transitionBG;
        private Vector2 _transitionBGOriginPos;
        private Image _transitionFace;
        private Vector2 _transitionFaceOriginPos;

        private Image _previewImage;
        private SelectBtn _selectBtn;
        private TMP_Text _btnText;
        private Color _selectColor;
        private bool _selected;
        private int _selectIdx;

        protected override void Awake()
        {
            base.Awake();
            Transform previewTrm = transform.Find("PreviewFrame");
            _transitionBG = previewTrm.Find("TransitionBG").GetComponent<Image>();
            _transitionFace = previewTrm.Find("TransitionFace").GetComponent<Image>();
            _previewImage = previewTrm.Find("Preview").GetComponent<Image>();
            _selectBtn = GetComponentInChildren<SelectBtn>();
            _btnText = _selectBtn.GetComponentInChildren<TMP_Text>();

            _selectColor = _currentCharacter.personalColor;
            _transitionBGOriginPos = _transitionBG.transform.localPosition;
            _transitionBG.color = _currentCharacter.personalColor;
            _transitionFaceOriginPos = _transitionFace.transform.localPosition;
            _transitionFace.sprite = _currentCharacter.cardImage;
            _previewImage.sprite = _currentCharacter.cardImage;

            _selected = false;
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
                    Selecting();
                else
                    DeSelecting();
            }
            else
            {
                if (DataLinkManager.Instance.Gem.Value >= _sellPrice)
                {
                    DataLinkManager.Instance.Gem.Value -= _sellPrice;
                    canPlay = true;
                }
                else
                {
                    Shake(0.5f, new Vector3(5, 10), new Vector3(0, 0, 5), DG.Tweening.Ease.OutQuart);
                }
            }
        }

        private void Selecting()
        {
            _transitionBG.transform.DOKill();
            _transitionFace.transform.DOKill();
            _transitionBG.transform.DOLocalMoveX(0, 0.5f);
            _transitionFace.transform.DOLocalMoveX(0, 0.7f);

            _selectBtn.ChangeColor(_selectColor);
            _selectIdx = DataManager.Instance.InsertMinion(_currentCharacter);
            SelectCount++;
        }
        private void DeSelecting()
        {
            _transitionBG.transform.DOKill();
            _transitionFace.transform.DOKill();
            _transitionBG.transform.DOLocalMoveX(_transitionBGOriginPos.x, 0.2f);
            _transitionFace.transform.DOLocalMoveX(_transitionFaceOriginPos.x, 0.35f);

            _selectBtn.ChangeColor(Color.white);
            DataManager.Instance.minions[_selectIdx] = null;
            SelectCount--;
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