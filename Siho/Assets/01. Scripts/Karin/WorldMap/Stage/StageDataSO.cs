using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace karin.worldmap
{
    [CreateAssetMenu(menuName = "SO/karin/stageData")]
    public class StageDataSO : ScriptableObject
    {
        public List<TileChangeData> TileChangeDatas;
        [SerializeField] private TileDataSO _noneTile;
        [SerializeField] private bool _isBossStage = false;
        [SerializeField] private TileDataSO _bossTile;
        [SerializeField] private TileDataSO _stageChangeTile;
        private int _maxTile = 32;


        [ContextMenu("GetTileDatas")]
        public List<TileDataSO> GetTileDatas()
        {
            List<TileDataSO> tileDatas = new();
            foreach (TileChangeData data in TileChangeDatas)
            {
                for (int i = 0; i < data.changeCount; i++)
                    tileDatas.Add(data.ChangeTile);
            }
            if (tileDatas.Count < _maxTile)
            {
                int idx = _maxTile - tileDatas.Count;
                for (int i = 0; i < idx; i++)
                    tileDatas.Add(_noneTile);
            }

            tileDatas = Utils.ShuffleList(tileDatas);

            var startTile = _isBossStage ? _bossTile : _stageChangeTile;
            
            tileDatas.Insert(0, _noneTile);
            tileDatas.Insert(9, _noneTile);
            tileDatas.Insert(18, _noneTile);
            tileDatas.Insert(27, _noneTile);
            tileDatas.Insert(36, startTile);

            StringBuilder sb = new StringBuilder();
            foreach (var data in tileDatas)
            {
                sb.Append(data.ToString() +"\n");
            }
            Debug.Log(sb.ToString());

            return tileDatas;
        }
    }
}