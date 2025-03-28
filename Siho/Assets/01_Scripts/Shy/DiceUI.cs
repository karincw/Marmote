using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Shy
{
    public class DiceUI : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private Vector2 clickPos = Vector2.zero;
        private Vector2 lastPos;
        [Range(1, 6)] private int value;
        private TextMeshProUGUI txt;
        private GameObject visual, target;
        private bool canMove = false;

        private void Awake()
        {
            txt = GetComponentInChildren<TextMeshProUGUI>();
            visual = transform.GetChild(0).gameObject;
        }

        public void RollDice()
        {
            value = Random.Range(0, 6) + 1;
            visual.SetActive(true);
            txt.text = value.ToString();
            canMove = true;
        }

        #region DiceMove
        public void OnBeginDrag(PointerEventData eventData)
        {
            clickPos = eventData.position;
            lastPos = transform.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!canMove) return;

            transform.localPosition += new Vector3(eventData.position.x - clickPos.x, eventData.position.y - clickPos.y);
            clickPos = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if(target != null)
            {
                target.GetComponentInParent<PCharacter>().nums.Add(value - 1);
                visual.SetActive(false);
            }
            transform.position = lastPos;
        }
        #endregion

        public void OnPointerClick(PointerEventData eventData)
        {
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Player"))
            {
                target = collision.gameObject;
            }
        }
    }
}
