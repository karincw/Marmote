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

    [SerializeField] private int _debug = -1;

    private int _result;
    private Animator _animator;
    private TMP_Text _resultText;
    private TMP_Text _doubleText;

    private readonly int _rollHash = Animator.StringToHash("Roll");

    public static Action<int> OnDiceRollEndEvent;
    public static Action OnDoubleEvent;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _resultText = transform.Find("CountText").GetComponent<TMP_Text>();
        _doubleText = transform.Find("DoubleText").GetComponent<TMP_Text>();
    }

    public void Roll()
    {
        _animator.SetTrigger(_rollHash);
        _result = 0;
        foreach (var face in _faces)
        {
            int dice = Random.Range(0, _diceFaces.Count);
            face.sprite = _diceFaces[dice];

            if (_result == dice + 1)
            {
                _doubleText.text = "´õºí!";
                OnDoubleEvent?.Invoke();
            }
            else
                _doubleText.text = "";

            _result += dice + 1;
        }
        _resultText.text = _result.ToString();
    }

    public void RollEnd()
    {
        if (_debug != -1)
        {
            OnDiceRollEndEvent?.Invoke(_debug);
            return;
        }
        OnDiceRollEndEvent?.Invoke(_result);
    }
}
