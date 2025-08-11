using Shy.Pooling;
using TMPro;
using UnityEngine;

namespace Shy
{
    public class SynergyTooltipManager : MonoBehaviour
    {
        public static SynergyTooltipManager Instance;

        [SerializeField] private GameObject tooltipBox;
        [SerializeField] private TextMeshProUGUI nameTmp;
        [SerializeField] private TextMeshProUGUI explainTmp;

        private Synergy currentSynergy;

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
            currentSynergy = null;
            tooltipBox.SetActive(false);
        }

        public void Use(Synergy _synergy)
        {
            if(currentSynergy == _synergy)
            {
                Init();
            }
            else
            {
                currentSynergy = _synergy;

                nameTmp.text = _synergy.so.synergyName;
                explainTmp.text = _synergy.so.explain;

                tooltipBox.transform.position = new Vector3(0, _synergy.transform.position.y - 1, tooltipBox.transform.position.z);
                tooltipBox.SetActive(true);
            }
        }
    }
}
