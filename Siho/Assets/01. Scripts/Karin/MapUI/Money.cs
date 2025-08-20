using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    private TMP_Text _text;

    public int Count
    {
        set
        {
            if (_count == value) return;
            _count = value;
            _text.text = _count.ToString();
        }
    }
    private int _count;

    private void Awake()
    {
        _text = transform.GetComponentInChildren<TMP_Text>();
        Count = 0;
    }

    public void AddMoney(int value)
    {
        Count = _count + value;
    }

    public bool RemoveMoney(int value)
    {
        if((_count - value) < 0)
            return false;

        Count = _count - value;
        return true;
    }

}
