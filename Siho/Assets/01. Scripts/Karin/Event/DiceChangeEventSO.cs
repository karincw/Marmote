using Shy;
using System.Collections.Generic;
using UnityEngine;

namespace karin
{
    [CreateAssetMenu(menuName = "SO/karin/EventS/DiceChange")]
    public class DiceChangeEventSO : EventSO
    {
        [SerializeField] private List<EventDiceBranchData> branchs = new();

        public override void Play(int index)
        {
            var currentBranch = branchs[index];
            var dataManager = DataManager.Instance;

            int characterIndex = currentBranch.characterIndex;
            if (currentBranch.usedByRandomCharacterIndex) characterIndex = Random.Range(0, dataManager.GetMinionCount);
            characterIndex = Mathf.Clamp(characterIndex, 0, dataManager.GetMinionCount - 1);

            int eyeIndex = currentBranch.eyeIndex;
            if (currentBranch.usedByRandomEyeIndex) eyeIndex = Random.Range(0, 6);

            var priviousWay = dataManager.minions[characterIndex].DiceSO.eyes[eyeIndex].attackWay;
            dataManager.minions[characterIndex].DiceSO.eyes[eyeIndex].attackWay = currentBranch.wayModify;

            string feedbackText = "";
            feedbackText += $"당신은 [{currentBranch.branchName}]를 선택했습니다.\n";
            feedbackText += currentBranch.feedbackScript;
            feedbackText += $"\n\nChange : Dice[{eyeIndex}]({priviousWay}) > Dice[{eyeIndex}]({currentBranch.wayModify})";
            EventManager.Instance.SendFeedback(feedbackText);
        }

        public override int GetBranchCount() => branchs.Count;
        public override string GetBranchName(int index) => branchs[index].branchName;
    }
}