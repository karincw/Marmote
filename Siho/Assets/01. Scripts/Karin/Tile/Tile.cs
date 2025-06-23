using DG.Tweening;
using UnityEngine;

namespace karin
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
        [SerializeField] private float _imageAngle;

        public TileDataSO myTileData;
        public bool CanChange => isChangableTile;
        public bool IsBreaking => myTileData.tileType == TileType.Boss;

        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            myTileData = Instantiate(myTileData);
            _meshRenderer.material.SetFloat("_Angle", _imageAngle);
            RefreshTileData();
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

        public void TileChange(TileDataSO newTileData)
        {
            if (!CanChange) return;
            myTileData = newTileData;
            RefreshTileData();
        }

        public void TileChangeAnimation(TileDataSO newTileData)
        {
            DOTween.Complete(1);
            float originYPos = transform.position.y;
            Sequence seq = DOTween.Sequence()
                .Append(transform.DOLocalMoveY(transform.position.y + _TileChangeAnimPullValue, _passingAnimPushDuration).SetEase(_passingAnimPushEase)).SetId(2)
                .AppendCallback(() =>
                {
                    if (!CanChange) return;
                    myTileData = newTileData;
                    RefreshTileData();
                }).SetId(2)
                .Append(transform.DOLocalMoveY(originYPos, _passingAnimPullDuration).SetEase(_passingAnimPullEase)).SetId(2);
        }

        private void RefreshTileData()
        {
            _meshRenderer.material.SetColor("_BaseColor", myTileData.tileColor);
            _meshRenderer.material.SetTexture("_MainTex", myTileData.iconTexture);
        }

        public void EnterAnimation()
        {
            EnterEvent();
        }
        private void EnterEvent()
        {
            myTileData.Play();
        }
    }
}