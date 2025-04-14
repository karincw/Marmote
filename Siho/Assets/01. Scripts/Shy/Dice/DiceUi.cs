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
        }

        public void HideDice()
        {
            visual.gameObject.SetActive(false);
            icon.gameObject.SetActive(false);
            userIcon.gameObject.SetActive(false);
        }

        public void RollDice()
        {
            //Visual Set
            transform.localScale = Vector2.one;
            visual.gameObject.SetActive(true);

            //Value
            dNum = Random.Range(0, 6);
            //icon.sprite = data.eyes[dNum].icon;
            user = null;

            //나중에 애니메이션으로 이동
            RollFin();
        }

        public void RollFin()
        {
            icon.gameObject.SetActive(true);
            BattleManager.Instance.CheckTurn(this);
        }

        public EyeSO UseDice()
        {
            //user.UseSkill(data.eyes[dNum].value, data.eyes[dNum].attackWay);
            return data.eyes[dNum];
        }

        public void SelectCharacter(Character _ch)
        {
            Debug.Log("User Change : " + _ch.gameObject.name);
            user = _ch;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!CanInteract.interact) return;

            Debug.Log("Dice Click by " + transform.GetSiblingIndex() + gameObject.name);

            SelectManager.Instance.ShowCharacter(team, (va)=>SelectCharacter(va));
        }
    }
}
