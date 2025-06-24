using DG.Tweening;
using Shy.Dice;
using Shy.Info;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shy.Unit
{
    [RequireComponent(typeof(HealthCompo))]
    public abstract class Character : MonoBehaviour
    {
        #region º¯¼ö
        private HealthCompo healthCompo;
        protected PressCompo pressCompo;
        private StatCompo statCompo;
        private CharacterSO data;

        public Team team = Team.None;
        
        internal List<BuffUI> buffs;
        public Transform buffGroup;

        private Image visual;
        private Transform parentTrm, uiTrm;

        public Color posColor;
        #endregion

        #region Get
        public Sprite GetIcon() => data.cardImage;
        public Transform GetVisual() => visual.transform;
        public bool IsDie() => healthCompo.isDie;
        public SkillSOBase GetSkill(int _idx) => data.skills[_idx];
        
        public int GetStackCnt(BuffType _type)
        {
            int _total = 0;
            foreach (var _buff in buffs) _total += _buff.GetBuffCount(_type);
            return _total;
        }
        #endregion

        #region Init
        public virtual void Awake()
        {
            healthCompo = GetComponent<HealthCompo>();
            pressCompo = GetComponentInChildren<PressCompo>();
            visual = transform.Find("Visual").GetComponent<Image>();
            uiTrm = transform.Find("Ui");
            parentTrm = transform.parent;
        }

        public virtual bool Init(CharacterSO _data)
        {
            if (_data == null)
            {
                gameObject.SetActive(false);
                return false;
            }

            data = _data;

            UnityAction hitEvent = null;
            hitEvent += () => VisualUpdate(4);
            hitEvent += () => StartCoroutine(HitAnime());
            hitEvent += () => HitBuffEvent();

            statCompo = new StatCompo(data.stats);
            healthCompo.Init(_data.stats.maxHp, _data.stats.hp, hitEvent);
            buffs = new List<BuffUI>();

            VisualUpdate(0);

            return true;
        }

        public void ReturnParent() => transform.SetParent(parentTrm);
        #endregion

        #region Visual
        public void HealthVisibleEvent(bool _show)
        {
            uiTrm.gameObject.SetActive(_show);
            healthCompo.UpdateHealth();
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
                Debug.Log(gameObject.name + " Die");
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
                    visual.sprite = data.skills[_value - 1].GetMotionSprite(Anime.AnimeType.UserVisual, this);
                    break;
                case 4:
                    visual.sprite = data.hitAnime;
                    break;
                default:
                    visual.sprite = data.sprite;
                    break;
            }
        }

        public void VisualUpdate(Sprite _sprite)
        {
            if (_sprite == null) return;
            visual.sprite = _sprite;
        }
        #endregion

        #region Skill
        public void SkillFin()
        {
            VisualUpdate(0);
            healthCompo.cnt = 0;
            //ReturnParent();
        }

        public void OnValueEvent(int _value, EventType _type, int _ignoreDefPer)
        {
            switch (_type)
            {
                case EventType.AttackEvent:
                    StartCoroutine(healthCompo.OnDamageEvent(BattleManager.Instance.SetDamage(_value, this, _ignoreDefPer)));
                    break;
                case EventType.ShieldEvent:
                    healthCompo.OnShieldEvent(_value);
                    break;
                case EventType.HealEvent:
                    healthCompo.OnHealEvent(_value);
                    break;
            }
        }

        public void OnBuffEvent(int _value, BuffType _buffType)
        {
            foreach (var _buff in buffs)
            {
                if (_buff.CheckBuff(_buffType))
                {
                    _buff.Add(_value);
                    return;
                }
            }

            BuffUI buff = Pooling.Instance.Use(PoolingType.Buff, buffGroup).GetComponent<BuffUI>();
            buff.Init(this, BuffManager.Instance.GetBuff(_buffType), _value);
            buff.gameObject.SetActive(true);

            buffs.Add(buff);
        }

        public void OnBuffEvent(BuffEvent _buffEvent) => OnBuffEvent(_buffEvent.value, _buffEvent.buffType);

        private void HitBuffEvent()
        {
            foreach (var _buff in buffs) BuffManager.Instance.OnBuffEvent(BuffUseCondition.OnHit, _buff, this);
        }

        public void BuffCheck()
        {
            List<BuffUI> _removeBuffs = new List<BuffUI>();

            foreach (BuffUI _buff in buffs)
            {
                var _result = _buff.CountDown();
                if (_result != null) _removeBuffs.Add(_result);
            }

            //HealthVisibleEvent(true);

            foreach (var _buff in _removeBuffs)
            {
                buffs.Remove(_buff);
            }
        }
        #endregion

        #region Stat
        public void SetBonusStat(StatEnum _stat, int _value)
        {
            statCompo.UpdateBonusStat(_stat, _value);
            if (_stat == StatEnum.Hp) healthCompo.SetMaxHp(GetNowStat(StatEnum.Hp));
        }

        public int GetBaseStat(StatEnum _stat)
        {
            if (_stat == StatEnum.Hp) _stat = StatEnum.MaxHp;
            return statCompo.GetBaseStat(_stat);
        }
        public int GetBonusStat(StatEnum _stat)
        {
            if (_stat == StatEnum.Hp) _stat = StatEnum.MaxHp;
            return statCompo.GetBonusStat(_stat);
        }
        public int GetNowStat(StatEnum _stat)
        {
            if (_stat == StatEnum.Hp) return healthCompo.GetHealth();
            return statCompo.GetApplyStat(_stat);
        }

        internal void BattleFinish()
        {
            data.stats.hp = GetNowStat(StatEnum.Hp);
        }
        #endregion
    }
}