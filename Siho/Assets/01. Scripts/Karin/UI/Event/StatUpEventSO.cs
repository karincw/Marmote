using UnityEngine;
using ShyDataManager = Shy.DataManager;

namespace karin.ui
{
    [CreateAssetMenu(menuName = "SO/karin/StatUpEventSO")]
    public class StatUpEventSO : EventSO
    {
        public StatUpData<StatChangeData>[] actions = new StatUpData<StatChangeData>[3];
        public override int branchCount => actions.Length;

        public override void Play(int index)
        {
            var targetAction = actions[index];
            var target = ShyDataManager.Instance.minions[actions[index].index];
            if (target == null) return;
            foreach (var change in targetAction.branchActions)
            {
                target.stats[change.targetStat] += change.statIncresement;
            }
        }
    }
}