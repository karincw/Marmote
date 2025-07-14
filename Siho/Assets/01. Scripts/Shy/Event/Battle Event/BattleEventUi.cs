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
        private int GetDefGap(Character[] _chs) => Mathf.RoundToInt(
            _chs[0].GetNowStat(MainStatEnum.Dex) - _chs[1].GetNowStat(MainStatEnum.Dex));

        private int GetIntGap(Character[] _chs) => Mathf.RoundToInt(
            _chs[0].GetNowStat(MainStatEnum.Int) - _chs[1].GetNowStat(MainStatEnum.Int));

        //_dValue = 1~5
        public void SetPercent(Character[] _characters, int _dValue)
        {
            int _per = 0, _def, _int;

            switch (eventType)
            {
                case BattleEvent.Run:
                    _per = BattleEventValue.RunValue[_dValue - 1];

                    _def = GetDefGap(_characters);
                    _def *= (_def >= 0) ? 8: 5;
                    _per += _def;

                    _int = GetIntGap(_characters);
                    _int *= (_int >= 0) ? 5 : 3;
                    _per += _int;
                    break;

                case BattleEvent.Surprise:
                    _per = BattleEventValue.SurpriseValue[_dValue - 1];

                    _def = GetDefGap(_characters);
                    _def *= (_def >= 0) ? 5 : 4;
                    _per += _def;
                    break;

                case BattleEvent.Talk:
                    _per = BattleEventValue.TalkValue[_dValue - 1];

                    _int = GetIntGap(_characters);
                    _int *= (_int >= 0) ? 10 : 0;
                    _per += _int;
                    break;
            }

            if (_per > 100) _per = 100;
            currentPer = _per;
            tmp.SetText("¼º°øÈ®·ü : " + _per.ToString());
        }
        #endregion

        public void OnPointerClick(PointerEventData eventData)
        {
            clickEvent.Invoke(eventType, currentPer);
        }
    }
}
