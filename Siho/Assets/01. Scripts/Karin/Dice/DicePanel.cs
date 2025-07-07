using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DicePanel : MonoBehaviour
{

    [SerializeField] private List<Sprite> _diceFaces;
    [SerializeField] private Image[] _faces;

    private int _result;
    private Animator _animator;
    private TMP_Text _resultText;

    private readonly int _rollHash = Animator.StringToHash("Roll");

    public Action<int> OnDiceRollEnd;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _resultText = GetComponentInChildren<TMP_Text>();
    }

    public void Roll()
    {
        _animator.SetTrigger(_rollHash);
        _result = 0;
        foreach (var face in _faces)
        {
            int dice = Random.Range(0, _diceFaces.Count);
            face.sprite = _diceFaces[dice];
            _result += dice + 1;
        }
        _resultText.text = _result.ToString();
    }

    public void RollEnd()
    {
        OnDiceRollEnd?.Invoke(_result);
    }
}
