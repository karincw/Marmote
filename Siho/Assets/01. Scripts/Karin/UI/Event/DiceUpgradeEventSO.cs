using UnityEngine;
using ShyDataManager = Shy.DataManager;

namespace karin.ui
{
    [CreateAssetMenu(menuName = "SO/karin/DiceUpgradeEventSO")]
    public class DiceUpgradeEventSO : EventSO
    {
        public StatUpData<DiceChangeData>[] actions = new StatUpData<DiceChangeData>[6];
        public override int branchCount => actions.Length;

        public override void Play(int index)
        {
            var targetAction = actions[index];
            var target = ShyDataManager.Instance.minions[actions[index].index];
            foreach (var change in targetAction.branchActions)
            {

            }
        }
    }
}