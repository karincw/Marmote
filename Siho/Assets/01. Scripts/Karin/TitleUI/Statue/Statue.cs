using Shy;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Statue : MonoBehaviour
{
    [SerializeField] private MainStatEnum _statType;

    [SerializeField] private int _value;

    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _add;
    [SerializeField] private Button _subtract;
    private string _originText;

    private void Awake()
    {
        _originText = _text.text;

        _add.onClick.AddListener(Add);
        _subtract.onClick.AddListener(Subtract);
        UpdateStat();
    }

    private void OnDestroy()
    {
        _add.onClick.RemoveListener(Add);
        _subtract.onClick.RemoveListener(Subtract);
    }

    private void Add()
    {
        if (StatueManager.Instance.count > 0)
        {
            _value++;
            StatueManager.Instance.count--;
        }
        UpdateStat();
    }

    private void Subtract()
    {
        if (_value > 0)
        {
            _value--;
            StatueManager.Instance.count++;
        }
        UpdateStat();
    }

    private void UpdateStat()
    {
        string t = _originText;
        t = t.Replace("A", (_value).ToString());
        _text.text = t;
    }

    public MainStat resultStat()
    {
        MainStat stat = default;
        switch (_statType)
        {
            case MainStatEnum.Str:
                stat.STR = _value;
                break;
            case MainStatEnum.Dex:
                stat.DEX = _value;
                break;
            case MainStatEnum.Int:
                stat.INT = _value;
                break;
        }
        return stat;
    }
}
