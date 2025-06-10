using UnityEngine;
using Shy.Unit;
using TMPro;
using System.Collections.Generic;

namespace Shy.Info
{
    public class InfoManager : MonoBehaviour
    {
        public static InfoManager Instance;

        [SerializeField] private InfoPanel buffPanel,minionPanel, enemyPanel;
        private Dictionary<InfoType, InfoPanel> panels = new Dictionary<InfoType, InfoPanel>();

        private float distance = 4f;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            panels.Add(InfoType.Minion, minionPanel);
            panels.Add(InfoType.Enemy, enemyPanel);
            panels.Add(InfoType.Buff, buffPanel);
        }

        private void Start()
        {
            CloseAllPanel();
        }

        public void OpenInfoPanel(Transform _targetPos, Character _character, CharacterSO _so)
        {
            float x = distance;
            if(_targetPos.position.x > 0) x = -distance;

            Vector3 addPos = new Vector3(x, -0.9f);


            //infoPanel.transform.position = _targetPos.position + addPos;
            //
            //infoPanel.gameObject.SetActive(true);
        }

        public void OpenPanel(InfoType _infoType, IInfoData _infoData)
        {
            InfoPanel _panel = panels[_infoType];

            _panel.UpdatePanelData(_infoData);
            _panel.gameObject.SetActive(true);
        }

        private void CloseAllPanel()
        {
            foreach (var _panel in panels.Values)
            {
                _panel.gameObject.SetActive(false);
            }
        }

        public void ClosePanel(InfoType _infoType)
        {
            panels[_infoType].gameObject.SetActive(false);
        }
    }
}
