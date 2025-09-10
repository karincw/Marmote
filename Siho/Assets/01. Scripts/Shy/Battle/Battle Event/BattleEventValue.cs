namespace Shy.Event
{
    public static class BattleEventValue
    {
        public static int GetPercent(BattleEventType _event, Character _p, Character _e)
        {
            int _value = 0;
            switch (_event)
            {
                case BattleEventType.Run:
                    _value = 40 + (_p.GetNowStat(MainStatEnum.Int) - _e.GetNowStat(MainStatEnum.Int)) * 4;
                    break;

                case BattleEventType.Surprise:
                    _value = 60 + (_p.GetNowStat(MainStatEnum.Dex) - _e.GetNowStat(MainStatEnum.Dex)) * 3;
                    break;

                case BattleEventType.Talk:
                    _value = 50 + (_p.GetNowStat(MainStatEnum.Str) - _e.GetNowStat(MainStatEnum.Str)) * 4;
                    break;
            }

            _value /= 5;

            return (_value > 20) ? 20 : (_value < 1) ? 1 : _value;
        }
    }
}