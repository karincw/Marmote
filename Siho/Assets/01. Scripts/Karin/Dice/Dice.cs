using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace karin
{
    public class Dice : MonoBehaviour
    {
        [SerializeField] private float _rollThrowPower = 10f;
        [SerializeField] private float _rollThrowDuration = 1f;
        [SerializeField] private AnimationCurve _throwCurve;

        private List<DiceFaceDetecter> _faces;
        private Floor _floor;

        private readonly Vector3[] _faceRotations =
        {
            new Vector3(90,0,0),
            new Vector3(0,90,0),
            new Vector3(0,0,90),
        };

        private void Awake()
        {
            _floor = FindFirstObjectByType<Floor>();
            _faces = GetComponentsInChildren<DiceFaceDetecter>().ToList();
            _faces.ForEach(face => face.Init(this));
        }

        [ContextMenu("DiceRoll")]
        public void DiceRoll()
        {
            Vector3 targetRotation = transform.localEulerAngles + _faceRotations[0] * Random.Range(-2, 2) + _faceRotations[1] * Random.Range(-2, 2) + _faceRotations[2] * Random.Range(-2, 2);
            _floor.resultOut = false;
            transform.DOJump(new Vector3(0, 0), _rollThrowPower, 1, _rollThrowDuration).SetEase(_throwCurve);
            transform.DOLocalRotate(targetRotation, _rollThrowDuration * 0.95f, RotateMode.FastBeyond360).SetEase(Ease.Linear);
        }
    }
}

