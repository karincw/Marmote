using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Dir = UnityEngine.UI.Image.OriginHorizontal;

namespace Shy.Event.LadderGame
{
    public class CrossNode : LadderNode
    {
        private GameObject linker;
        private Image linkerPaint, head;
        private bool isLeftNode;

        private const float headAnimeTime = 0.1f, linkerAnimeTime = 0.5f;

        internal override void Init(bool _isLinkeNode, LadderNode _downNode)
        {
            base.Init(_isLinkeNode, _downNode);

            head = transform.Find("Head").Find("Paint").GetComponent<Image>();
            linker = transform.Find("Link").gameObject;
            linkerPaint = linker.transform.Find("Paint").GetComponent<Image>();
        }

        public override void InitEvent()
        {
            base.InitEvent();

            head.fillAmount = linkerPaint.fillAmount = 0;
            head.transform.rotation = Quaternion.Euler(Vector3.zero);
            linkerPaint.fillOrigin = (int)Dir.Left;
            isLeftNode = false; 
            linker.SetActive(false);
        }

        public void LinkNode(LadderNode _linkNode, bool _isLeftNode)
        {
            linkedNode = _linkNode;
            isLeftNode = _isLeftNode;
            if (_isLeftNode) linker.SetActive(true);
        }

        public override void AnimateDown(UnityAction _action)
        {
            UnityAction<UnityAction> _anime = null;

            if (linkedNode == null)
                _anime = downNode.AnimateDown;
            else
            {
                if (isLeftNode)
                {
                    head.transform.Rotate(new(0, 0, 45));
                    var linkHead = (linkedNode as CrossNode).head;
                    linkHead.transform.Rotate(new(0, 0, 45));

                    _anime = (UnityAction _ua) => SequnceTool.Instance.DOFillAmount(linkerPaint, 1, linkerAnimeTime, () => 
                    SequnceTool.Instance.DOFillAmount(linkHead, 1, headAnimeTime, () => linkedNode.downNode.AnimateDown(_ua)));
                }
                else
                {
                    head.transform.Rotate(new(0, 0, -45));
                    _anime = linkedNode.AnimateLeft;
                }
            }

            base.AnimateDown(() => SequnceTool.Instance.DOFillAmount(head, 1, headAnimeTime, () => _anime?.Invoke(_action)));
        }

        public override void AnimateLeft(UnityAction _action)
        {
            linkerPaint.fillOrigin = (int)Dir.Right;
            head.transform.Rotate(new(0, 0, -45));

            SequnceTool.Instance.DOFillAmount(linkerPaint, 1, linkerAnimeTime, () => 
            SequnceTool.Instance.DOFillAmount(head, 1, headAnimeTime, () => downNode.AnimateDown(_action)));
        }
    }
}
