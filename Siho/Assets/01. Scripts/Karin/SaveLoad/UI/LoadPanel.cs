using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace karin
{

    public class LoadPanel : CanvasFadeObject
    {
        private List<LoadButton> loadButtons;

        protected override void Awake()
        {
            base.Awake();
            loadButtons = GetComponentsInChildren<LoadButton>().ToList();
        }

        public override void Open()
        {
            loadButtons.ForEach(btn => btn.SetUpViewData());
            base.Open();
        }
    }

} 