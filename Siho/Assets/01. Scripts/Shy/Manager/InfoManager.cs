using UnityEngine;
using Shy.Unit;
using TMPro;

namespace Shy.Info
{
    public class InfoManager : MonoBehaviour
    {
        public static InfoManager Instance;

        [SerializeField] private InfoPanel infoPanel;

        private float distance = 4f;
        private Character currentCharacter;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            CloseInfoPanel();
        }

        public void OpenInfoPanel(Transform _targetPos, Character _character, CharacterSO _so)
        {
            currentCharacter = _character;

            float x = distance;
            if(_targetPos.position.x > 0) x = -distance;

            Vector3 addPos = new Vector3(x, -0.9f);
            infoPanel.transform.position = _targetPos.position + addPos;

            //Data Update
            infoPanel.gameObject.SetActive(true);
        }

        private void CloseInfoPanel()
        {
            currentCharacter = null;
            infoPanel.gameObject.SetActive(false);
        }

        public void CloseInfoPanel(Character _character)
        {
            if (_character != currentCharacter) return;
            CloseInfoPanel();
        }
    }
}
