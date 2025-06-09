using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Shy.Unit
{
    public class DiceUi : MonoBehaviour, IPointerClickHandler
    {
        #region Variable
        private Image visual, icon, userIcon;
        private GameObject noUsedIcon;

        [SerializeField] private int dNum;
        [SerializeField] private DiceSO data;
        private Character user;
        public Team team;

        private bool isDead;
        #endregion

        #region Init
        private void Awake()
        {
            visual = transform.Find("Visual").GetComponent<Image>();
            icon = transform.Find("Icon").GetComponent<Image>();
            userIcon = transform.Find("UserIcon").GetComponent<Image>();
            noUsedIcon = transform.Find("None").gameObject;
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
        #endregion

        #region Die
        private void DeleteUser()
        {
            user = null;
            userIcon.sprite = null;
            userIcon.gameObject.SetActive(false);
        }

        public void CharacterDeadCheck(Character _user)
        {
            if (CharacterCheck(_user)) return;
            DeleteUser();
            UseBlock();
        }

        public void DiceDie() => isDead = true;

        public bool DiceDieCheck()
        {
            if (isDead) Destroy(gameObject);
            else HideDice();

            return isDead;
        }

        private void HideDice()
        {
            visual.gameObject.SetActive(false);
            icon.gameObject.SetActive(false);
            noUsedIcon.SetActive(false);
            DeleteUser();
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
            BattleManager.Instance.CheckDiceAllFin(this);
        }
        #endregion

        #region Use
        public bool CharacterCheck(Character _ch) => user == _ch;
        private void UseBlock() => noUsedIcon.SetActive(true);

        public DiceData GetData()
        {
            EyeSO eye = data.GetEye(dNum);
            DiceData dd;
            dd.user = user;
            dd.actionWay = eye.attackWay;
            dd.team = team;
            dd.skillNum = eye.value;
            return dd;
        }

        public void SelectUser(Character _ch)
        {
            if (_ch.team != team) return;
            if (!BattleManager.Instance.CanSelectChacter(_ch)) return;

            user = _ch;
            userIcon.gameObject.SetActive(true);
            userIcon.sprite = _ch.GetIcon();
        }

        public void UseCheck()
        {
            if (user == null) UseBlock();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            DeleteUser();
        }
        #endregion
    }
}
