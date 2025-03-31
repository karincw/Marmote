using DG.Tweening;
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
        public TileDataSO tileData;
        public bool canChange => isChangableTile;

        private MeshRenderer _meshRenderer;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            tileData = Instantiate(tileData);
            switch (tileData.tileType)
            {
                case TileType.None:
                    _meshRenderer.material.color = Color.white;
                    break;
                case TileType.Battle:
                    _meshRenderer.material.color = Color.red;
                    break;
                case TileType.Elite:
                    _meshRenderer.material.color = Color.black;
                    break;
                case TileType.Boss:
                    _meshRenderer.material.color = Color.blue;
                    break;
                case TileType.Shop:
                    _meshRenderer.material.color = Color.green;
                    break;
                case TileType.Event:
                    _meshRenderer.material.color = Color.cyan;
                    break;
                case TileType.ChangeStage:
                    _meshRenderer.material.color = Color.yellow;
                    break;
            }
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
        public void TileChangeAnimation(TileType tileType)
        {
            DOTween.Complete(1);
            float originYPos = transform.position.y;
            Sequence seq = DOTween.Sequence()
                .Append(transform.DOLocalMoveY(transform.position.y + _TileChangeAnimPullValue, _passingAnimPushDuration).SetEase(_passingAnimPushEase)).SetId(2)
                .AppendCallback(() =>
                {
                    tileData.tileType = tileType;
                    switch (tileType)
                    {
                        case TileType.None:
                            _meshRenderer.material.color = Color.white;
                            break;
                        case TileType.Battle:
                            _meshRenderer.material.color = Color.red;
                            break;
                        case TileType.Elite:
                            _meshRenderer.material.color = Color.black;
                            break;
                        case TileType.Boss:
                            _meshRenderer.material.color = Color.blue;
                            break;
                        case TileType.Shop:
                            _meshRenderer.material.color = Color.green;
                            break;
                        case TileType.Event:
                            _meshRenderer.material.color = Color.cyan;
                            break;
                        case TileType.ChangeStage:
                            _meshRenderer.material.color = Color.yellow;
                            break;
                    }
                }).SetId(2)
                .Append(transform.DOLocalMoveY(originYPos, _passingAnimPullDuration).SetEase(_passingAnimPullEase)).SetId(2);
        }

        public void EnterAnimation()
        {
            EnterEvent();
        }
        private void EnterEvent()
        {
            Debug.Log(tileData.tileType.ToString());
        }
    }
}