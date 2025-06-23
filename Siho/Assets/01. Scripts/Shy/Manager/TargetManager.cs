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
                //dic.Add(_action.way, _action.icon);
            }
        }

        public Sprite GetIcon(ActionWay _way) => dic[_way];

        public List<Character> GetTargets(TargetData _td)
        {
            var _targets = new List<Character>();
            Character _currentTarget = null;

            Team _targetTeam = _td.user.team;

            if (_td.targetTeam == TargetWay.Opponenet)
            {
                if (_targetTeam == Team.Player) _targetTeam = Team.Enemy;
                else _targetTeam = Team.Player;
            }

            switch (_td.actionWay)
            {
                case ActionWay.Self:
                    _targets.Add(_td.user);
                    break;

                case ActionWay.Opposite:
                    break;

                case ActionWay.LessHp:
                    var _lessTargets = BattleManager.Instance.GetCharacters(_targetTeam);
                    _currentTarget = _lessTargets[0];

                    for (int i = 1; i < _lessTargets.Count; i++)
                    {
                        if (_lessTargets[i].GetNowStat(StatEnum.Hp) > _currentTarget.GetNowStat(StatEnum.Hp))
                        {
                            _currentTarget = _lessTargets[i];
                        }
                    }
                    _targets.Add(_currentTarget);
                    break;

                case ActionWay.MoreHp:
                    var _moreTargets = BattleManager.Instance.GetCharacters(_targetTeam);
                    _currentTarget = _moreTargets[0];

                    for (int i = 1; i < _moreTargets.Count; i++)
                    {
                        if (_moreTargets[i].GetNowStat(StatEnum.Hp) > _currentTarget.GetNowStat(StatEnum.Hp))
                        {
                            _currentTarget = _moreTargets[i];
                        }
                    }
                    _targets.Add(_currentTarget);
                    break;

                case ActionWay.Random:
                    var c = BattleManager.Instance.GetCharacters(_targetTeam);
                    _targets.Add(c[Random.Range(0, c.Count)]);
                    break;

                case ActionWay.All:
                    _targets = BattleManager.Instance.GetCharacters(_targetTeam);
                    break;
            }

            return _targets;
        }
    }
}
