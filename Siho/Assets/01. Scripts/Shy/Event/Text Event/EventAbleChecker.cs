namespace Shy.Event
{
    public static class EventAbleChecker
    {
        #region Compare
        private static int GetItemValue(EventItemSO _so)
        {
            if (_so is Item_Stat _stat)
                return PlayerManager.Instance.GetStatCount(_stat.statType);
            else if (_so is Item_Money _money)
            {
                return MapManager.instance.money.Value;
            }
            else if (_so is Item_Synergy _synergy)
                return PlayerManager.Instance.GetSynergyCount(_synergy.item.synergyType);

            return 0;
        }

        public static bool AbleCheck(EventCondition _ev)
        {
            int _value = GetItemValue(_ev.item);

            switch (_ev.conditionType)
            {
                case ConditionType.Equal:
                    return _ev.value == _value;

                case ConditionType.More:
                    return _ev.value <= _value;

                case ConditionType.Below:
                    return _ev.value >= _value;
            }

            return true;
        }
        #endregion
    }
}