using UnityEngine;

namespace karin
{
    public class GameStartBtn : SceneChangeBtn
    {
        
        private void Update()
        {
            _btn.interactable = SelectCard.SelectCount > 0;
        }

        protected override void SceneChange()
        {
            base.SceneChange();
        }
    }
}
