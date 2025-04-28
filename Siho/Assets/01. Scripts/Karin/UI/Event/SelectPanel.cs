using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace karin.ui
{
    public class SelectPanel : MonoBehaviour
    {
        private List<BranchBtn> branchBtns = new();
        [SerializeField] private BranchBtn branchPrefab;

        public List<BranchBtn> SetUp(int btnCount)
        {
            if (branchBtns.Count < btnCount)
            {
                MakeButtons(btnCount - branchBtns.Count);
            }
            EnableButtons(btnCount);

            return branchBtns.GetRange(0, btnCount);
        }

        private void MakeButtons(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var newBtn = Instantiate(branchPrefab, transform);
                branchBtns.Add(newBtn);
            }
        }

        private void EnableButtons(int count)
        {
            for (int i = 0; i < branchBtns.Count; i++)
            {
                branchBtns[i].gameObject.SetActive(i < count);
            }
        }
    }
}