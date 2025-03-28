using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Shy
{
    public class BattleManager : MonoBehaviour
    {
        [Header("Characters")]
        public List<PCharacter> playerCharacters;
        public List<Enemy> enemies;
        private List<Character> orders = new List<Character>();

        [Header("Dices")]
        public List<DiceUI> dices;

        public string test;
        [ContextMenu("Test")]
        public void Test()
        {
            //foreach (string item in )
            //{
                Debug.Log(Formula.GetFormula(test));
            //}
        }

        #region Debuging
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0)) Order();
            if (Input.GetKeyDown(KeyCode.Alpha9)) StartCoroutine(ActionOperate());
            if (Input.GetKeyDown(KeyCode.Alpha8)) Roll();
        }

        [ContextMenu("Order")]
        private void DebugOrder()
        {
            Order();
        }
        #endregion

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            orders = new List<Character>();

            for (int i = 0; i < playerCharacters.Count; i++) orders.Add(playerCharacters[i]);
            for (int i = 0; i < enemies.Count; i++) orders.Add(enemies[i]);
        }

        public void Order()
        {
            int cnt = orders.Count;

            for (int i = 0; i < cnt - 1; i++)
            {
                for (int j = 0; j < cnt - 1 - i; j++)
                {
                    if (orders[j].spd < orders[j + 1].spd)
                    {
                        Character temp = orders[j];
                        orders[j] = orders[j + 1];
                        orders[j + 1] = temp;
                    }
                }
            }

            Debug.Log("Set Order");
        }

        public void Roll()
        {
            for (int i = 0; i < dices.Count; i++)
            {
                dices[i].RollDice();
            }
        }

        public void OnSkill(SkillSO _skill)
        {
            Character target = null;
            if(_skill.way == ActionWay.RandomEnemy)
            {
                target = enemies[Random.Range(0, enemies.Count)];
            }

            target.hp -= _skill.GetValue();

            StartCoroutine(ActionOperate());
        }

        public IEnumerator ActionOperate()
        {
            bool isFin = true;

            yield return new WaitForSeconds(0.5f);

            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].CheckDice())
                {
                    orders[i].UseAction((skill) =>OnSkill(skill));
                    isFin = false;
                    break;
                }
            }

            if(isFin)
            {
                Debug.Log("액션 종료");
            }
        }
    }
}
