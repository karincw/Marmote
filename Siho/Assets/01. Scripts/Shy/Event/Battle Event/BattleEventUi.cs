using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Shy.Event
{
    public class BattleEventUi : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI tmp;
        [SerializeField] private BattleEvent eventType;
        private int currentPer;
        internal UnityEvent<BattleEvent, int> clickEvent;

        #region Percent
        public void SetPercent(Character[] _characters)
        {
            int _per = 0;

            _per = BattleEventValue.GetPercent(eventType, _characters[0], _characters[1]);
            currentPer = _per;

            tmp.SetText("성공 조건 : " + _per.ToString() + " 이하");
        }
        #endregion

        public void OnPointerClick(PointerEventData eventData)
        {
            clickEvent.Invoke(eventType, currentPer);
        }
    }
}
