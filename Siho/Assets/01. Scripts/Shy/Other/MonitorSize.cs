using UnityEngine;
using UnityEngine.Playables;

public class MonitorSize : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    private void Start()
    {
        canvas.renderMode = RenderMode.WorldSpace;
        
    }
}
