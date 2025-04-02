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
        private List<DiceUi> dices = new List<DiceUi>();
        private int diceLoop;

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
            DiceUi dUi = Pooling.Instance.Use(PoolingType.Dice).GetComponent<DiceUi>();
            dUi.transform.parent = transform;
            dices.Add(dUi);
            
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
