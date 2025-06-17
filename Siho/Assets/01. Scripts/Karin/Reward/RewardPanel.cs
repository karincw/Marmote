using DG.Tweening;
using TMPro;
using UnityEngine;

namespace karin
{
    public class RewardPanel : MonoBehaviour
    {
        private TMP_Text _titleText;
        private TMP_Text _gemText;
        private TMP_Text _coinText;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _titleText = transform.Find("Title").GetComponent<TMP_Text>();
            _gemText = transform.Find("GemText").GetComponent<TMP_Text>();
            _coinText = transform.Find("CoinText").GetComponent<TMP_Text>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetData(bool isWin, int gem, int coin)
        {
            _titleText.text = isWin ? "Win" : "Defeat";
            _gemText.SetText(gem.ToString());
            _coinText.SetText(coin.ToString());
            Debug.LogWarning("게임 초기화 실행");
        }

        public void Init()
        {
            _canvasGroup.alpha = 0;
        }

        public void Fade(float value, float duration)
        {
            _canvasGroup.DOFade(value, duration);
        }

    }
}
