using Shy.Pooling;
using TMPro;
using UnityEngine;

namespace Shy
{
    public class SynergyTooltipManager : MonoBehaviour
    {
        public static SynergyTooltipManager Instance;

        [SerializeField] private GameObject tooltipBox;
        [SerializeField] private TextMeshProUGUI nameTmp, explainTmp, nowValueTmp;

        private void Awake()
        {
            if(Instance == null) Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        public void Init()
        {
            tooltipBox.SetActive(false);
        }

        public void Use(Synergy _synergy)
        {
            nameTmp.text = _synergy.so.synergyName;
            explainTmp.text = _synergy.so.explain;

            var _value = _synergy.GetDataValue();
            nowValueTmp.text = $"ÇöÀç °ª : {_value}";
            nowValueTmp.gameObject.SetActive(_value != "");

            tooltipBox.SetActive(true);
        }
    }
}
