using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace karin
{
    public class WarningPanel : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private TMP_Text _warningText;
        private Button _applyBtn;
        private Button _cancelBtn;

        private Action callbackAction;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            Transform buttonsParent = transform.Find("ButtonContainer").Find("Layout");
            _applyBtn = buttonsParent.Find("ApplyBtn").GetComponent<Button>();
            _cancelBtn = buttonsParent.Find("CancelBtn").GetComponent<Button>();
            _warningText = transform.Find("Text").GetComponent<TMP_Text>();
            Utils.FadeCanvasGroup(_canvasGroup, true, 0);
        }
        private void OnEnable()
        {
            _applyBtn.onClick.AddListener(Apply);
            _cancelBtn.onClick.AddListener(Cancel);
        }
        private void OnDisable()
        {
            _applyBtn.onClick.RemoveListener(Apply);
            _cancelBtn.onClick.RemoveListener(Cancel);
        }

        public void Open(string warningText, Action callback)
        {
            callbackAction = callback;
            _warningText.text = warningText;
            Utils.FadeCanvasGroup(_canvasGroup, false, 0);
        }
        public void Close()
        {
            Utils.FadeCanvasGroup(_canvasGroup, true, 0);
        }

        public void Cancel()
        {
            Close();
        }
        public void Apply()
        {
            if (callbackAction == null) return;
            callbackAction?.Invoke();
            Close();
        }
    }
}