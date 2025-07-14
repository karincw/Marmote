using UnityEngine;
using UnityEngine.Events;
using IEnumerator = System.Collections.IEnumerator;

namespace Shy
{
    public class GameMakeTool : MonoBehaviour
    {
        public static GameMakeTool Instance;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void Delay(UnityAction _endEvent, float _delay) => StartCoroutine(DelayEvent(_endEvent, _delay));

        public IEnumerator DelayEvent(UnityAction _endEvent, float _delay)
        {
            yield return new WaitForSeconds(_delay);
            _endEvent?.Invoke();
        }

        public void DOFadeCanvasGroup(CanvasGroup _canvas, float _useTime) => StartCoroutine(FadeCanvasGroup(_canvas, _useTime));

        public IEnumerator FadeCanvasGroup(CanvasGroup _canvas, float _useTime)
        {
            _canvas.alpha = 0;
            float delay = _useTime * 0.01f;

            while (_canvas.alpha < 1)
            {
                yield return new WaitForSeconds(delay);
                _canvas.alpha += 0.01f;
            }
        }
    }
}
