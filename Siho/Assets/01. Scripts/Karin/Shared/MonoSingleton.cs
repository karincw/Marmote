using UnityEngine;

namespace karin
{

    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T _instance = null;
        private static bool IsDestoryed = false;

        public static T Instance
        {
            get
            {
                if (IsDestoryed)
                {
                    _instance = null;
                }

                if (_instance == null)
                {
                    _instance = FindFirstObjectByType<T>();
                    if (_instance == null)
                    {
                        Debug.LogError($"{typeof(T).Name} singleton is not exists!");
                    }
                    else
                    {
                        IsDestoryed = false;
                    }
                }

                return _instance;
            }
        }

        private void OnDisable()
        {
            IsDestoryed = true;
        }

        protected virtual void Start()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
        }
        
    }

}