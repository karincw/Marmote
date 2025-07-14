using AYellowpaper.SerializedCollections;
using Shy;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SynergyView : MonoBehaviour
{
    public SynergyType type;
    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            OnValueChanged?.Invoke(value);
            _value = value;
        }
    }
    private int _value = 0;
    private Action<int> OnValueChanged;

    private Image _icon;
    private TMP_Text _Text;

    private void Awake()
    {
        _icon = GetComponentInChildren<Image>();
        _Text = GetComponentInChildren<TMP_Text>();
        OnValueChanged += HandleValueChanged;
    }

    private void OnDestroy()
    {
        OnValueChanged -= HandleValueChanged;
    }

    private void HandleValueChanged(int value)
    {
        RefreshData();
    }

    private void RefreshData()
    {
        _Text.text = Value.ToString();
    }
}
