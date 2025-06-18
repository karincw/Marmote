using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace karin
{
    public class EventPanel : FadableUI
    {
        [SerializeField] private Button _buttonPrefab;

        private Transform _rightContainer;
        private Image _image;
        private TMP_Text _text;
        private EventSO _currentEvent;
        private List<GameObject> _previousButtons = new();

        protected override void Awake()
        {
            base.Awake();
            Transform panelTrm = transform.Find("Panel");
            Transform leftContainer = panelTrm.Find("LeftPanel").Find("Layout");
            _rightContainer = panelTrm.Find("RightPanel").Find("Layout");
            _image = leftContainer.Find("Image").GetComponent<Image>();
            _text = leftContainer.Find("TextArea").Find("Text").GetComponent<TMP_Text>();
        }

        public void SetEvent(EventSO evt)
        {
            _currentEvent = evt;
        }

        public void Setup()
        {
            if (_currentEvent == null)
            {
                Debug.LogWarning("CurrentEvent is not");
                return;
            }
            Sprite sprite = _currentEvent.eventImage;
            if (sprite == null)
            {
                _image.gameObject.SetActive(false);
            }
            else
            {
                _image.gameObject.SetActive(true);
                _image.sprite = sprite;
            }
            _text.text = _currentEvent.eventScript;
            for (int i = 0; i < _currentEvent.GetBranchCount(); i++)
            {
                Button btn = Instantiate(_buttonPrefab, _rightContainer);
                TMP_Text text = btn.GetComponentInChildren<TMP_Text>();

                text.text = _currentEvent.GetBranchName(i);

                var buttonObject = btn.gameObject;
                var index = i;
                btn.onClick.AddListener(() =>
                {
                    _previousButtons.Where(t => t != buttonObject).ToList().ForEach(btn => Destroy(btn));
                    _previousButtons = _previousButtons.Where(t => t != null).ToList();
                    buttonObject.GetComponent<Button>().interactable = false;
                    _currentEvent.Play(index);
                });
                _previousButtons.Add(btn.gameObject);
            }
        }

        public void SetFeedback(string feedbackScript)
        {
            _text.text = feedbackScript;
            Button btn = Instantiate(_buttonPrefab, _rightContainer);
            TMP_Text text = btn.GetComponentInChildren<TMP_Text>();

            text.text = "³¡³»±â";
            btn.onClick.AddListener(() => Close());
            _previousButtons.Add(btn.gameObject);
        }

    }
}
