using System.Collections.Generic;
using UnityEngine;

namespace karin.worldmap
{
    [CreateAssetMenu(menuName = "SO/karin/stageData")]
    public class StageDataSO : ScriptableObject
    {
        public List<TileChangeData> TileChangeDatas;
    }
}