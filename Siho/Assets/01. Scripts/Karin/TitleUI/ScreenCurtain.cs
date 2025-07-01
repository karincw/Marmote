using UnityEngine;
using UnityEngine.EventSystems;

public class ScreenCurtain : FadeUI, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Close();
    }
}
