using UnityEngine;
using UnityEngine.EventSystems;

namespace Shy
{
    public class Targeting : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        Vector2 clickPos;

        public void OnBeginDrag(PointerEventData eventData)
        {
            clickPos = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.localPosition += new Vector3(eventData.position.x - clickPos.x, eventData.position.y - clickPos.y);
            clickPos = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.localPosition = Vector2.zero;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
        }
    }
}
