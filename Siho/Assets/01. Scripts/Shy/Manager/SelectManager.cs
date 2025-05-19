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
            blackBoard.gameObject.SetActive(false);
        }

        public void ShowCharacter(Team _targetTeam, UnityAction<Character> _act)
        {
            blackBoard.gameObject.SetActive(true);
            blackBoard.DOFade(0.8f, 0.3f);

            act = _act;
            blackBoard.transform.SetAsLastSibling();

            if (_targetTeam != Team.Player) enemies.SetAsLastSibling();
            if (_targetTeam != Team.Enemy) minions.SetAsLastSibling();
        }

        public void SelectCancel()
        {
            SelectCharacter(null);
        }

        public void SelectCharacter(Character _ch)
        {
            if (_ch == null || _ch.IsDie()) act?.Invoke(null);
            else act?.Invoke(_ch);

            act = null;
            BattleManager.Instance.EndCheck();
            blackBoard.color = Color.clear;
            blackBoard.gameObject.SetActive(false);
        }
    }
}
