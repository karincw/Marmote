namespace Shy
{
    public static class Calculator
    {
        public static float GetValue(float _oldValue, float _newValue, Calculate _calc)
        {
            switch (_calc)
            {
                case Calculate.Plus: return _oldValue + _newValue;
                case Calculate.Minus: return _oldValue - _newValue;
                case Calculate.Multiply: return _oldValue * _newValue;
                case Calculate.Divide: return _oldValue / _newValue;
                case Calculate.Change: return _newValue;
                case Calculate.Percent: return _oldValue * (1 - _newValue * 0.01f);
                case Calculate.ReflectPercent: return (_oldValue / _newValue) * 100f;
            }
            return 0;
        }
    }
}