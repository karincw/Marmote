using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    private TMP_Text _text;

    public int Value
    {
        get => _value;
        set
        {
            if (_value == value) return;
            _value = value;
            _text.text = _value.ToString();
        }
    }
    private int _value = -1;

    private void Awake()
    {
        _text = transform.GetComponentInChildren<TMP_Text>();
        Value = 1000;

        DicePanel.OnDoubleEvent += AddMoney;
    }

    private void OnDestroy()
    {
        DicePanel.OnDoubleEvent -= AddMoney;
    }

    public void AddMoney(int value)
    {
        Value = _value + value;
    }

    public bool RemoveMoney(int value)
    {
        if ((_value - value) < 0)
            return false;

        Value = _value - value;
        return true;
    }

}
