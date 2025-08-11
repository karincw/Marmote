namespace Shy.Event
{
    public static class BattleEventValue
    {
        public static int GetPercent(BattleEvent _event, Character _p, Character _e)
        {
            int _value = 0;
            switch (_event)
            {
                case BattleEvent.Run:
                    _value = 60 + (_p.GetNowStat(MainStatEnum.Int) - _e.GetNowStat(MainStatEnum.Int)) * 5;
                    break;

                case BattleEvent.Surprise:
                    _value = 80 + (_p.GetNowStat(MainStatEnum.Dex) - _e.GetNowStat(MainStatEnum.Dex)) * 3;
                    break;

                case BattleEvent.Talk:
                    _value = 40 + (_p.GetNowStat(MainStatEnum.Str) - _e.GetNowStat(MainStatEnum.Str)) * 3;
                    break;
            }

            _value /= 5;

            return (_value > 20) ? 20 : (_value < 1) ? 1 : _value;
        }
    }
}