using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/karin/stageData")]
public class StageSO : ScriptableObject
{
    public string stageName;
    public Sprite stageImage;
    public List<MapDataSO> mapDatas;
}
