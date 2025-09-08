using UnityEngine;
using UnityEngine.UI;

namespace Shy.Event.LadderGame
{
    public class CrossNode : LadderNode
    {
        private Image linkLine;
        private bool showLinkLine, isLinkNode;

        internal override void Init(bool _isLinkeNode, LadderNode _downNode)
        {
            base.Init(_isLinkeNode, _downNode);

            isLinkNode = _isLinkeNode;
            if (_isLinkeNode) linkLine = transform.Find("Link").GetComponent<Image>();
        }

        public override void InitEvent()
        {
            base.InitEvent();

            if (isLinkNode)
            {
                linkLine.color = Color.white;
                linkLine.gameObject.SetActive(false);
            }
        }

        public void LinkNode(LadderNode _linkNode, bool _showLink)
        {
            linkedNode = _linkNode;

            if (_showLink) linkLine.gameObject.SetActive(true);
        }

        internal override LadderNode GetNextNode()
        {
            if (isLinkNode) linkLine.color = showLinkLine ? Color.yellow : Color.white;
            return base.GetNextNode();
        }
    }
}
