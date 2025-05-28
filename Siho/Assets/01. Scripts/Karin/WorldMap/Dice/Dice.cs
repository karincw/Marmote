using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace karin.worldmap.dice
{
    public class Dice : MonoBehaviour
    {
        [SerializeField] private float _rollThrowPower = 10;
        [SerializeField] private float _rollThrowDuration;
        [SerializeField] private float _rollRotatePower = 10;
        private Rigidbody _rigidBody;
        private List<DiceFaceDetecter> _faces;
        private Vector3[] rollEnd =
        {
           //new Vector3(000,0,000),
           new Vector3(270,0,090),
           new Vector3(000,0,090),
           new Vector3(000,0,180),
           new Vector3(180,0,090),
           new Vector3(090,0,090),
        };

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
            _faces = GetComponentsInChildren<DiceFaceDetecter>().ToList();
            _faces.ForEach(f => f.Init(this));
        }

        [ContextMenu("DiceRoll")]
        public void DiceRoll()
        {
            Vector3 face = rollEnd[Random.Range(0, 5)];
            Quaternion endRotation = Quaternion.Euler(face + transform.localRotation.eulerAngles);
            transform.DOJump(new Vector3(0, 0.15f), _rollThrowPower, 1, _rollThrowDuration).SetEase(Ease.Linear);
            transform.DOLocalRotateQuaternion(endRotation, _rollThrowDuration * 0.9f).SetEase(Ease.Linear);
        }

        private IEnumerator DiceRollCoroutine()
        {
            _rigidBody.linearVelocity = Vector3.zero;
            _rigidBody.AddForce(Vector3.up * _rollThrowPower, ForceMode.Impulse);
            yield return new WaitForSeconds(0.1f);
            float xPower = Random.value * 0.5f + 1f * Random.value >= 0.5f ? 1 : -1;
            float yPower = Random.value * 0.5f + 1f * Random.value >= 0.5f ? 1 : -1;
            float zPower = Random.value * 0.5f + 1f * Random.value >= 0.5f ? 1 : -1;
            _rigidBody.AddTorque(Vector3.right * xPower * _rollRotatePower, ForceMode.Impulse);
            _rigidBody.AddTorque(Vector3.up * yPower * _rollRotatePower, ForceMode.Impulse);
            _rigidBody.AddTorque(Vector3.forward * zPower * _rollRotatePower, ForceMode.Impulse);
        }
    }
}