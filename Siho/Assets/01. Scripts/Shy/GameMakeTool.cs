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

        public IEnumerator DelayEvent(UnityEvent _endEvent, float _delay)
        {
            yield return new WaitForSeconds(_delay);
            _endEvent?.Invoke();
        }

        public IEnumerator DOFadeCanvasGroup(CanvasGroup _canvas, float _useTime)
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
