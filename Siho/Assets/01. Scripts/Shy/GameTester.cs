using Shy.Event;
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
        }

        public void BattleStart()
        {
            BattleManager.Instance.InitBattle(enemy);
        }

        public void EventStart()
        {
            EventManager.Instance.InitEvent(eventSO);
        }
    }
}