using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Shy.Event
{
    public class BattleEventButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI tmp;
        public BattleEventType eventType;
        internal int per { get; private set; }
        internal UnityAction<BattleEventButton> clickEvent { private get; set; }

        public void SetPercent(Character[] _characters)
        {
            per = BattleEventValue.GetPercent(eventType, _characters[0], _characters[1]);
            tmp.SetText("성공 조건 : " + per.ToString() + " 이하");
            gameObject.SetActive(true);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            clickEvent.Invoke(this);
        }
    }
}
