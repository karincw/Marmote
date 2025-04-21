using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Shy
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance;

        [Header("Character")]
        public List<Character> minions;
        public List<Character> enemies;

        [Header("Dice")]
        [SerializeField] private DiceUi dicePrefab;
        private int diceLoop;
        private List<DiceUi> dices = new List<DiceUi>();
        private List<DiceUi> enDices = new List<DiceUi>();
        [SerializeField] private RectTransform hand;
        [SerializeField] private RectTransform handVisual;

        [Header("Other")]
        [SerializeField] private GameObject endBtn;
        private UnityAction buffEvent;


        #region Init
        private void Awake()
        {
            if (Instance != null) Destroy(this);
            else Instance = this;
        }

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            buffEvent = null;

            // Minion의 값
            CharacterSO[] pSO = DataManager.Instance.minions;

            for (int i = 0; i < minions.Count; i++)
            {
                minions[i].Init(Team.Player, pSO[i]);
                buffEvent += minions[i].BuffCheck;
            }

            // Enemy의 값
            CharacterSO[] eSO = karin.Core.DataManager.Instance.GetEnemyData.enemyList.ToArray();

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Init(Team.Enemy, eSO[i]);
                buffEvent += enemies[i].BuffCheck;
            }

            // Dice의 값
            Transform spawn = hand.Find("Groups");

            List<DiceSO> dArr = DataManager.Instance.dices.ToList();

            for (int i = 0; i < dArr.Count; i++)
            {
                DiceUi dUi = Instantiate(dicePrefab, spawn);
                dices.Add(dUi);
                dUi.gameObject.SetActive(true);
                dUi.team = Team.Player;
                dUi.Init(dArr[i]);
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                DiceUi dUi = Instantiate(dicePrefab, spawn);
                enDices.Add(dUi);
                dices.Add(dUi);
                dUi.gameObject.SetActive(true);
                dUi.team = Team.Enemy;
                dUi.HideDice();
            }

            endBtn.SetActive(false);

            StartCoroutine(TurnStart(0));
        }
        #endregion

        #region Turn
        public IEnumerator TurnStart(float _delay)
        {
            yield return new WaitForSeconds(_delay);

            // +Dice Shuffle
            for (int i = 0; i < dices.Count; i++) dices[i].HideDice();

            handVisual.sizeDelta = Vector2.zero;
            hand.sizeDelta = new Vector2(60 + 180 * dices.Count, 200);
            float sec = 0.075f / dices.Count;

            for (int i = 0; i <= dices.Count * 10; i++)
            {
                yield return new WaitForSeconds(sec);
                handVisual.sizeDelta = new Vector2(60 + 18 * i, 40);

                if(i % 10 == 8) dices[i / 10].RollDice();
            }
        }

        public void CheckTurn(DiceUi _dice)
        {
            if (_dice != dices[dices.Count - 1]) return;

            CanInteract.interact = true;
        }

        public void TurnEnd()
        {
            diceLoop = 0;

            for (int i = 0; i < dices.Count; i++)
            {
                if(dices[i].user == null) dices[i].noUsed.SetActive(true);
            }

            StartCoroutine(DiceDelay());
        }
        #endregion

        private void UseDice()
        {
            EyeSO eye = dices[diceLoop].UseDice();
            dices[diceLoop].user.SkillUse(eye.value, eye.attackWay, minions.ToArray(), enemies.ToArray());
        }

        public void NextAction()
        {
            diceLoop++;

            if(diceLoop == dices.Count)
            {
                Debug.Log("모든 다이스 종료");


                for (int i = 0; i < minions.Count; i++)
                    if (minions[i].buffs.Count != 0) buffEvent += minions[i].BuffCheck;

                for (int i = 0; i < enemies.Count; i++)
                    if (enemies[i].buffs.Count != 0) buffEvent += enemies[i].BuffCheck;

                buffEvent += () => StartCoroutine(TurnStart(0.7f));
                buffEvent.Invoke();
                return;
            }

            if (dices[diceLoop].user == null)
            {
                NextAction();
                return;
            }

            StartCoroutine(DiceDelay());
        }

        private IEnumerator DiceDelay()
        {
            yield return new WaitForSeconds(0.7f);
            UseDice();
        }

        public void CharacterDie(Team _team, Character _ch)
        {

        }

        public void EndCheck()
        {
            for (int i = 0; i < enDices.Count; i++)
            {
                if (enDices[i].user == null)
                {
                    return;
                }
            }

            endBtn.SetActive(true);
        }
    }
}
