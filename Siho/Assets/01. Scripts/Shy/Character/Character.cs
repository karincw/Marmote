using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shy
{
    [RequireComponent(typeof(HealthCompo))]
    public class Character : MonoBehaviour, IPointerClickHandler
    {
        #region 변수
        private HealthCompo health;
        private AnimeCompo anime;
        [SerializeField] private CharacterSO data;

        [Header("Stat")]
        public int str;
        public int def;

        [Header("Other")]
        public int bonusAtk = 0;
        public int bonusDef = 0;

        public List<Buff> buffs;

        internal Team team = Team.None;
        public UnityAction skillActions;
        public UnityAction visualAction;

        public Transform buffGroup;
        private Image visual;
        #endregion

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

            UnityAction act = null;
            act += () => VisualUpdate(4);
            act += () => StartCoroutine(HitAnime());
            health.Init(_data.stats.hp, act);

            buffs = new List<Buff>();

            //Visual
            VisualUpdate(0);
            transform.Find("Info").Find("Name").GetComponent<TextMeshProUGUI>().text = data.characterName;
        }
        #endregion

        private IEnumerator HitAnime()
        {
            yield return new WaitForSeconds(0.6f);
            VisualUpdate(0);
        }

        #region Skill
        public void OnSkillEvent(int _value, EventType _type)
        {
            if(_type == EventType.AttackEvent) health.OnDamageEvent(_value - def);
            else if (_type == EventType.HealEvent) health.OnHealEvent(_value);
            else if (_type == EventType.ShieldEvent) health.OnShieldEvent(_value);
        }

        private void VisualUpdate(int _value)
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
                case 4:
                    visual.sprite = data.hitAnime;
                    break;
                default:
                    visual.sprite = data.sprite;
                    break;
            }
        }

        public Transform GetVisual() => visual.transform;
        #endregion

        #region 공격
        public void SkillSet(int _v, ActionWay _way, Character[] players, Character[] enemies)
        {
            SkillSO so = data.skills[_v - 1];
            skillActions = visualAction = null;

            //적이면 타겟 반전
            if(team == Team.Enemy)
            {
                Character[] p = players;
                players = enemies;
                enemies = p;
            }

            visualAction += () => VisualUpdate(_v);

            if (!Bool.IsPetMotion(so.motion))
                skillActions += visualAction;

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

            SkillManager.Instance.UseSkill(this, so);
        }

        public void SkillFin()
        {
            VisualUpdate(0);

            BattleManager.Instance.NextAction();
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

        public void BuffCheck()
        {
            foreach (Buff item in buffs)
            {
                item.CountDown();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("Click Character");
            SelectManager.Instance.SelectCharacter(this);
        }
    }
}
