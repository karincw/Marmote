using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shy
{
    [RequireComponent(typeof(HealthCompo), typeof(StatCompo))]
    public class Character : MonoBehaviour, IPointerClickHandler
    {
        #region 변수
        private HealthCompo health;
        private StatCompo stat;
        [SerializeField] private CharacterSO data;

        internal List<Buff> buffs;
        internal Team team = Team.None;
        internal UnityAction skillActions, visualAction;

        public Transform buffGroup;
        private Image visual;
        #endregion

        #region Get
        public Sprite GetIcon() => data.sprite;
        public Transform GetVisual() => visual.transform;
        public int GetStat(StatEnum _stat)
        {
            if (_stat == StatEnum.Str) return stat.GetStr();
            if (_stat == StatEnum.Def) return stat.GetDef();
            if (_stat == StatEnum.MaxHp) return health.GetMaxHealth();
            if (_stat == StatEnum.Hp) return health.GetHealth();
            Debug.LogError("Not Found"); return 0;
        }
        public int GetNowStr() => stat.bonusAtk;
        public int GetNowDef() => stat.bonusDef;
        public bool IsDie() => health.isDie;
        #endregion

        #region Init
        public virtual void Awake()
        {
            health = GetComponent<HealthCompo>();
            stat = GetComponent<StatCompo>();
            visual = transform.Find("Visual").GetComponent<Image>();
        }

        public void Init(Team _team, CharacterSO _data)
        {
            data = _data;
            team = _team;

            UnityAction hitEvent = null;
            hitEvent += () => VisualUpdate(4);
            hitEvent += () => StartCoroutine(HitAnime());

            stat.Init(_data.stats);
            health.Init(_data.stats.hp, hitEvent);

            buffs = new List<Buff>();

            //Visual
            VisualUpdate(0);
            transform.Find("Info").Find("Name").GetComponent<TextMeshProUGUI>().text = data.characterName;
        }
        #endregion

        #region Visual
        private IEnumerator HitAnime()
        {
            yield return new WaitForSeconds(0.6f);

            if(!IsDie()) VisualUpdate(0);
            else DeadAnime();
        }

        protected virtual void DeadAnime()
        {
            Sequence seq = DOTween.Sequence();

            seq.Append(visual.DOColor(new Color(0.25f, 0.25f, 0.25f), 0.3f));
            seq.OnComplete(() =>
            {
                VisualUpdate(0);
            });
        }

        private void VisualUpdate(int _value)
        {
            switch (_value)
            {
                case 1:
                    visual.sprite = data.attackAnime;
                    break;
                case 2:
                    visual.sprite = data.skillAnime;
                    break;
                case 3:
                    visual.sprite = data.skill2Anime;
                    break;
                case 4:
                    visual.sprite = data.hitAnime;
                    break;
                default:
                    visual.sprite = data.sprite;
                    break;
            }
        }
        #endregion

        #region Skill
        public void OnValueEvent(int _value, EventType _type)
        {
            if(_type == EventType.AttackEvent) health.OnDamageEvent(_value - stat.GetDef());
            else if (_type == EventType.HealEvent) health.OnHealEvent(_value);
            else if (_type == EventType.ShieldEvent) health.OnShieldEvent(_value);
        }

        public void SkillUse(int _v, ActionWay _way, Character[] players, Character[] enemies)
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
            if (!Bool.IsPetMotion(so.motion)) skillActions += visualAction;

            for (int i = 0; i < so.skills.Count; i++)
            {
                Character[] targets = (so.skills[i].targetTeam == Team.Player) ? players : enemies;

                ActionWay way = so.skills[i].actionWay;
                if (way == ActionWay.None) way = _way;

                int a = i;
                Character t;
                switch (way)
                {
                    case ActionWay.Self:
                        skillActions += () => so.skills[a].UseSkill(this, this);
                        break;
                    case ActionWay.Random:
                        t = targets[Random.Range(0, targets.Length)];
                        skillActions += () => so.skills[a].UseSkill(this, t);
                        break;
                    case ActionWay.All:
                        for (int j = 0; j < targets.Length; j++)
                        {
                            t = targets[j];
                            skillActions += () => so.skills[a].UseSkill(this, t);
                        }
                        break;
                    #region Hp
                    case ActionWay.LessHp:
                        t = targets[0];
                        for (int j = 1; j < targets.Length; j++)
                            if (targets[j].GetStat(StatEnum.Hp) < t.GetStat(StatEnum.Hp)) t = targets[j];
                        skillActions += () => so.skills[a].UseSkill(this, t);
                        break;
                    case ActionWay.MoreHp:
                        t = targets[0];
                        for (int j = 1; j < targets.Length; j++)
                            if (targets[j].GetStat(StatEnum.Hp) > t.GetStat(StatEnum.Hp)) t = targets[j];
                        skillActions += () => so.skills[a].UseSkill(this, t);
                        break;
                    #endregion
                    case ActionWay.Fast:
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

        public void BuffCheck()
        {
            foreach (Buff buff in buffs) buff.CountDown();
        }

        public void BonusStat(StatEnum _stat, int _value)
        {
            if (_stat == StatEnum.Str) stat.bonusAtk += _value;
            if (_stat == StatEnum.Def) stat.bonusDef += _value;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            SelectManager.Instance.SelectCharacter(this);
        }
    }
}