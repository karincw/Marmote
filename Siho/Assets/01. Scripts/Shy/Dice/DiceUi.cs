using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Shy
{
    public class DiceUi : MonoBehaviour, IPointerClickHandler
    {
        // Visual
        private Image visual;
        private Image icon;
        private Image userIcon;
        internal GameObject noUsed;

        // Data
        [SerializeField] private int dNum;
        [SerializeField] private DiceSO data;
        public Character user;
        public Team team;

        private void Awake()
        {
            visual = transform.Find("Visual").GetComponent<Image>();
            icon = transform.Find("Icon").GetComponent<Image>();
            userIcon = transform.Find("UserIcon").GetComponent<Image>();
            noUsed = transform.Find("None").gameObject;
        }
        
        public void Init(DiceSO _so)
        {
            data = _so;
            visual.color = data.color;
            HideDice();
        }

        public void HideDice()
        {
            visual.gameObject.SetActive(false);
            icon.gameObject.SetActive(false);
            UserReset();
            noUsed.SetActive(false);
        }

        public void UserReset()
        {
            user = null;
            userIcon.sprite = null;
            userIcon.gameObject.SetActive(false);
        }

        public void RollDice()
        {
            //Visual Set
            transform.localScale = Vector3.one;
            visual.gameObject.SetActive(true);

            //Value
            dNum = Random.Range(0, 6);
            icon.sprite = data.eyes[dNum].icon;

            //나중에 애니메이션으로 이동
            RollFin();
        }

        private void RollFin()
        {
            icon.gameObject.SetActive(true);
            BattleManager.Instance.CheckTurn(this);
        }

        public EyeSO UseDice() => data.eyes[dNum];

        private void SelectCharacter(Character _ch)
        {
            if (_ch == null) return;

            user = _ch;
            userIcon.gameObject.SetActive(true);
            userIcon.sprite = _ch.GetIcon();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!CanInteract.interact) return;

            SelectManager.Instance.ShowCharacter(team, (va)=>SelectCharacter(va));
        }
    }
}
