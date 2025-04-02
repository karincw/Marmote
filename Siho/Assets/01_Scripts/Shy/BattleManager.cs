using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Shy
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance;

        //Characters
        public List<Character> minions;
        public List<Character> enemies;

        //Dices
        [SerializeField] private DiceUi dicePrefab;
        private List<DiceUi> dices;
        private int diceLoop;

        

        private void Awake()
        {
            if (Instance != null) Destroy(this);
            else Instance = this;
        }

        public void Init()
        {
            // Minion의 값

            // Enemy의 값
            
            // Dice의 값

            //

        }

        public void TurnEnd()
        {
            diceLoop = 0;

            UseDice();
        }

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
