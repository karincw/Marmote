using System.Collections;
using UnityEngine;
using DG.Tweening;

namespace Shy
{
    public class EndingManager : MonoBehaviour
    {
        public static EndingManager Instance;

        [Header("Dead End")]
        [SerializeField] private GameObject deadPanel;
        [SerializeField] private Transform enemyParent;
        [SerializeField] private CanvasGroup deadCanvasGroup;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            deadPanel.SetActive(false);
            deadCanvasGroup.alpha = 0;
        }

        public void PlayerDead(Transform _enemy)
        {
            deadPanel.SetActive(true);

            _enemy.SetParent(enemyParent);

            Sequence seq = DOTween.Sequence();
            seq.AppendInterval(0.2f);
            seq.Append(_enemy.DOLocalMove(new Vector2(-250, 0), 0.5f));
            seq.Join(_enemy.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f));

            GameMakeTool.Instance.DOFadeCanvasGroup(deadCanvasGroup, 0.5f);
        }

        public void GoToTitle()
        {
            if (deadCanvasGroup.alpha < 1) return;
            SceneChanger.instance.LoadScene("Title");
        }
    }
}