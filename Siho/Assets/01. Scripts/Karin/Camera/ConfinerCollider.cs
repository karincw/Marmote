using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ConfinerCollider : MonoBehaviour
{
    private BoxCollider2D _col;

    private void Awake()
    {
        _col = GetComponent<BoxCollider2D>();
        _col.size = new Vector2(Screen.width, Screen.height);
    }
}
