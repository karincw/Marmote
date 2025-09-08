using TMPro;
using UnityEngine;
using UnityEngine.Events;
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

        public void DOCountDown(TextMeshProUGUI _tmp, int _value, float _useTime, TextEvent _event) => StartCoroutine(CountDown(_tmp, _value, _useTime, _event));

        private IEnumerator CountDown(TextMeshProUGUI _tmp, int _value, float _useTime, TextEvent _event)
        {
            int _alreadyValue = int.Parse(_tmp.text);
            int _gap = Mathf.Abs(_value - _alreadyValue);
            
            for (int i = 0; i < _gap; i++)
            {
                yield return new WaitForSeconds(_useTime);
                _event.EqualCheck(++_alreadyValue);
                _tmp.SetText(_alreadyValue.ToString());
            }

            _event.endEvent?.Invoke();
        }
    }
}
