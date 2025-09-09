using Shy.Event;
using Shy.Event.BlackJack;
using Shy.Event.LadderGame;
using UnityEngine;

namespace Shy
{
    public class GameTester : MonoBehaviour
    {
        [Header("Battle : Q")]
        public CharacterDataSO player;
        public CharacterDataSO enemy;

        [Header("Event : W")]
        public EventSO eventSO;

        [Header("Event : E")]
        public BlackJack blackJack;

        [Header("Event : R")]
        public LadderGame ladderGame;

        private void Start()
        {
            PlayerManager.Instance.Init(player);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Test Q : Battle Start");
                BattleStart();
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                Debug.Log("Test W : Event Start");
                EventStart();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Test E : BlackJack Start");
                blackJack.Init();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("Test E : Ladder Game Start");
                ladderGame.Init();
            }
        }

        public void BattleStart()
        {
            BattleManager.Instance.InitBattle(enemy);
        }

        public void EventStart()
        {
            EventManager.Instance.TextEventInit(eventSO);
        }
    }
}