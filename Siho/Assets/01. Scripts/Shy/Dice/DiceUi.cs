using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Shy
{
    public class DiceUi : MonoBehaviour, IPointerClickHandler
    {
        #region Variable
        private Image visual, icon, userIcon;
        internal GameObject noUsed;

        [SerializeField] private int dNum;
        [SerializeField] private DiceSO data;
        public Character user;
        public Team team;

        private bool isDead;
        #endregion

        #region Init
        private void Awake()
        {
            visual = transform.Find("Visual").GetComponent<Image>();
            icon = transform.Find("Icon").GetComponent<Image>();
            userIcon = transform.Find("UserIcon").GetComponent<Image>();
            noUsed = transform.Find("None").gameObject;
        }
        
        public void Init(DiceSO _so, Team _team)
        {
            gameObject.SetActive(true);
            data = _so;
            visual.color = data.color;
            team = _team;
            isDead = false;
            HideDice();
        }

        private void HideDice()
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
        #endregion

        #region Kill
        public void KillDice() => isDead = true;

        public bool DiceDieCheck()
        {
            if (isDead) Destroy(gameObject);
            else HideDice();

            return isDead;
        }
        #endregion

        #region Roll
        public void RollDice()
        {
            transform.localScale = Vector3.one;
            visual.gameObject.SetActive(true);

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
        #endregion

        #region Use
        public EyeSO UseDice() => data.eyes[dNum];

        public void CharacterSelect(Character _ch)
        {
            user = _ch;
            userIcon.gameObject.SetActive(true);
            userIcon.sprite = _ch.GetIcon();
        }

        public bool CanUseCheck() => !noUsed.activeSelf && user == null;

        public void OnPointerClick(PointerEventData eventData)
        {
            if(user == null)
            {
                if(team == Team.Player)
                {
                    noUsed.SetActive(!noUsed.activeSelf);
                }
                return;
            }

            UserReset();
        }
        #endregion
    }
}
