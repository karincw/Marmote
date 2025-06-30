using System.Collections.Generic;
using UnityEngine;

public class SlideScreen : MonoBehaviour
{
    [SerializeField] private List<RectTransform> _screenList;

    private void Awake()
    {
        SetUp();
    }

    private void SetUp()
    {
        float sWidth = Screen.width;
        for (int i = 0; i < _screenList.Count; i++)
        {
            _screenList[i].localPosition = Vector2.right * sWidth * i;
        }
    }
}
