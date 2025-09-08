using UnityEngine.Events;

namespace Shy.Event.LadderGame
{
    public class LadderButton : Button
    {
        public void Init(UnityAction _action)
        {
            onClickEvent = _action;
            useable = true;
        }
    }
}
