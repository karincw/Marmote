using System;
using UnityEngine;
using UnityEngine.UI;

namespace Shy.Event.LadderGame
{
    public class LadderNode : MonoBehaviour
    {
        public LadderNode linkedNode;
        private Image line, linkLine;

        private bool isLinkNode;

        private void Awake()
        {
            line = transform.Find("Line").GetComponent<Image>();
        }

        public void LinkNode(LadderNode _linkNode, bool _showLink)
        {
            linkedNode = _linkNode;

            if (_showLink) linkLine.gameObject.SetActive(true);
        }

        internal void Init(bool _isLinkeNode)
        {
            isLinkNode = _isLinkeNode;
            linkedNode = null;

            if (isLinkNode)
            {
                linkLine = transform.Find("Link").GetComponent<Image>();
                linkLine.color = Color.white;
                linkLine.gameObject.SetActive(false);
            }

            line.color = Color.white;
        }
    }
}
