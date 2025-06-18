using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace karin
{
    public class SettingPanel : MonoSingleton<InventoryPanel>
    {
        private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeTime = 0.4f;
        private float _fadePosY;
        private RectTransform rt;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            rt = transform as RectTransform;
            _fadePosY = Camera.main.pixelHeight;
            Utils.FadeCanvasGroup(_canvasGroup, true, 0);
            rt.localPosition = new Vector2(0, _fadePosY);
        }

        public void Open()
        {
            rt.DOLocalMoveY(0, _fadeTime).SetEase(Ease.Linear);
            Utils.FadeCanvasGroup(_canvasGroup, false, _fadeTime);
            Setup();
        }
        public void Close()
        {
            rt.DOLocalMoveY(_fadePosY, _fadeTime).SetEase(Ease.Linear);
            Utils.FadeCanvasGroup(_canvasGroup, true, _fadeTime);
        }

        public void Setup()
        {

        }
    }
}