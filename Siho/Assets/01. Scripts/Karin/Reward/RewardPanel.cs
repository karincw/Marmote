using Shy;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace karin
{
    public class RewardPanel : FadableUI
    {
        private TMP_Text _titleText;
        private TMP_Text _gemText;
        private TMP_Text _coinText;
        private Button _button;

        protected override void Awake()
        {
            var t = transform.Find("BG");
            _titleText = t.Find("Title").GetComponent<TMP_Text>();
            _gemText = t.Find("GemText").GetComponent<TMP_Text>();
            _coinText = t.Find("CoinText").GetComponent<TMP_Text>();
            _button = t.Find("ExitBtn").GetComponent<Button>();
            _canvasGroup = GetComponent<CanvasGroup>();
            rt = transform as RectTransform;
        }

        public void SetData(bool isWin, int gem, int coin)
        {
            if(DataLinkManager.Instance.isBossStage)
            {
                _titleText.text = isWin ? "Clear" : "Defeat";
                _gemText.SetText($"Gem : {gem.ToString()}");
                _coinText.SetText($"Coin : {coin.ToString()}");
                int index = Save.Instance.slotIndex;
                Save.Instance.RemoveFile(index);
                Load.Instance.ResetGame();
                _button.onClick.AddListener(() => SceneChanger.Instance.LoadScene("Title"));
                return;
            }
            _titleText.text = isWin ? "Win" : "Defeat";
            _gemText.SetText($"Gem : {gem.ToString()}");
            _coinText.SetText($"Coin : {coin.ToString()}");
            if (isWin)
                _button.onClick.AddListener(() => SceneChanger.Instance.LoadScene("WorldMap"));
            else
            {
                int index = Save.Instance.slotIndex;
                Save.Instance.RemoveFile(index);
                Load.Instance.ResetGame();
                _button.onClick.AddListener(() => SceneChanger.Instance.LoadScene("Title"));
            }

        }

    }
}
