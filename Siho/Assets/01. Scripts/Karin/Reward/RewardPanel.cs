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

        private void Awake()
        {
            _titleText = transform.Find("Title").GetComponent<TMP_Text>();
            _gemText = transform.Find("GemText").GetComponent<TMP_Text>();
            _coinText = transform.Find("CoinText").GetComponent<TMP_Text>();
            _button = transform.Find("ExitBtn").GetComponent<Button>();
            _canvasGroup = GetComponent<CanvasGroup>();
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
