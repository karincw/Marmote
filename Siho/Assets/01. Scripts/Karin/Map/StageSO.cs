using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/karin/stageData")]
public class StageSO : ScriptableObject
{
    public string stageName;
    public int stageIndex;
    public Sprite stageImage;
    public List<MapDataSO> mapDatas;
}
