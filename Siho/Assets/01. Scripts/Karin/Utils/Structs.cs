using Shy.Unit;
using System.Collections.Generic;

namespace karin
{
    [System.Serializable]
    public struct TileChangeData
    {
        public TileType ChangeTile;
        public int changeCount;
    }

    [System.Serializable]
    public struct SerializeEnemyList
    {
        public List<EnemySO> list;
    }

    [System.Serializable]
    public struct SerializeEventList
    {
        public List<EventSO> list;
    }

}