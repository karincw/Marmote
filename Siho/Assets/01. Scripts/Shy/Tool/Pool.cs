using UnityEngine;

namespace Shy.Pooling
{
    public class Pool : MonoBehaviour
    {
        public virtual void Init()
        {
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
        }
    }
}
