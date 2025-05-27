using DG.Tweening;
using karin.Core;
using UnityEngine;
using UnityEngine.UI;

namespace karin
{
    public class RewardCanvas : MonoBehaviour
    {
        [SerializeField] private float _openDuration;
        [SerializeField] private float _closeDuration;

        private RewardPanel _rewardPanel;
        private Image _bg;
        private Color _bgAlphaZero;
        private readonly Vector2 offsetPanelPosition = new Vector2(0, 1500);

        private void Awake()
        {
            _rewardPanel = GetComponentInChildren<RewardPanel>();
            _bg = transform.Find("BG").GetComponent<Image>();
            _bgAlphaZero = _bg.color;
            _bgAlphaZero.a = 0;
            _bg.color = _bgAlphaZero;
            _rewardPanel.transform.localPosition = offsetPanelPosition;
        }

        public void Open(bool isWin, int gem, int coin)
        {
            _rewardPanel.SetData(isWin, gem, coin);

            DataLinkManager.Instance.Gem.Value += gem;
            DataLinkManager.Instance.Coin.Value += coin;

            _bg.DOFade(1, _openDuration / 2);
            _rewardPanel.Fade(1, _openDuration);
            _rewardPanel.transform.DOMoveY(0, _openDuration);
        }

        public void Close()
        {
            _bg.DOFade(0, _closeDuration);
            _rewardPanel.Fade(0, _closeDuration);
            _rewardPanel.transform.DOMoveY(offsetPanelPosition.y, _closeDuration);
        }
    }
}
