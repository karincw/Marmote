using TMPro;
using UnityEngine;

namespace Shy.Info
{
    public abstract class InfoPanel : MonoBehaviour
    {
        public TextMeshProUGUI nameTmp;
        public abstract void UpdatePanelData(IInfoData _data);
    }
}