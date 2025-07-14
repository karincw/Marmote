using Shy.Data;
using Shy.Event;
using UnityEngine;

namespace Shy
{

    public class GameTester : MonoBehaviour
    {
        [Header("Battle")]
        public CharacterDataSO player, enemy;

        [Header("Event")]
        public EventSO eventSO;

        private void Start()
        {
            GameData.Init(player);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q)) BattleStart();
            if (Input.GetKeyDown(KeyCode.W)) EventStart();
        }

        public void BattleStart()
        {
            BattleManager.Instance.InitBattle(player, enemy);
        }

        public void EventStart()
        {
            EventManager.Instance.InitEvent(eventSO);
        }
    }
}