using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shy
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance;

        //Characters
        public List<Character> minions;
        public List<Character> enemies;

        //Dices
        private List<DiceUi> dices = new List<DiceUi>();
        private int diceLoop;
        [SerializeField] private RectTransform hand;
        [SerializeField] private RectTransform handVisual;


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
            // Minion의 값

            //PIayerData

            // Enemy의 값

            // Dice의 값
            Transform spawn = hand.Find("Groups");

            for (int i = 0; i < 1; i++)
            {
                DiceUi dUi = Pooling.Instance.Use(PoolingType.Dice).GetComponent<DiceUi>();
                dUi.transform.SetParent(spawn);
                dices.Add(dUi);
                dUi.gameObject.SetActive(true);
                dUi.team = Team.Player;
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                DiceUi dUi = Pooling.Instance.Use(PoolingType.Dice).GetComponent<DiceUi>();
                dUi.transform.SetParent(spawn);
                dices.Add(dUi);
                dUi.gameObject.SetActive(true);
                dUi.team = Team.Enemy;
            }

            StartCoroutine(TurnStart());
        }

        #region Turn
        public IEnumerator TurnStart()
        {
            // +Dice Shuffle
            for (int i = 0; i < dices.Count; i++)
            {
                dices[i].HideDice();
            }

            handVisual.sizeDelta = Vector2.zero;
            hand.sizeDelta = new Vector2(60 + 180 * dices.Count, 200);

            for (int i = 0; i <= dices.Count * 10; i++)
            {
                yield return new WaitForSeconds(0.025f);
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
            Debug.Log("Turn End");

            diceLoop = 0;

            UseDice();
        }
        #endregion

        public void UseDice()
        {
            if(diceLoop == dices.Count)
            {
                return;
            }

            if(dices[diceLoop].user == null)
            {
                return;
            }

            EyeSO eye = dices[diceLoop].UseDice();

            dices[diceLoop].user.SkillSet(eye.value, eye.attackWay, minions.ToArray(), enemies.ToArray());
        }
    }
}
