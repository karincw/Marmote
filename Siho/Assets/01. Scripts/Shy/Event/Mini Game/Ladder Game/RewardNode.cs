using UnityEngine;
using UnityEngine.Events;

namespace Shy.Event.LadderGame
{
    public class RewardNode : LadderNode
    {
        public override void AnimateDown(UnityAction _action)
        {
            base.AnimateDown(_action);
            Debug.Log("Reward Node µ¿ÀÛ -> " + gameObject.name);
        }
    }
}