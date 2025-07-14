using UnityEngine;

[CreateAssetMenu(menuName = "SO/karin/TileData/Base")]
public class TileDataSO : ScriptableObject
{
    public Texture imageTexture;

    public virtual void ThrowEvent()
    {
    }
    public virtual void EnterEvent() { }
}
