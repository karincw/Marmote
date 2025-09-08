using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Shy.Pooling
{
    public class Alert : Pool
    {
        private TextMeshProUGUI tmp;
        [SerializeField] private float spd = 0.5f;

        private void Awake()
        {
            tmp = GetComponent<TextMeshProUGUI>();
        }

        public void ShowMessage(string _mes, float _t = 0f)
        {
            if (_t == 0) _t = spd;

            tmp.text = _mes;
            tmp.alpha = 0;
            gameObject.SetActive(true);

            Init();

            Sequence seq = DOTween.Sequence();

            seq.Append(tmp.DOFade(1, _t * 0.4f));
            seq.Join(transform.DOLocalMoveY(100, _t));
            seq.AppendCallback(() => PoolingManager.Instance.Push(PoolType.Alert, this));
        }
    }
}