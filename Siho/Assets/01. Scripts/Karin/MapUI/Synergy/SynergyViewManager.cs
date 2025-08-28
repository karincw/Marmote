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
        var syList = Shy.PlayerManager.Instance.GetPlayerData().synergies;

        foreach (var view in _synergyViews)
        {
            if(syList.TryGetValue(view.type, out int value))
            {
                view.Value = value;
                if (view.Value == 0)
                    view.gameObject.SetActive(false);
                else
                    view.gameObject.SetActive(true);
            }
            else
            {
                view.gameObject.SetActive(false);
            }
        }
    }
}
