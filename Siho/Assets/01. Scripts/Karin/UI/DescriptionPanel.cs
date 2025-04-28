using DG.Tweening;
using Shy;
using UnityEngine;
using UnityEngine.UI;
namespace karin.ui
{
    public class DescriptionPanel : MonoBehaviour
    {
        [SerializeField] private Ease _openCurve;
        [SerializeField] private Ease _closeCurve;
        [SerializeField] private float _animationTime = 1f;

        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private Vector2 originPos;
        private SkillDescriptionPanel[] _skillDescriptionPanels;


        private void Awake()
        {
            foreach (var cp in FindObjectsByType<CharacterPanel>(FindObjectsSortMode.None))
            {
                cp.OnDescriptionOpen += Open;
            }
            _skillDescriptionPanels = FindObjectsByType<SkillDescriptionPanel>(FindObjectsSortMode.None);
            _canvasGroup = GetComponent<CanvasGroup>();
            _rectTransform = transform as RectTransform;

            originPos = _rectTransform.localPosition;

            _rectTransform.localPosition = new Vector2(originPos.x + 1080, originPos.y);
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        public void Open(CharacterSO data)
        {
            _skillDescriptionPanels[0].SetUpDate(data);
            _skillDescriptionPanels[1].SetUpDate(data);
            _skillDescriptionPanels[2].SetUpDate(data);

            _canvasGroup.DOFade(1, _animationTime);
            _rectTransform.DOLocalMoveX(originPos.x, _animationTime).SetEase(_openCurve).OnComplete(() =>
            {
                _canvasGroup.interactable = true;
            });
        }

        public void Close()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.DOFade(0, _animationTime);
            _rectTransform.DOLocalMoveX(originPos.x + 1080, _animationTime).SetEase(_closeCurve).OnComplete(() =>
            {
                _canvasGroup.blocksRaycasts = false;
            });
        }
    }
}