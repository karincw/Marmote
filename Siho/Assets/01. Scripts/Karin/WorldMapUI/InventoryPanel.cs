using DG.Tweening;
using Shy;
using Shy.Unit;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace karin
{
    public class InventoryPanel : MonoSingleton<InventoryPanel>
    {
        private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeTime = 0.4f;
        private float _fadePosY;
        private RectTransform rt;

        [HideInInspector] public List<CharacterView> characterViews;
        [HideInInspector] public InfoView _infoView;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            GetComponentsInChildren<CharacterView>(characterViews);
            _infoView = GetComponentInChildren<InfoView>();
            rt = transform as RectTransform;
            _fadePosY = Camera.main.pixelHeight;
            rt.localPosition = new Vector2(0, _fadePosY);
            Utils.FadeCanvasGroup(_canvasGroup, true, 0);
            SetUpCharacter();
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
            foreach (var view in characterViews)
            {
                view.SetUp();
            }
            _infoView.SetUp();
        }

        public void SetUpCharacter()
        {
            List<CharacterSO> characters = DataManager.Instance.minions.ToList();
            for (int i = 0; i < 3; i++)
            {
                characterViews[i].SetCharacter(characters[i]);
            }
        }
    }
}