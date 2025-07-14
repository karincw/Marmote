using UnityEngine;

[CreateAssetMenu(menuName = "SO/karin/TileData/Enemy")]
public class EnemyTileDataSO : TileDataSO
{
    public override void EnterEvent()
    {
        SceneChanger.instance.LoadScene("New Battle");
    }
}
