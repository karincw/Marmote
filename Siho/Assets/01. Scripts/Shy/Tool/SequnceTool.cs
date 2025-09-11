using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using IEnumerator = System.Collections.IEnumerator;

namespace Shy
{
    public class SequnceTool : MonoBehaviour
    {
        public static SequnceTool Instance;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void Delay(UnityAction _endAction, float _delay) => StartCoroutine(DelayEvent(_endAction, _delay));

        private IEnumerator DelayEvent(UnityAction _endAction, float _delay)
        {
            yield return new WaitForSeconds(_delay);
            _endAction?.Invoke();
        }

        public void FadeInCanvasGroup(CanvasGroup _canvas, float _useTime) => StartCoroutine(FadeCanvas(_canvas, 1, _useTime, null));
        public void FadeInCanvasGroup(CanvasGroup _canvas, float _useTime, UnityAction _endAction) => StartCoroutine(FadeCanvas(_canvas, 1, _useTime, _endAction));
        public void FadeOutCanvasGroup(CanvasGroup _canvas, float _useTime, UnityAction _endAction) => StartCoroutine(FadeCanvas(_canvas, 0, _useTime, _endAction));

        private IEnumerator FadeCanvas(CanvasGroup _canvas, float lastValue, float _useTime, UnityAction _endAction)
        {
            float addValue = 0.01f, delay = _useTime * 0.01f;

            if(lastValue == 1)
                _canvas.alpha = 0;
            else
            {
                _canvas.alpha = 1;
                addValue = -0.01f;
            }

            while (_canvas.alpha != lastValue)
            {
                yield return new WaitForSeconds(delay);
                _canvas.alpha += addValue;
            }

            _endAction?.Invoke();
        }

        public void DOCountUp(TextMeshProUGUI _tmp, int _v, float _t, TextEvent _e) => StartCoroutine(CountUp(new(_tmp), int.Parse(_tmp.text), _v, _t, _e));
        public void DOCountUp(CountDownText _cdt, int _bv , int _v, float _t, TextEvent _e) => StartCoroutine(CountUp(_cdt, _bv, _v, _t, _e));

        private IEnumerator CountUp(CountDownText _cdt, int _beforeValue, int _value, float _useTime, TextEvent _event)
        {
            int _gap = Mathf.Abs(_value - _beforeValue);
            
            for (int i = 0; i < _gap; i++)
            {
                yield return new WaitForSeconds(_useTime);
                _event.EqualCheck(++_beforeValue);
                _cdt.tmp.SetText(_cdt.frontMessage + _beforeValue + _cdt.backMessage);
            }

            _event.endEvent?.Invoke();
        }

        public void DOFillAmount(Image _img, float _v, float _t, UnityAction _e) => StartCoroutine(FillAmount(_img, _v, _t, _e));

        private IEnumerator FillAmount(Image _img, float _value, float _useTime, UnityAction _endAction)
        {
            float _delay = _useTime * 0.1f, _addValue = (_value - _img.fillAmount) * 0.1f;

            for (int i = 0; i < 10; i++)
            {
                yield return new WaitForSeconds(_delay);
                _img.fillAmount += _addValue;
            }

            _endAction?.Invoke();
        }
    }
}
