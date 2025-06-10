using Shy.Target;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shy.Unit
{
    public class DiceUi : MonoBehaviour, IPointerClickHandler
    {
        #region Variable
        private Image visual, targetIcon, userIcon;
        private TextMeshProUGUI skillTmp;
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
            userIcon = visual.transform.GetChild(0).GetComponent<Image>();
            targetIcon = transform.Find("Target Sign").GetChild(0).GetComponent<Image>();
            skillTmp = transform.Find("Skill Num").GetComponent<TextMeshProUGUI>();
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
        public void DiceDie() => isDead = true;

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

        public bool DiceDieCheck()
        {
            if (isDead) Destroy(gameObject);
            else HideDice();

            return isDead;
        }

        private void HideDice()
        {
            visual.gameObject.SetActive(false);
            skillTmp.gameObject.SetActive(false);
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

            var eye = data.GetEye(dNum);
            skillTmp.SetText(eye.value.ToString());
            //targetIcon.sprite =  TargetManager.Instance.GetIcon(eye.attackWay);

            //나중에 애니메이션으로 이동
            RollFin();
        }

        private void RollFin()
        {
            skillTmp.gameObject.SetActive(true);
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
