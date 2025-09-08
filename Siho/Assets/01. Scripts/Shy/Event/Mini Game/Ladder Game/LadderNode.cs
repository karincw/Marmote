using UnityEngine;
using UnityEngine.UI;

namespace Shy.Event.LadderGame
{
    public class LadderNode : MonoBehaviour
    {
        public LadderNode linkedNode, downNode;
        protected Image line;

        internal virtual void Init(bool _isLinkeNode, LadderNode _downNode)
        {
            line = transform.Find("Line").GetComponent<Image>();
            downNode = _downNode;
        }

        public virtual void InitEvent()
        {
            linkedNode = null;
            line.color = Color.white;
        }

        internal virtual LadderNode GetNextNode()
        {
            line.color = Color.yellow;
            return linkedNode ? linkedNode.downNode : downNode;
        }
    }
}
