using UnityEngine;

namespace Shy.Event.LadderGame
{
    public class Ladder : MonoBehaviour
    {
        [SerializeField] private bool linkLadder = true;
        private LadderNode[] linkNode;
        private LadderButton button;
        public int yValue { get; private set; }

        public void ButtonOff() => button.useable = false;
        public LadderNode GetNode(int _y) => linkNode[_y];

        private void Awake()
        {
            var nodes = transform.Find("Nodes");

            yValue = nodes.childCount;
            linkNode = nodes.GetComponentsInChildren<LadderNode>();
            button = GetComponentInChildren<LadderButton>();
        }

        public void Init(UnityEngine.Events.UnityAction _action)
        {
            foreach (var _node in linkNode) _node.Init(linkLadder);

            button.Init(_action);
        }

        public void Link(int _y, Ladder _linkLadder)
        {
            var otherNode = _linkLadder.linkNode[_y];

            linkNode[_y].LinkNode(otherNode, true);
            otherNode.LinkNode(linkNode[_y], false);
        }
    }
}
