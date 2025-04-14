using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

namespace Shy
{
    public class SelectManager : MonoBehaviour
    {
        [SerializeField] private Image blackBoard;
        [SerializeField] private Transform minions;
        [SerializeField] private Transform enemies;

        public static SelectManager Instance;
        private static UnityAction<Character> act;

        private void Awake()
        {
            if (Instance != null) { Destroy(this); return; }
            Instance = this;

            blackBoard.color = Color.clear;
        }

        public void ShowCharacter(Team _targetTeam, UnityAction<Character> _act)
        {
            blackBoard.DOFade(0.8f, 0.3f);
            //blackBoard.color = new Color(0, 0, 0, 0.8f);

            act = _act;

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
