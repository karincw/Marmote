using UnityEngine;

namespace Shy
{
    public class DDOManager : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}