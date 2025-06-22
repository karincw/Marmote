using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Shy.Unit;
using System.Collections.Generic;

namespace Shy.DarkPanel
{
    public class DarkManager : MonoBehaviour
    {
        public static DarkManager Instance;

        [SerializeField] private Image darkPanel;
        [SerializeField] private Transform characterTrm;
        private List<Character> characters = new List<Character>();

        private void Awake()
        {
            if (Instance != null) Destroy(gameObject);
            else Instance = this;
        }

        private void Start()
        {
            PanelOff(0);
        }

        public void PanelOff(float _time = 0.15f)
        {
            foreach (var _character in characters)
            {
                _character.ReturnParent();
            }

            characters = new List<Character>();

            darkPanel.DOFade(0, _time);
            darkPanel.gameObject.SetActive(false);
        }

        private void SetParent(Character _ch)
        {
            characters.Add(_ch);
            _ch.transform.SetParent(darkPanel.transform);
        }

        public void PanelOpen(Character _user, List<Character> _targets)
        {
            SetParent(_user);

            foreach (var _target in _targets)
            {
                SetParent(_target);
            }

            PanelOpen();
        }

        public void PanelOpen(Character _user, Character _target)
        {
            SetParent(_target);
            SetParent(_user);

            PanelOpen();
        }

        public void PanelOpen()
        {
            darkPanel.gameObject.SetActive(true);
            darkPanel.DOFade(0.7f, 0.15f);
        }

        public void ShowCharacter(Character _character)
        {
            _character.transform.SetParent(characterTrm);
        }
    }
}
