using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shy.Info
{
    public class BuffInfoPanel : InfoPanel
    {
        [SerializeField] private TextMeshProUGUI explain;
        [SerializeField] private Image icon;

        public override void UpdatePanelData(IInfoData _data)
        {
            if (_data is not BuffInfo _bInfo) return;

            var _buff = BuffManager.Instance.GetBuff(_bInfo.buffType);

            nameTmp.SetText(_buff.itemName);
            explain.SetText(_buff.explain);
            icon.sprite = _buff.sprite;

            transform.position = _bInfo.trm.position + new Vector3(0, 0.5f);
            gameObject.SetActive(true);
        }
    }
}
