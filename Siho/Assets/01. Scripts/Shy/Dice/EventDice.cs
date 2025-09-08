using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Shy.Event
{
    public class EventDice : MonoBehaviour
    {
        private Animator anime;
        [SerializeField] private TextMeshProUGUI tmp;
        [SerializeField] private Vector2Int diceRnage = Vector2Int.one;

        internal UnityAction<int> diceFinEvent { private get; set; }

        private void Awake()
        {
            anime = GetComponent<Animator>();
        }

        public void Roll()
        {
            tmp.gameObject.SetActive(false);
            anime.SetTrigger("Roll");
        }

        public void EndRoll()
        {
            int _value = Random.Range(diceRnage.x, diceRnage.y + 1);
            tmp.SetText(_value.ToString());
            tmp.gameObject.SetActive(true);

            diceFinEvent?.Invoke(_value);
        }
    }
}