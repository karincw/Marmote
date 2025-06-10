using Shy.Unit;
using System.Collections.Generic;
using UnityEngine;

namespace Shy.Target
{
    public class TargetManager : MonoBehaviour
    {
        public static TargetManager Instance;

        [SerializeField]
        private List<ActionTarget> list;
        private Dictionary<ActionWay, Sprite> dic;

        private void Awake()
        {
            if(Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;

            DontDestroyOnLoad(gameObject);

            foreach (var _action in list)
            {
                dic.Add(_action.way, _action.icon);
            }
        }

        public Sprite GetIcon(ActionWay _way) => dic[_way];

        public List<Character> GetTargets(TargetData _td)
        {
            List<Character> targets = new List<Character>();

            Team targetTeam = _td.user.team;

            if (_td.targetTeam == TargetWay.Opponenet)
            {
                if (targetTeam == Team.Player) targetTeam = Team.Enemy;
                else targetTeam = Team.Player;
            }

            switch (_td.actionWay)
            {
                case ActionWay.Self:
                    targets.Add(_td.user);
                    break;

                case ActionWay.Opposite:
                    break;

                case ActionWay.Random:
                    var c = BattleManager.Instance.GetCharacters(targetTeam);
                    targets.Add(c[Random.Range(0, c.Count)]);
                    break;

                case ActionWay.All:
                    targets = BattleManager.Instance.GetCharacters(targetTeam);
                    break;
            }

            return targets;
        }
    }
}
