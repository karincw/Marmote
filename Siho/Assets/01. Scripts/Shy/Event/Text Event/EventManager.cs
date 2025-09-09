using UnityEngine;

namespace Shy.Event
{
    public class EventManager : MonoBehaviour
    {
        public static EventManager Instance;

        private TextEventManager textEvent;
        private BlackJack.BlackJack blackJack;
        private LadderGame.LadderGame ladderGame;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Debug.LogError("Find Two EventManager");

            textEvent = GetComponent<TextEventManager>();
            blackJack = GetComponent<BlackJack.BlackJack>();
            ladderGame = GetComponent<LadderGame.LadderGame>();
        }

        public void TextEventInit(EventSO _eventSO)
        {
            textEvent.Init(_eventSO);
        }

        public void LadderGameInit()
        {
            ladderGame.Init();
        }

        public void BlackJackInit()
        {
            blackJack.Init();
        }
    }
}
