using UnityEngine;

namespace karin
{
    public class GameStartBtn : MoveButton
    {
        
        private void Update()
        {
            _btn.interactable = SelectCard.SelectCount > 0;
        }
    }
}
