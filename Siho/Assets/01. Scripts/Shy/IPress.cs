using UnityEngine;
using UnityEngine.EventSystems;

public interface IPress : IPointerDownHandler, IPointerUpHandler
{
    void ExitPress();
    void LongPress();
}
