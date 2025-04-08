using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shy
{
    public class Selector : MonoBehaviour
    {
        [SerializeField] private Image blackBoard;
        [SerializeField] private Transform minions;
        [SerializeField] private Transform enemies;

        public static Selector Instance;
        private static UnityAction<Character> act;

        private void Awake()
        {
            if (Instance != null) { Destroy(this); return; }
            Instance = this;

            blackBoard.color = Color.clear;
        }

        public void ShowCharacter(Team _targetTeam, UnityAction<Character> _act)
        {
            //Dotween Black Board
            blackBoard.color = new Color(0, 0, 0, 0.8f);

            blackBoard.transform.SetAsLastSibling();

            if (_targetTeam != Team.Player) enemies.SetAsLastSibling();
            if (_targetTeam != Team.Enemy) minions.SetAsLastSibling();
        }

        public void SelectCharacter(Character _ch)
        {
            act?.Invoke(_ch);
            blackBoard.color = Color.clear;
        }
    }
}
