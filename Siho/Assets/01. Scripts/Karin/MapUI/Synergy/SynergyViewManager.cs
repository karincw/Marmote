using Shy;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SynergyViewManager : MonoBehaviour
{
    public static SynergyViewManager instance;

    [SerializeField] private List<SynergyView> _synergyViews;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }

    private void Update()
    {
        SetUp();
    }

    private void SetUp()
    {
        var syList = DataLinkManager.instance.characterData.synergies;

        foreach (var view in _synergyViews)
        {
            view.Value = syList[view.type];

            if (view.Value == 0)
                view.gameObject.SetActive(false);
            else
                view.gameObject.SetActive(true);
        }
    }
}
