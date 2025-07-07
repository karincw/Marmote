using DG.Tweening;
using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileDataSO tileData;
    [SerializeField] private bool _isSquareTile;

    [Header("Animation-Setting")]
    [SerializeField] private float _tweenTime;
    [SerializeField] private AnimationCurve _tweenEase;

    private SpriteRenderer _spriteRenderer;
    private Material _material;
    private int _valueHash = Shader.PropertyToID("_Fade");
    private int _startTextureHash = Shader.PropertyToID("_STex");
    private int _endTextureHash = Shader.PropertyToID("_ETex");
    private Texture _prevTexture;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _material = _spriteRenderer.material;
    }

    public void SetTileAnimation(TileType tileType, bool isRight)
    {
        tileData = MapManager.instance.TypeToDataDictionary[tileType];
        _prevTexture = _material.GetTexture(_endTextureHash);
        if (isRight || _isSquareTile)
        {
            _material.SetTexture(_startTextureHash, _prevTexture);
            _material.SetTexture(_endTextureHash, tileData.imageTexture);
            _material.SetFloat(_valueHash, -1);
            _material.DOFloat(2, _valueHash, _tweenTime).SetEase(_tweenEase);
        }
        else
        {
            _material.SetTexture(_endTextureHash, _prevTexture);
            _material.SetTexture(_startTextureHash, tileData.imageTexture);
            _material.SetFloat(_valueHash, 2);
            _material.DOFloat(-1, _valueHash, _tweenTime).SetEase(_tweenEase);
        }

    }
}
