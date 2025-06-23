using System;
using UnityEngine;

namespace karin
{
    public class Floor : MonoBehaviour
    {
        public bool resultOut = true;
        public event Action<int> OnDiceStopEvent;

        private float _confirmTime = 0f;
        private float _confirmThreshold = 0.75f;
        
        private DiceFaceDetecter _previousFace;

        private void Update()
        {
            if(!resultOut && _confirmTime >= _confirmThreshold)
            {
                resultOut = true;
                OnDiceStopEvent?.Invoke(_previousFace.CurrntNumber);
                _previousFace = null;
                _confirmTime = 0f;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (resultOut) return;

            if (!other.TryGetComponent(out DiceFaceDetecter face)) return;

            if (_previousFace == null)
            {
                _previousFace = face;
                return;
            }

            if (_previousFace == face)
            {
                _confirmTime += Time.deltaTime;
            }
            else
            {
                _previousFace = face;
                _confirmTime = 0f;
            }
        }
    }
}
