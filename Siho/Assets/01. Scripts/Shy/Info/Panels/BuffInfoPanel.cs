using TMPro;
using UnityEngine;

namespace Shy.Info
{
    public class BuffInfoPanel : InfoPanel
    {
        [SerializeField] private TextMeshProUGUI explain;

        public override void UpdatePanelData(IInfoData _data)
        {
            if (_data is not BuffInfo _bInfo) return;

            nameTmp.SetText("");
            explain.SetText("");
        }
    }
}
