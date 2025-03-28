using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace karin.worldmap.dice
{
    public class Dice : MonoBehaviour
    {
        [SerializeField] private float _rollThrowPower = 10;
        [SerializeField] private float _rollRotatePower = 10;
        private Rigidbody _rigidBody;
        private List<DiceFaceDetecter> _faces;

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _faces = GetComponentsInChildren<DiceFaceDetecter>().ToList();
            _faces.ForEach(f => f.Init(this));
        }

        [ContextMenu("DiceRoll")]
        public void DiceRoll()
        {
            transform.position = Vector3.zero + Vector3.up;
            StartCoroutine(DiceRollCoroutine());
        }

        private IEnumerator DiceRollCoroutine()
        {
            _rigidBody.AddForce(Vector3.up * _rollThrowPower, ForceMode.Impulse);
            yield return new WaitForSeconds(0.3f);
            _rigidBody.AddTorque(Vector3.right * Random.value * _rollRotatePower, ForceMode.Impulse);
            _rigidBody.AddTorque(Vector3.up * Random.value * _rollRotatePower, ForceMode.Impulse);
            _rigidBody.AddTorque(Vector3.forward * Random.value * _rollRotatePower, ForceMode.Impulse);
        }
    }
}