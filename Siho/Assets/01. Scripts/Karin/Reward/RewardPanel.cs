using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace karin
{
    public class RewardPanel : MonoBehaviour
    {
        private TMP_Text _titleText;
        private TMP_Text _gemText;
        private TMP_Text _coinText;
        private Button _button;
        private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeTime = 0.4f;

        private float _fadePosY;
        private RectTransform rt;

        protected virtual void Start()
        {
            Utils.FadeCanvasGroup(_canvasGroup, true, 0);
        }
        private void Awake()
        {
            rt = transform as RectTransform;
            _fadePosY = Camera.main.pixelHeight;
            rt.localPosition = new Vector2(0, _fadePosY);
            rt = transform as RectTransform;
            _fadePosY = Camera.main.pixelHeight;
            rt.localPosition = new Vector2(0, _fadePosY);
            _titleText = transform.Find("Title").GetComponent<TMP_Text>();
            _gemText = transform.Find("GemText").GetComponent<TMP_Text>();
            _coinText = transform.Find("CoinText").GetComponent<TMP_Text>();
            _button = transform.Find("ExitBtn").GetComponent<Button>();
            _canvasGroup = GetComponent<CanvasGroup>(); 
            Utils.FadeCanvasGroup(_canvasGroup, true, 0);
        }

        public void SetData(bool isWin, int gem, int coin)
        {
            _titleText.text = isWin ? "Win" : "Defeat";
            _gemText.SetText(gem.ToString());
            _coinText.SetText(coin.ToString());
            if (isWin)
                _button.onClick.AddListener(() => SceneChanger.Instance.LoadScene("WorldMap"));
            else
            {
                int index = Save.Instance.slotIndex;
                Load.Instance.saveRunDatas[index] = default;
                Save.Instance.RemoveFile(index);
                _button.onClick.AddListener(() => SceneChanger.Instance.LoadScene("Title"));
            }

        }

        public virtual void Open()
        {
            rt.DOLocalMoveY(0, _fadeTime).SetEase(Ease.Linear);
            Utils.FadeCanvasGroup(_canvasGroup, false, _fadeTime);
        }

        public virtual void Close()
        {
            rt.DOLocalMoveY(_fadePosY, _fadeTime).SetEase(Ease.Linear);
            Utils.FadeCanvasGroup(_canvasGroup, true, _fadeTime);
        }

    }
}
