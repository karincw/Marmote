using UnityEngine;

namespace Shy
{

    public class GameTester : MonoBehaviour
    {
        public CharacterDataSO player, enemy;

        private void Start()
        {
            player = player.Init();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q)) BattleStart();
        }

        public void BattleStart()
        {
            BattleManager.Instance.InitBattle(player, enemy);
        }
    }
}