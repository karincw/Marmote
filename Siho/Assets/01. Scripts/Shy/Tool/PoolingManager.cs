using System.Collections.Generic;
using UnityEngine;

namespace Shy.Pooling
{
    public class PoolingManager : MonoBehaviour
    {
        public static PoolingManager Instance;

        [SerializeField] private List<PoolItem> poolingItems;
        [SerializeField] private Transform parentTrm;


        private Dictionary<PoolType, Queue<Pool>> poolDic = new Dictionary<PoolType, Queue<Pool>>();
        private Dictionary<PoolType, Pool> prefabDic = new Dictionary<PoolType, Pool>();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }

            if (parentTrm == null) parentTrm = transform;

            foreach (var _item in poolingItems)
            {
                if(poolDic.ContainsKey(_item.poolType) == false)
                {
                    prefabDic.Add(_item.poolType, _item.prefab);
                    poolDic.Add(_item.poolType, new Queue<Pool>());
                }
            }

            foreach (var _item in poolingItems)
            {
                for (int i = 0; i < _item.count; i++)
                {
                    Make(_item.poolType);
                }
            }
        }

        private Pool Make(PoolType _type, bool _return = false)
        {
            var _obj = Instantiate(prefabDic[_type], parentTrm);
            _obj.transform.localPosition = Vector3.zero;
            _obj.gameObject.SetActive(false);

            if(_return) return _obj;

            poolDic[_type].Enqueue(_obj);
            return null;
        }

        public void Push(PoolType _type, Pool _obj)
        {
            _obj.gameObject.SetActive(false);
            _obj.transform.SetParent(parentTrm);
            poolDic[_type].Enqueue(_obj);
        }

        public Pool Pop(PoolType _type)
        {
            if(poolDic[_type].Count == 0) return Make(_type, true);

            return poolDic[_type].Dequeue();
        }
    }
}
