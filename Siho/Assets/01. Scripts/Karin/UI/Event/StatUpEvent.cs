using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace karin.ui
{
    public class StatUpEvent : MonoBehaviour, IEvent
    {
        //private Image _eventImage;
        private TMP_Text _eventText;
        private SelectPanel _selectPanel;
        private CanvasGroup _canvasGroup;

        public StatUpEventSO currentEvent;
        private Vector3 originPos;
        private Vector3 offsetPos;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _eventText = transform.Find("Text").GetComponent<TMP_Text>();
            _selectPanel = GetComponentInChildren<SelectPanel>();
            //_eventImage = transform.Find("Image").GetComponent<Image>();
            originPos = transform.localPosition;
            offsetPos = new Vector3(0, 1080);
            transform.localPosition += offsetPos;
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

        }

        public void SetUpEvent(EventSO eventData)
        {
            //_eventImage.sprite = eventData.eventSprite;
            _eventText.text = eventData.EventChat;
            currentEvent = eventData as StatUpEventSO;

            var currentbtns = _selectPanel.SetUp(eventData.branchCount);

            int i;
            for (i = 0; i < currentbtns.Count; i++)
            {
                int idx = i;
                currentbtns[i].button.onClick.RemoveAllListeners();
                currentbtns[i].text.text = currentEvent.actions[i].branchName;
                currentbtns[i].button.onClick.AddListener(() => PlayEvent(idx));
            }
        }

        public void PlayEvent(int idx)
        {
            currentEvent.Play(idx);
            Close();
        }

        public void Open()
        {
            Sequence seq = DOTween.Sequence()
                .Append(transform.DOLocalMove(originPos, 0.5f))
                .Append(_canvasGroup.DOFade(1, 0.5f))
                .OnComplete(() =>
                {
                    _canvasGroup.interactable = true;
                    _canvasGroup.blocksRaycasts = true;
                });
        }

        public void Close()
        {
            Sequence seq = DOTween.Sequence()
           .Append(transform.DOLocalMove(originPos + offsetPos, 0.5f))
                .Append(_canvasGroup.DOFade(0, 0.5f))
                .OnComplete(() =>
                {
                    _canvasGroup.alpha = 0;
                    _canvasGroup.interactable = false;
                    _canvasGroup.blocksRaycasts = false;
                });
        }

        public void OpenEvent(EventSO eventData)
        {
            SetUpEvent(eventData);
            Open();
        }
    }
}