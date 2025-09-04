using System.Collections.Generic;
using UnityEngine;

namespace Shy.Event.LadderGame
{
    public class LadderGame : MiniGame
    {
        [SerializeField] private GameObject screen;
        [SerializeField] private List<Ladder> ladders;

        private Vector2Int arrSize;
        private int[] arr;

        [SerializeField][Range(0, 10)] private int linkCount = 5;

        protected override void Start()
        {
            base.Start();

            foreach (var item in ladders) item.Init(() => Play(item));
        }

        public override void Init()
        {
            base.Init();

            screen.SetActive(true);

            arrSize = new(ladders.Count, ladders[0].yValue);
            arr = new int[arrSize.y];

            SequnceTool.Instance.FadeInCanvasGroup(canvasGroup, 0.5f, LadderSet);
        }

        private void LadderSet()
        {
            LadderValueSet(linkCount, 0, linkCount - 2);
            LadderLinkShow();
        }

        private void Play(Ladder _ladder)
        {
            Debug.Log("Click");

            foreach (var item in ladders) item.ButtonOff();

            screen.SetActive(false);

            

            for (int i = 0; i < arrSize.y; i++)
            {
                var n = _ladder.GetNode(0);
                if (n.linkedNode != null)
                {
                    //ladder º¯°æ
                }
            }
        }

        #region Ladder Setting
        private void LadderValueSet(int _total, int _y, int _n)
        {
            if (_total == 0) return;

            int _value = Mathf.Min(2 - arr[_y], _total);
            int _row = Random.Range(0, _value + 1);

            _total -= _row;

            arr[_y++] += _row;
            LadderValueSet(_total, (_y == arrSize.y) ? 0 : _y, _n);
        }

        private void LadderLinkShow()
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == 1)
                {
                    int _rand = Random.Range(1, arrSize.x);
                    ladders[_rand].Link(i, ladders[_rand - 1]);
                }
                else if (arr[i] == 2)
                {
                    switch (Random.Range(0, 3))
                    {
                        case 0:
                            ladders[1].Link(i, ladders[0]);
                            ladders[3].Link(i, ladders[2]);
                            break;
                        case 1:
                            ladders[1].Link(i, ladders[0]);
                            ladders[4].Link(i, ladders[3]);
                            break;

                        case 2:
                            ladders[2].Link(i, ladders[1]);
                            ladders[4].Link(i, ladders[3]);
                            break;
                    }
                }
            }
        }
        #endregion
    }
}
