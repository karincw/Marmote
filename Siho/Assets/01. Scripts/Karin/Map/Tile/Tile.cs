using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public TileDataSO tileData;

    [Header("Animation-Setting")]
    [SerializeField] private float _tweenTime;
    [SerializeField] private AnimationCurve _tweenEase;

    private SpriteRenderer _spriteRenderer;
    private Material _material;
    private int _valueHash = Shader.PropertyToID("_Fade");
    private int _startTextureHash = Shader.PropertyToID("_STex");
    private int _endTextureHash = Shader.PropertyToID("_ETex");
    private Texture _prevTexture;
    private TMP_Text _numberText;

    public Action OnThrowEvent;
    public Action OnEnterEvent;

    private void Awake()
    {
        _numberText = transform.Find("Number").GetComponent<TMP_Text>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _material = _spriteRenderer.material;
        OnEnterEvent += EnterEvent;
        OnThrowEvent += ThrowEvent;
    }

    private void OnDestroy()
    {
        OnEnterEvent -= EnterEvent;
        OnThrowEvent -= ThrowEvent;
    }

    public void SetTileAnimation(TileType tileType)
    {
        tileData = MapManager.instance.TypeToDataDictionary[tileType];
        _prevTexture = _material.GetTexture(_endTextureHash);
        _material.SetTexture(_startTextureHash, _prevTexture);
        _material.SetTexture(_endTextureHash, tileData.imageTexture);
        _material.SetFloat(_valueHash, -1);
        _material.DOFloat(2, _valueHash, _tweenTime).SetEase(_tweenEase);
    }

    public void SetTileNumber(int index)
    {
        if (index < 1 || index > 12)
        {
            _numberText.text = "";
            return;
        }
        _numberText.text = index.ToString();
    }

    private void EnterEvent()
    {
        tileData.EnterEvent();
    }

    private void ThrowEvent()
    {
        tileData.ThrowEvent();
    }
}
