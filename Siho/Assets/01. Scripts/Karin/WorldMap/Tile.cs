using DG.Tweening;
using System;
using UnityEngine;

namespace karin.worldmap
{
    public class Tile : MonoBehaviour
    {
        [Header("Animations Setting")]
        [SerializeField] private float _passingAnimPushValue;
        [SerializeField] private float _passingAnimPushDuration;
        [SerializeField] private AnimationCurve _passingAnimPushEase;
        [Space(10)]

        [SerializeField] private float _TileChangeAnimPullValue;
        [Space(10)]

        [SerializeField] private float _passingAnimPullDuration;
        [SerializeField] private Ease _passingAnimPullEase;

        [Space(10), Header("Tile Settings")]
        [SerializeField] private bool isChangableTile = true;
        public TileDataSO myTileData;
        public bool canChange => isChangableTile;

        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            myTileData = Instantiate(myTileData);
            ApplyTileColor();
        }

        [ContextMenu("PassingAnimation")]
        public void PassingAnimation()
        {
            DOTween.Complete(2);
            float originYPos = transform.position.y;
            Sequence seq = DOTween.Sequence()
                .Append(transform.DOLocalMoveY(transform.position.y - _passingAnimPushValue, _passingAnimPushDuration).SetEase(_passingAnimPushEase)).SetId(1)
                .Append(transform.DOLocalMoveY(originYPos, _passingAnimPullDuration).SetEase(_passingAnimPullEase)).SetId(1);
        }

        [ContextMenu("TileChangeAnimation")]
        public void TileChangeAnimation()
        {
            DOTween.Complete(1);
            float originYPos = transform.position.y;
            Sequence seq = DOTween.Sequence()
                .Append(transform.DOLocalMoveY(transform.position.y + _TileChangeAnimPullValue, _passingAnimPushDuration).SetEase(_passingAnimPushEase)).SetId(2)
                .Append(transform.DOLocalMoveY(originYPos, _passingAnimPullDuration).SetEase(_passingAnimPullEase)).SetId(2);
        }
        
        public void TileChange(TileDataSO newTileData)
        {
            myTileData = newTileData;
            ApplyTileColor();
        }

        public void TileChangeAnimation(TileDataSO newTileData)
        {
            DOTween.Complete(1);
            float originYPos = transform.position.y;
            Sequence seq = DOTween.Sequence()
                .Append(transform.DOLocalMoveY(transform.position.y + _TileChangeAnimPullValue, _passingAnimPushDuration).SetEase(_passingAnimPushEase)).SetId(2)
                .AppendCallback(() =>
                {
                    myTileData = newTileData;
                    ApplyTileColor();
                }).SetId(2)
                .Append(transform.DOLocalMoveY(originYPos, _passingAnimPullDuration).SetEase(_passingAnimPullEase)).SetId(2);
        }

        private void ApplyTileColor()
        {
            _meshRenderer.material.color = myTileData.tileColor;
        }

        public void EnterAnimation()
        {
            EnterEvent();
        }
        private void EnterEvent()
        {
            myTileData.Play();

            Debug.Log(myTileData.tileType.ToString());
        }
    }
}