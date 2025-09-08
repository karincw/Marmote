using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopPanel : FadeUI
{
    [SerializeField] private List<Item> _itemList = new List<Item>();
    [SerializeField] private List<ItemDataSO> _SellItems = new List<ItemDataSO>();
    [SerializeField] private bool _useRandom = false;

    protected override void Awake()
    {
        base.Awake();
    }

    public void RefreshItems()
    {
        List<ItemDataSO> list;
        if (_useRandom)
            list = _SellItems.OrderBy(t => Random.value).ToList();
        else
            list = _SellItems.ToList();

        for (int i = 0; i < _itemList.Count; i++)
        {
            _itemList[i].Init(list[i]);
        }
    }

    [ContextMenu("ShopOpen")]
    public override void Open()
    {
        RefreshItems();
        base.Open();
    }

    public override void Close()
    {
        base.Close();
    }
}
