using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shy.Event.LadderGame
{
    public class LadderNode : MonoBehaviour
    {
        public LadderNode linkedNode, downNode;
        protected Image line;

        internal virtual void Init(bool _isLinkeNode, LadderNode _downNode)
        {
            line = transform.Find("Line").Find("Paint").GetComponent<Image>();

            downNode = _downNode;
        }

        public virtual void InitEvent()
        {
            linkedNode = null;
            line.fillAmount = 0;
        }

        public virtual void AnimateDown(UnityAction _action)
        {
            SequnceTool.Instance.DOFillAmount(line, 1, 0.8f, _action);
        }

        public virtual void AnimateLeft(UnityAction _action) { }
    }
}
