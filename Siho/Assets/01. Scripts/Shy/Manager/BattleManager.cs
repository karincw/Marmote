using karin;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Shy.Unit;

namespace Shy
{
    public class BattleManager : MonoBehaviour
    {
        #region 변수
        public static BattleManager Instance;

        [Header("Character")]
        public List<Character> minions, enemies;
        private Dictionary<Character, List<DiceUi>> enemyDiceDic = new();

        [Header("Dice")]
        [SerializeField] private DiceUi dicePrefab;
        private int diceLoop;
        private List<DiceUi> dices = new(), enDices = new();
        [SerializeField] private RectTransform hand, handVisual;

        [Header("Other")]
        [SerializeField] private GameObject endBtn;
        private UnityAction buffEvent;
        [SerializeField] private RewardCanvas rewardPanel;
        #endregion

        #region Init
        private void Awake()
        {
            if (Instance != null) Destroy(this);
            else Instance = this;
        }

        private void Start()
        {
            rewardPanel.Close();
            Init();
        }

        public void Init()
        {
            EnemySO[] enemyDatas = null;
            //EnemySO[] enemyDatas = karin.DataLinkManager.Instance.GetEnemyData.list.ToArray();
            List<DiceSO> _diceList = DataManager.Instance.dices;
            Transform spawn = hand.Find("Groups");

            buffEvent = null;
            handVisual.sizeDelta = Vector2.zero;

            CharacterInit(Team.Player, DataManager.Instance.minions);
            CharacterInit(Team.Enemy, enemyDatas.Select(enemy => enemy.data).ToArray());

            for (int i = 0; i < _diceList.Count; i++)
            {
                DiceUi dUi = Instantiate(dicePrefab, spawn);
                dices.Add(dUi);
                dUi.Init(_diceList[i], Team.Player);
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                List<DiceUi> dUiList = new List<DiceUi>();
                foreach (DiceSO dice in enemyDatas[i].GetDices())
                {
                    DiceUi dUi = Instantiate(dicePrefab, spawn);
                    enDices.Add(dUi); dices.Add(dUi);
                    dUi.Init(dice, Team.Enemy);
                    dUiList.Add(dUi);
                }
                enemyDiceDic.Add(enemies[i], dUiList);
            }

            endBtn.SetActive(false);
            buffEvent += () => StartCoroutine(TurnStart(0.7f));
            StartCoroutine(TurnStart(0));
        }

        private void CharacterInit(Team _team, CharacterSO[] _so)
        {
            List<Character> characterList = (_team == Team.Player ? minions : enemies);

            for (int i = 0; i < characterList.Count; i++)
            {
                if (i >= _so.Length || _so[i] == null)
                {
                    characterList[i].Init(_team, null);
                    characterList.RemoveAt(i--);
                    continue;
                }

                characterList[i].Init(_team, _so[i]);
                buffEvent += characterList[i].BuffCheck;
            }
        }
        #endregion

        #region Turn
        public IEnumerator TurnStart(float _delay)
        {
            yield return new WaitForSeconds(_delay);

            //초기화
            for (int i = 0; i < dices.Count;)
            {
                if (dices[i].DiceDieCheck()) dices.RemoveAt(i);
                else i++;
            }

            //섞기
            for (int i = 0; i < 10; i++)
            {
                int rand = Random.Range(0, dices.Count);
                DiceUi temp = dices[0];
                dices[0] = dices[rand];
                dices[rand] = temp;
            }

            for (int i = 0; i < dices.Count; i++) dices[i].transform.SetSiblingIndex(i);

            hand.sizeDelta = new Vector2(60 + 180 * dices.Count, 200);
            float sec = 0.065f / dices.Count;

            for (int i = 0; i <= dices.Count * 10; i++)
            {
                yield return new WaitForSeconds(sec);
                if(i % 10 == 8) dices[i / 10].RollDice();
            }
        }

        public bool CanSelectChacter(Character _ch)
        {
            if (_ch is not Enemy _enemy) return true;
            
            int useCnt = enDices.Count(_dice => _dice.user == _enemy);
            return _enemy.actionValue > useCnt;
        }

        public void EndCheck()
        {
            Debug.Log("End Check");

            foreach (DiceUi _dice in enDices)
            {
                if (_dice.user == null) return;
            }

            endBtn.SetActive(true);
        }

        public void TurnEnd()
        {
            diceLoop = 0;

            for (int i = 0; i < dices.Count; i++)
                if (dices[i].user == null) dices[i].noUsed.SetActive(true);

            endBtn.SetActive(false);
            StartCoroutine(DiceDelay());
        }
        #endregion

        #region Dice
        public void CheckDiceAllFin(DiceUi _dice)
        {
            if (_dice != dices[dices.Count - 1]) return;
            CanInteract.interact = true;
        }

        private void UseDice()
        {
            EyeSO eye = dices[diceLoop].GetEyes();
            dices[diceLoop].user.SkillUse(eye.value, eye.attackWay, minions.ToArray(), enemies.ToArray());
        }

        public void NextAction()
        {
            if(++diceLoop >= dices.Count)
            {
                Debug.Log("모든 다이스 종료");
                buffEvent.Invoke();
                return;
            }

            StartCoroutine(DiceDelay());
        }

        private IEnumerator DiceDelay()
        {
            yield return new WaitForSeconds(0.18f);

            if (dices[diceLoop].user == null) NextAction();
            else
            {
                yield return new WaitForSeconds(0.8f);
                UseDice();
            }
        }
        #endregion

        public void HideHealthUi()
        {
            foreach (var item in minions) item.HideUi();
            foreach (var item in enemies) item.HideUi();
        }

        public void ShowHealthUi()
        {
            foreach (var item in minions) item.ShowUi();
            foreach (var item in enemies) item.ShowUi();
        }

        public void CharacterDie(Character _ch)
        {
            bool isEnemy = enemies.Contains(_ch);

            if (isEnemy) enemies.Remove(_ch);
            else minions.Remove(_ch);

            if(enemies.Count == 0 || minions.Count == 0)
            {
                rewardPanel.Open(isEnemy, 10, 10);
            }

            for (int i = 0; i < dices.Count; i++)
            {
                if (dices[i].user == _ch)
                {
                    dices[i].DeleteUser();
                    dices[i].noUsed.SetActive(true);
                }
            }
             
            if (isEnemy)
            {
                foreach (DiceUi dice in enemyDiceDic[_ch])
                {
                    dice.KillDice();
                    enDices.Remove(dice);
                }
            }
        }
    }
}
