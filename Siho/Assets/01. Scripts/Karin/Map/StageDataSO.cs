using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace karin
{
    [CreateAssetMenu(menuName = "SO/karin/stageData")]
    public class StageDataSO : ScriptableObject
    {
        public List<TileChangeData> TileChangeDatas;
        [SerializeField] private bool _isBossStage = false;
        private int _maxTile = 32;


        [ContextMenu("GetTileDatas")]
        public List<TileType> GetTileDatas()
        {
            List<TileType> tileDatas = new();
            foreach (TileChangeData data in TileChangeDatas)
            {
                for (int i = 0; i < data.changeCount; i++)
                    tileDatas.Add(data.ChangeTile);
            }
            if (tileDatas.Count < _maxTile)
            {
                int idx = _maxTile - tileDatas.Count;
                for (int i = 0; i < idx; i++)
                    tileDatas.Add(TileType.None);
            }

            tileDatas = Utils.ShuffleList(tileDatas);

            tileDatas.Insert(0, TileType.None);
            tileDatas.Insert(9, TileType.Event);
            tileDatas.Insert(18, TileType.Event);
            tileDatas.Insert(27, TileType.Event);
            tileDatas.Insert(36, TileType.ChangeStage);
            if (_isBossStage)
                tileDatas[35] = TileType.Boss;

            return tileDatas;
        }
    }
}