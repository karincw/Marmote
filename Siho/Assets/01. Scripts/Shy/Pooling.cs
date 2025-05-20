using System.Collections.Generic;
using UnityEngine;

namespace Shy
{
    public class Pooling : MonoBehaviour
    {
        public static Pooling Instance;

        public List<PoolItem> items;

        private Dictionary<PoolingType, PoolItem> makeData = new Dictionary<PoolingType, PoolItem>();
        private Dictionary<PoolingType, List<GameObject>> pool = new Dictionary<PoolingType, List<GameObject>>();

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            for (int i = 0; i < items.Count; i++)
            {
                //key ¿¬°á
                makeData.Add(items[i].type, items[i]);
                pool.Add(items[i].type, new List<GameObject>());

                for (int s = 0; s < items[i].spawnCnt; s++) Make(items[i].type, true);
            }
        }

        public GameObject Use(PoolingType _type) => Use(_type, null);

        public GameObject Use(PoolingType _type, Transform _parent)
        {
            GameObject obj;

            if (pool[_type].Count == 0) obj = Make(_type, false);
            else
            {
                obj = pool[_type][0];
                pool[_type].RemoveAt(0);
            }

            //Parent Check
            if(_parent != null)
            {
                Transform trm = obj.transform;

                trm.SetParent(_parent);
                trm.localScale = Vector3.one;
                trm.localPosition = Vector3.zero;
            }

            return obj;
        }

        private GameObject Make(PoolingType _type, bool _add)
        {
            GameObject obj = Instantiate(makeData[_type].obj, transform);
            if(_add) pool[_type].Add(obj);
            obj.SetActive(false);

            return obj;
        }

        public void Return(GameObject _obj, PoolingType _type)
        {
            _obj.SetActive(false);
            _obj.transform.SetParent(transform);
            pool[_type].Add(_obj);
        }
    }
}
