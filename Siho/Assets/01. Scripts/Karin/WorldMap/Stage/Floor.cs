using karin.worldmap.dice;
using System;
using UnityEngine;

namespace karin.worldmap
{
    public class Floor : MonoBehaviour
    {
        public bool resultOut = true;
        public int result = -1;
        public event Action<int> OnDiceStopEvent;
        private float confirmTime = 0;
        private DiceFaceDetecter beforeFace;

        private void Update()
        {
            if (!resultOut && confirmTime >= 1.5f)
            {
                resultOut = true;
                result = beforeFace.CurrntNumber;
                OnDiceStopEvent?.Invoke(result);
                confirmTime = 0;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (resultOut) return;
            if (other.gameObject.TryGetComponent<DiceFaceDetecter>(out var face))
            {
                if (beforeFace == null) beforeFace = face;

                if (beforeFace == face)
                {
                    confirmTime += Time.fixedDeltaTime;
                }
                else
                {
                    beforeFace = face;
                    confirmTime = 0;
                }
            }
        }
    }
}