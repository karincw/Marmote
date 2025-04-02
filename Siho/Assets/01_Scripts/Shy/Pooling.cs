using System.Collections.Generic;
using UnityEngine;

namespace Shy
{
    public class Pooling : MonoBehaviour
    {
        public static Pooling Instance;

        public List<PoolItem> items;
        private Dictionary<PoolingType, GameObject> poolDic = new Dictionary<PoolingType, GameObject>();

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            for (int i = 0; i < (int)PoolingType.end; i++)
            {
                GameObject obj = new GameObject();
                obj.name = ((PoolingType)i).ToString();
                obj.transform.parent = transform;
            }

            for (int i = 0; i < items.Count; i++)
            {
                Transform parent = transform.Find(items[i].type.ToString());
                poolDic.Add(items[i].type, items[i].obj);

                for (int s = 0; s < items[i].spawnCnt; s++) Make(items[i].type, parent);
            }
        }

        public GameObject Use(PoolingType _type)
        {
            Transform trm = transform.Find(_type.ToString());
            if(trm.childCount == 0) return Make(_type, trm);
            return trm.GetChild(0).gameObject;
        }

        private GameObject Make(PoolingType _type, Transform _parent)
        {
            GameObject obj = Instantiate(poolDic[_type], _parent);
            obj.name = _type.ToString();
            obj.SetActive(false);
            return obj;
        }

        public void Return(GameObject _obj)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).name == _obj.name)
                {
                    _obj.transform.parent = transform.GetChild(i);
                    return;
                }
            }
        }
    }
}
