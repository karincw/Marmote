using DG.Tweening;
using Shy.Info;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shy.Unit
{
    [RequireComponent(typeof(HealthCompo), typeof(StatCompo))]
    public class Character : MonoBehaviour, IPress, IBeginDragHandler, IDragHandler
    {
        #region 변수
        private HealthCompo health;
        private StatCompo stat;
        private CharacterSO data;

        internal Team team = Team.None;
        internal List<Buff> buffs;
        internal List<Character> targets;
        internal UnityAction skillActions, visualAction;

        public Transform buffGroup;
        private Image visual;
        private Transform parentTrm, uiTrm;

        private bool pressing = false, openInfo;
        private float pressStartTime;
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
        public int GetStackCnt(BuffType _type)
        {
            for (int i = 0; i < buffs.Count; i++)
            {
                int n = buffs[i].CheckBuff(_type);
                if(n != 0) return n;
            }
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
            uiTrm = transform.Find("Ui");
            parentTrm = transform.parent;
        }

        private void ResetParent() => transform.parent = parentTrm;

        public void Init(Team _team, CharacterSO _data)
        {
            if (_data == null)
            {
                gameObject.SetActive(false);
                return;
            }

            data = _data;
            team = _team;

            UnityAction hitEvent = null;
            hitEvent += () => VisualUpdate(4);
            hitEvent += () => StartCoroutine(HitAnime());

            stat.Init(_data.stats);
            health.Init(_data.stats.maxHp, hitEvent);

            buffs = new List<Buff>();

            //Visual
            VisualUpdate(0);
            //Transform namePos = transform.Find("Ui").Find("Name");
            //namePos.GetComponent<TextMeshProUGUI>().text = data.characterName;
            //namePos.GetChild(0).GetComponent<TextMeshProUGUI>().text = data.characterName;
        }
        #endregion

        #region Visual
        public void HideUi() => uiTrm.gameObject.SetActive(false);
        public void ShowUi()
        {
            uiTrm.gameObject.SetActive(true);
            health.UpdateHealth();
        }

        private IEnumerator HitAnime()
        {
            yield return new WaitForSeconds(0.6f);

            if(!IsDie()) VisualUpdate(0);
            else DeadAnime();
        }

        protected virtual void DeadAnime()
        {
            Sequence seq = DOTween.Sequence();

            seq.OnStart(() =>
            {
                BattleManager.Instance.CharacterDie(this);
            });
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
                case 1: case 2: case 3:
                    visual.sprite = data.skills[_value - 1].GetSkillMotion();
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

        private void AddTarget(Character _ch, SkillEventSO _skill)
        {
            targets.Add(_ch);
            skillActions += () => _skill.UseSkill(this, _ch);
        }

        public void SkillUse(int _v, ActionWay _way, Character[] players, Character[] enemies)
        {
            SkillSOBase so = data.skills[_v - 1];
            targets = new();
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

            foreach (var item in so.GetSkills())
            {
                Character[] targets = (item.targetTeam == Team.Player) ? players : enemies;

                ActionWay way = item.actionWay;
                if (way == ActionWay.None) way = _way;

                Character t;
                switch (way)
                {
                    case ActionWay.Self:
                        AddTarget(this, item);
                        break;

                    case ActionWay.Random:
                        t = targets[Random.Range(0, targets.Length)];
                        AddTarget(t, item);
                        break;

                    case ActionWay.All:
                        for (int j = 0; j < targets.Length; j++)
                        {
                            t = targets[j];
                            AddTarget(t, item);
                        }
                        break;

                    #region Hp
                    case ActionWay.LessHp:
                        t = targets[0];
                        for (int j = 1; j < targets.Length; j++)
                            if (targets[j].GetStat(StatEnum.Hp) < t.GetStat(StatEnum.Hp)) t = targets[j];

                        AddTarget(t, item);
                        break;

                    case ActionWay.MoreHp:
                        t = targets[0];
                        for (int j = 1; j < targets.Length; j++)
                            if (targets[j].GetStat(StatEnum.Hp) > t.GetStat(StatEnum.Hp)) t = targets[j];

                        AddTarget(t, item);
                        break;
                    #endregion

                    case ActionWay.Fast:
                        break;
                }
            }

            SkillMotionManager.Instance.UseSkill(this, so);
        }

        public void SkillFin()
        {
            VisualUpdate(0);
            ResetParent();
            for (int i = 0; i < targets.Count; i++) targets[i].ResetParent();

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

        #region Press
        public void OnPointerDown(PointerEventData eventData)
        {
            if (IsDie()) return;

            pressing = true;
            pressStartTime = Time.time;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (IsDie()) return;

            ExitPress();
        }

        public void ExitPress()
        {
            if (pressing)
            {
                InfoManager.Instance.CloseInfoPanel(this);
                pressing = false;
                openInfo = false;
            }
        }

        public void LongPress()
        {
            Debug.Log("Long Press");
            openInfo = true;
            InfoManager.Instance.OpenInfoPanel(transform, this, data);
        }

        private void Update()
        {
            if(pressing && !openInfo)
            {
                if(Time.time - pressStartTime >= 1) LongPress();
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (IsDie()) return;
            Select.SelectManager.Instance.DragBegin(this);
            ExitPress();
        }

        public void OnDrag(PointerEventData eventData) { }
        #endregion
    }
}