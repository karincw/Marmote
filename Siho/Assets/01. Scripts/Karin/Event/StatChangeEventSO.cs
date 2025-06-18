using Shy;
using System.Collections.Generic;
using UnityEngine;

namespace karin
{
    [CreateAssetMenu(menuName = "SO/karin/EventS/StatChange")]
    public class StatChangeEventSO : EventSO
    {
        [SerializeField] private List<EventStatBranchData> branchs = new();

        public override void Play(int index)
        {
            var currentBranch = branchs[index];
            var dataManager = DataManager.Instance;

            int characterIndex = currentBranch.characterIndex;
            if (currentBranch.usedByRandomCharacterIndex) characterIndex = Random.Range(0, dataManager.GetMinionCount);
            characterIndex = Mathf.Clamp(characterIndex, 0, dataManager.GetMinionCount - 1);

            dataManager.minions[characterIndex].stats += currentBranch.statModify;

            currentBranch.feedbackScript +=
                $"\nSelect : {currentBranch.branchName}\nChange : {currentBranch.statModify.ToString()}";
            EventManager.Instance.SendFeedback(currentBranch.feedbackScript);
        }

        public override int GetBranchCount() => branchs.Count;
        public override string GetBranchName(int index) => branchs[index].branchName;
    }
}