using UnityEngine;

namespace Shy
{
    public static class Formula
    {
        private static float Calc(string _fir, string _sec, char _calc)
        {
            float f = float.Parse(_fir), s = float.Parse(_sec);

            if (_calc == '+') return f + s;
            if (_calc == '-') return f - s;
            if (_calc == '*') return f * s;

            Debug.LogError("Calc Error");
            return 0f;
        }

        #region Multipy
        private static string Multiple(string _mes)
        {
            int c = -1, fPos = 0;

            for (int i = 0; i < _mes.Length; i++)
            {
                if (i == _mes.Length || _mes[i] == '+' || _mes[i] == '-' || _mes[i] == '*')
                {
                    if (c != -1)
                    {
                        string fir = _mes.Substring(fPos, c - fPos);
                        string sec = _mes.Substring(c + 1, i - c - 1);

                        int value = Mathf.RoundToInt(Calc(fir, sec, _mes[c]));
                        _mes = _mes.Replace(fir + _mes[c] + sec, value.ToString());

                        i = fPos; c = -1;
                    }
                    else
                    {
                        if (i == _mes.Length) break;

                        if (_mes[i] == '*') c = i;
                        else fPos = i + 1;
                    }
                }
            }

            return _mes;
        }
        #endregion

        #region Add
        private static string Plus(string _mes)
        {
            if (_mes[0] == '(')
            {
                _mes = _mes.Remove(0, 1);
                _mes = _mes.Remove(_mes.Length - 1);
            }
            _mes += "+";
            char lastChar = ' ';

            for (int i = 0; i < _mes.Length; i++)
            {
                if (_mes[i] == lastChar && lastChar == '-')
                {
                    _mes = _mes.Remove(i - 1, 2);
                    _mes = _mes.Insert(i - 1, "+");
                }
                lastChar = _mes[i];
            }

            _mes = Multiple(_mes);
            int c = -1;

            for (int i = 0; i < _mes.Length; i++)
            {
                if (i == _mes.Length || _mes[i] == '+' || _mes[i] == '-')
                {
                    if (c != -1)
                    {
                        string fir = _mes.Substring(0, c - 0), sec = _mes.Substring(c + 1, i - c - 1);
                        int value = Mathf.RoundToInt(Calc(fir, sec, _mes[c]));
                        _mes = _mes.Replace(fir + _mes[c] + sec, value.ToString());

                        i = 0; c = -1;
                    }
                    else
                    {
                        if (i == _mes.Length) break;
                        if (_mes[i] == '+' || _mes[i] == '-') c = i;
                    }
                }
            }

            return _mes.Remove(_mes.Length - 1);
        }
        #endregion

        public static string GetFormula(string _mes)
        {
            #region °ýÈ£
            _mes = _mes.Replace(" ", "");

            int v = -1;
            string changeV = "";
            bool isFin = false;

            while (!isFin)
            {
                isFin = true;
                for (int i = 0; i < _mes.Length; i++)
                {
                    if (_mes[i] == '(') { v = i; changeV = ""; }

                    if (v >= 0) changeV += _mes[i];

                    if (_mes[i] == ')')
                    {
                        if (v == -1) { isFin = false; continue; }

                        _mes = _mes.Replace(changeV, Plus(changeV));
                        i = v - 1; v = -1;
                        changeV = "";
                        isFin = true;
                    }
                }
            }
            #endregion

            return Plus(_mes);
        }
    }
}
