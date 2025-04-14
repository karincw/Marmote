using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.UI;

namespace Shy
{
    [RequireComponent(typeof(HealthCompo))]
    public class Character : MonoBehaviour, IPointerClickHandler
    {
        private HealthCompo health;
        [SerializeField] private PlayableAsset timeLineAsset;
        [SerializeField] private CharacterSO data;

        [Header("Stat")]
        public int str;
        public int def;

        [Header("Other")]
        public int bonusAtk = 0;
        public int bonusDef = 0;

        public List<Buff> buffs;

        private Team team = Team.None;
        public Transform buffGroup;
        public UnityAction skillActions;

        private Image visual;

        #region 초기화
        public virtual void Awake()
        {
            health = GetComponent<HealthCompo>();
            visual = transform.Find("Visual").GetComponent<Image>();
        }

        public void Init(Team _team, CharacterSO _data)
        {
            data = _data;
            team = _team;

            //Stat
            str = data.stats.str;
            def = data.stats.def;
            health.Init(_data.stats.hp);

            buffs = new List<Buff>();

            //Visual
            VisualUpdate(0);
            transform.Find("Info").Find("Name").GetComponent<TextMeshProUGUI>().text = data.characterName;
        }
        #endregion

        public void BuffCheck()
        {
            foreach (Buff item in buffs)
            {
                item.CountDown();
            }
        }

        #region Skill
        public void OnSkillEvent(int _value, EventType _type)
        {
            Debug.Log(gameObject.name + " Event : " + _type.ToString() + " / " + _value);

            if(_type == EventType.AttackEvent) health.OnDamageEvent(_value - def);
            else if (_type == EventType.HealEvent) health.OnHealEvent(_value);
            else if (_type == EventType.ShieldEvent) health.OnShieldEvent(_value);

            Debug.Log("Event 끝");
        }

        public void VisualUpdate(int _value)
        {
            switch (_value)
            {
                case 1:
                    visual.sprite = data.skillAnime;
                    break;
                case 2:
                    visual.sprite = data.skillAnime;
                    break;
                case 3:
                    visual.sprite = data.skillAnime;
                    break;
                default:
                    visual.sprite = data.sprite;
                    break;
            }
        }
        #endregion

        #region 공격 시점
        public void SkillSet(int _v, ActionWay _way, Character[] players, Character[] enemies)
        {
            if(team == Team.Enemy)
            {
                Character[] p = players;
                players = enemies;
                enemies = p;
            }


            SkillSO so = data.skills[_v - 1];
            skillActions = null;

            for (int i = 0; i < so.skills.Count; i++)
            {
                Character[] targets = (so.skills[i].targetTeam == Team.Player) ? players : enemies;

                ActionWay way = so.skills[i].actionWay;
                if (way == ActionWay.None) way = _way;

                int a = i;

                switch (way)
                {
                    case ActionWay.Self:
                        skillActions += () => so.skills[a].UseSkill(this, this);
                        break;
                    case ActionWay.Opposite:
                        break;
                    case ActionWay.Select:
                        break;
                    case ActionWay.Random:
                        Character tR = targets[Random.Range(0, targets.Length)];
                        skillActions += () => so.skills[a].UseSkill(this, tR);
                        break;
                    case ActionWay.All:
                        for (int j = 0; j < targets.Length; j++)
                        {
                            Character tA = targets[j];
                            
                            skillActions += () => so.skills[a].UseSkill(this, tA);
                        }
                        break;
                }
            }

            //skillActions += BattleManager.Instance.NextAction;

            AttackAnime();
        }

        public void AttackAnime()
        {
            skillActions?.Invoke();
        }
        #endregion

        public int GetStat(StatEnum _stat)
        {
            if (_stat == StatEnum.Str) return str;
            if (_stat == StatEnum.Def) return def;
            if (_stat == StatEnum.MaxHp) return health.GetMaxHealth();
            if (_stat == StatEnum.Hp) return health.GetHealth();

            Debug.LogError("Not Found"); return 0;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Click Character");
            //SelectManager.Instance.SelectCharacter(this);

            AnimeManager.Instance.PlayAnime(timeLineAsset, gameObject);
        }

        public void SingalTest()
        {
            VisualUpdate(1);
        }
    }
}
