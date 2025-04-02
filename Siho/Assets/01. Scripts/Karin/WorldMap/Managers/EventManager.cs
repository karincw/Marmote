using UnityEngine;

namespace karin.worldmap
{
    public class EventManager : MonoSingleton<EventManager>
    {
        [SerializeField] private Canvas _backgroundCanvas;
        private CanvasGroup _backgroundGroup;
        [SerializeField] private Canvas _eventCanvas;
        private CanvasGroup _eventGroup;

        private void Awake()
        {
            _backgroundGroup = _backgroundCanvas.GetComponent<CanvasGroup>();
            _eventGroup = _eventCanvas.GetComponent<CanvasGroup>();

            _eventGroup.alpha = 0;
        }
    }
}