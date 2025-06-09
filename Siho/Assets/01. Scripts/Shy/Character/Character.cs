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
        internal UnityAction skillActions, visualAction;

        internal List<BuffUI> buffs;
        public Transform buffGroup;

        private Image visual;
        private Transform parentTrm, uiTrm;

        private bool pressing = false, openInfo;
        private float pressStartTime;

        private List<SkillData> skillDatas = new List<SkillData>(3);
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
        public SkillSOBase GetSkill(int _idx) => data.skills[_idx];

        private int GetDamage(int _baseDamage) => _baseDamage - (int)(GetNowDef() * 0.5f);
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

        private void ResetParent() => transform.SetParent(parentTrm);

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

            buffs = new List<BuffUI>();

            //Visual
            VisualUpdate(0);
        }
        #endregion

        #region Visual
        public void HealthVisibleEvent(bool _show)
        {
            uiTrm.gameObject.SetActive(_show);
            if (_show) health.UpdateHealth();
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
                    visual.sprite = data.skills[_value - 1].GetMotionSprite(this);
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
        public void OnValueEvent(int _value, EventType _type, bool _igonoreDef)
        {
            switch (_type)
            {
                case EventType.AttackEvent:
                    StartCoroutine(health.OnDamageEvent(_igonoreDef ? _value : GetDamage(_value)));
                    break;
                case EventType.ShieldEvent:
                    health.OnShieldEvent(_value);
                    break;
                case EventType.HealEvent:
                    health.OnHealEvent(_value);
                    break;
            }
        }

        public void OnBuffEvent(BuffEvent _buffEvent)
        {
            foreach (var _buff in buffs)
            {
                if(_buff.CheckBuff(_buffEvent.buffData))
                {
                    //중첩 코드
                    return;
                }
            }

            BuffUI buff = Pooling.Instance.Use(PoolingType.Buff, buffGroup).GetComponent<BuffUI>();
            buff.Init(this, _buffEvent.buffData, _buffEvent.value);
            buff.gameObject.SetActive(true);

            buffs.Add(buff);
        }

        public void SkillFin()
        {
            VisualUpdate(0);
            health.cnt = 0;
            ResetParent();
        }

        public void BuffCheck()
        {
            foreach (BuffUI buff in buffs) buff.CountDown();
        }
        #endregion

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