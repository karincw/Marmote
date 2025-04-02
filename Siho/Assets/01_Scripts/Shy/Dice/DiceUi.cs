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

        private void Awake()
        {
            visual = transform.Find("Visual").GetComponent<Image>();
            icon = transform.Find("Icon").GetComponent<Image>();
            userIcon = transform.Find("UserIcon").GetComponent<Image>();

            HideDice();
        }

        public void HideDice()
        {
            visual.gameObject.SetActive(false);
            icon.gameObject.SetActive(false);
            userIcon.gameObject.SetActive(false);
        }

        public void RollDice()
        {
            visual.gameObject.SetActive(true);
            dNum = Random.Range(0, 6);

            //나중에 애니메이션으로 이동
            RollFin();
        }

        public void RollFin()
        {
            //icon visual Update
            icon.gameObject.SetActive(true);
        }

        public EyeSO UseDice()
        {
            //user.UseSkill(data.eyes[dNum].value, data.eyes[dNum].attackWay);
            return data.eyes[dNum];
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Dice Click by " + gameObject.name);
        }
    }
}
