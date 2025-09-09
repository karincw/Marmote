using System.Collections.Generic;
using UnityEngine;

namespace Shy.Event.LadderGame
{
    public class LadderGame : MiniGame
    {
        [SerializeField] private List<Ladder> ladders;

        private Vector2Int arrSize;
        private int[] arr;

        [SerializeField][Range(0, 10)] private int linkCount = 5;

        [Header("Rewards")]
        [SerializeField] private Sprite failIcon;
        [SerializeField] private Item_Money moneySO;
        [SerializeField] private List<LadderReward> normalReward;
        [SerializeField] private List<LadderBestReward> bestRewards;

        protected override void Start()
        {
            base.Start();
            foreach (var item in ladders) item.Init(Play);
        }

        public override void Init()
        {
            base.Init();

            arrSize = new(ladders.Count, ladders[0].yValue);
            arr = new int[arrSize.y];

            foreach (var item in ladders) item.InitEvent();

            SetReward();

            SequnceTool.Instance.FadeInCanvasGroup(canvasGroup, 0.5f, () => LadderValueSet(linkCount, 0, linkCount - 2));
        }

        private void Play(LadderNode _startNode)
        {
            foreach (var item in ladders) item.ButtonOff();
            LadderLinkShow();

            _startNode.AnimateDown(() => SequnceTool.Instance.FadeOutCanvasGroup(canvasGroup, 0.8f, 
                () => canvasGroup.gameObject.SetActive(false)));
        }

        #region Ladder Setting
        private void SetReward()
        {
            List<int> _arr = new() { 0, 1, 2, 3, 4 };

            int randTotal = 0;

            foreach (var _item in normalReward) randTotal += _item.weight;

            int rand1 = Random.Range(0, randTotal), rand2 = Random.Range(0, randTotal);

            for (int i = 0; i < normalReward.Count; i++)
            {
                if (rand1 >= 0) rand1 -= normalReward[i].weight;
                if (rand2 >= 0) rand2 -= normalReward[i].weight;

                if (rand1 < 0 && rand1 != int.MinValue)
                {
                    int _rand = Random.Range(0, _arr.Count);
                    ladders[_arr[_rand]].RewardSet(moneySO, normalReward[i].value);
                    _arr.RemoveAt(_rand);
                    rand1 = int.MinValue;
                }

                if (rand2 < 0 && rand2 != int.MinValue)
                {
                    int _rand = Random.Range(0, _arr.Count);
                    ladders[_arr[_rand]].RewardSet(moneySO, normalReward[i].value);
                    _arr.RemoveAt(_rand);
                    rand2 = int.MinValue;
                }

                if (rand2 < 0 && rand1 < 0) break;
            }

            randTotal = 0;
            foreach (var _item in bestRewards) randTotal += _item.weight;
            rand1 = Random.Range(0, randTotal);

            for (int i = 0; i < bestRewards.Count; i++)
            {
                rand1 -= bestRewards[i].weight;

                if(rand1 < 0)
                {
                    int _rand = Random.Range(0, _arr.Count);
                    ladders[_arr[_rand]].RewardSet(bestRewards[i].item, bestRewards[i].value);
                    _arr.RemoveAt(_rand);
                    break;
                }
            }

            foreach (var item in _arr)
            {
                ladders[item].RewardSet(failIcon);
            }
        }

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
                    int _rand = Random.Range(0, arrSize.x - 1);
                    ladders[_rand].Link(i, ladders[_rand + 1]);
                }
                else if (arr[i] == 2)
                {
                    switch (Random.Range(0, 3))
                    {
                        case 0:
                            ladders[0].Link(i, ladders[1]);
                            ladders[2].Link(i, ladders[3]);
                            break;
                        case 1:
                            ladders[0].Link(i, ladders[1]);
                            ladders[3].Link(i, ladders[4]);
                            break;

                        case 2:
                            ladders[1].Link(i, ladders[2]);
                            ladders[3].Link(i, ladders[4]);
                            break;
                    }
                }
            }
        }
        #endregion
    }
}
