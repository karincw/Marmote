using UnityEngine;

namespace Shy.Event.LadderGame
{
    public class Ladder : MonoBehaviour
    {
        [SerializeField] private bool isLinkLadder = true;
        private CrossNode[] linkNode;
        private RewardNode result;
        private LadderButton button;
        public int yValue { get; private set; }

        public void ButtonOff() => button.useable = false;

        public LadderNode GetNode(int _y) => linkNode[_y];

        public void Init(UnityEngine.Events.UnityAction<LadderNode> _action)
        {
            var nodes = transform.Find("Nodes");
            yValue = nodes.childCount;
            linkNode = nodes.GetComponentsInChildren<CrossNode>();

            button = GetComponentInChildren<LadderButton>();
            result = GetComponentInChildren<RewardNode>();

            for (int i = linkNode.Length - 1; i > 0; i--)
            {
                linkNode[i].Init(isLinkLadder, linkNode[i - 1]);
            }
            linkNode[0].Init(isLinkLadder, result);
            result.Init(false, null);

            button.Init(() => _action?.Invoke(linkNode[yValue - 1]));
        }

        public void InitEvent()
        {
            foreach (var _node in linkNode) _node.InitEvent();
            result.InitEvent();
        }

        public void Link(int _y, Ladder _linkLadder)
        {
            var otherNode = _linkLadder.linkNode[_y];

            linkNode[_y].LinkNode(otherNode, true);
            otherNode.LinkNode(linkNode[_y], false);
        }
    }
}
