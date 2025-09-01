using UnityEngine;

public class MiniGame : MonoBehaviour
{
    [SerializeField] protected CanvasGroup canvasGroup;

    protected virtual void Start()
    {
        canvasGroup.gameObject.SetActive(false);
    }

    public virtual void Init()
    {
        canvasGroup.gameObject.SetActive(true);
    }
}
