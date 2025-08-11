using TMPro;
using UnityEngine;

namespace Shy.Event
{
    public class RollingDice : MonoBehaviour
    {
        private Animator anime;
        [SerializeField] private TextMeshProUGUI tmp;
        [SerializeField] private Vector2Int diceRnage = Vector2Int.one;

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

            Debug.Log("Dice Value : " + _value);

            EventManager.Instance.ReturnDiceResult(_value);
        }
    }
}
