using DG.Tweening;
using karin.Core;
using TMPro;
using UnityEngine;

namespace karin
{
    public class RewardPanel : MonoBehaviour
    {
        private TMP_Text _titleText;
        private TextSetter _gemText;
        private TextSetter _coinText;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _titleText = transform.Find("Title").GetComponent<TMP_Text>();
            _gemText = transform.Find("GemText").GetComponent<TextSetter>();
            _coinText = transform.Find("CoinText").GetComponent<TextSetter>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void SetData(bool isWin, int gem, int coin)
        {
            _titleText.text = isWin ? "Win" : "Defeat";
            _gemText.SetText(gem.ToString());
            _coinText.SetText(coin.ToString());
            if(!isWin) DataLinkManager.Instance.ResetGame();
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
