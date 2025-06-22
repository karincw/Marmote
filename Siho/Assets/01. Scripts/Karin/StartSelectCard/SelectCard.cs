using DG.Tweening;
using Shy;
using Shy.Unit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace karin
{
    public class SelectCard : ShakableUI
    {
        [Header("Configuration")]
        [SerializeField, ColorUsage(true)] private Color _disabledColor;
        [SerializeField] private CharacterSO _currentCharacter;
        [SerializeField] private int _sellPrice = 800;
        [SerializeField] private bool debugStart;

        [Header("UI References")]
        private Image _transitionBG;
        private Vector2 _transitionBGOriginPos;
        private Image _transitionFace;
        private Vector2 _transitionFaceOriginPos;
        private Image _previewImage;
        private SelectBtn _selectBtn;
        private TMP_Text _btnText;

        [Header("Selection State")]
        private Color _selectColor;
        private bool _selected;
        private int _selectIdx;
        private StartingPanel _startingPanel;

        public int SiblingIndex { get; private set; }

        private bool _canPlay;
        public bool canPlay
        {
            get => _canPlay;
            set
            {
                _canPlay = value;
                UpdateVisualState();
            }
        }

        #region Unity Events

        protected override void Awake()
        {
            base.Awake();
            CacheReferences();
            InitVisuals();
            RegisterCallbacks();
        }

        private void OnDestroy()
        {
            _selectBtn.onClick.RemoveListener(HandleSelect);
        }

        private void Start()
        {
            canPlay = debugStart;
        }

        #endregion

        #region Initialization

        private void CacheReferences()
        {
            Transform previewTrm = transform.Find("PreviewFrame");
            _transitionBG = previewTrm.Find("TransitionBG").GetComponent<Image>();
            _transitionFace = previewTrm.Find("TransitionFace").GetComponent<Image>();
            _previewImage = previewTrm.Find("Preview").GetComponent<Image>();
            _selectBtn = GetComponentInChildren<SelectBtn>();
            _startingPanel = FindFirstObjectByType<StartingPanel>();
            _btnText = _selectBtn.GetComponentInChildren<TMP_Text>();
        }

        private void InitVisuals()
        {
            _selected = false;
            _selectColor = _currentCharacter.personalColor;

            _transitionBG.color = _selectColor;
            _transitionFace.sprite = _currentCharacter.cardImage;
            _previewImage.sprite = _currentCharacter.cardImage;

            _transitionBGOriginPos = _transitionBG.transform.localPosition;
            _transitionFaceOriginPos = _transitionFace.transform.localPosition;

            SiblingIndex = transform.GetSiblingIndex();
        }

        private void RegisterCallbacks()
        {
            _selectBtn.onClick.AddListener(HandleSelect);
        }

        #endregion

        #region Selection Logic

        private void HandleSelect()
        {
            if (!_canPlay)
            {
                TryUnlock();
                return;
            }

            if (!_selected && !_startingPanel.CanAdd()) return;

            _selected = !_selected;
            if (_selected)
                Select();
            else
                Deselect();
        }

        private void Select()
        {
            PlayTransition();
            _selectBtn.ChangeColor(_selectColor);
            _selectIdx = DataManager.Instance.InsertMinion(_currentCharacter);
            _startingPanel.Add();
        }

        private void Deselect()
        {
            ResetTransition();
            _selectBtn.ChangeColor(Color.white);
            DataManager.Instance.minions[_selectIdx] = null;
            _startingPanel.Remove();
        }

        private void TryUnlock()
        {
            if (DataLinkManager.Instance.Gem >= _sellPrice)
            {
                DataLinkManager.Instance.Gem -= _sellPrice;
                canPlay = true;
            }
            else
            {
                Shake(0.3f, new Vector3(5, 10), new Vector3(0, 0, 5), Ease.OutExpo);
            }
        }

        #endregion

        #region Visuals & Animations

        private void UpdateVisualState()
        {
            _previewImage.color = _canPlay ? Color.white : _disabledColor;
            _btnText.text = _canPlay ? "Select" : $"{_sellPrice}Gem";
        }

        private void PlayTransition()
        {
            EndTransitions();
            _transitionBG.transform.DOLocalMoveX(0f, 0.5f);
            _transitionFace.transform.DOLocalMoveX(0f, 0.7f);
        }

        private void ResetTransition()
        {
            EndTransitions();
            _transitionBG.transform.DOLocalMoveX(_transitionBGOriginPos.x, 0.2f);
            _transitionFace.transform.DOLocalMoveX(_transitionFaceOriginPos.x, 0.35f);
        }

        private void EndTransitions()
        {
            _transitionBG.transform.DOComplete();
            _transitionFace.transform.DOComplete();
        }

        #endregion
    }
}
