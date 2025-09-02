using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shy.Pooling;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;

namespace Shy
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI characterName;
        public Team team;
        [SerializeField] private MainStat mainStats;
        [SerializeField] private SubStat subStats;
        private Characteristic characteristic;

        private Transform dmgTxtPos;

        private HealthGuageCompo healthCompo;
        private Image visual, hitVisual;
        private Sprite idle, attack;

        [SerializeField] private Transform synergyParent;
        private Dictionary<SynergyType, Synergy> synergies;

        public event UnityAction<Attack> OnAttack, OnHit;
        public event UnityAction<float> OnHeal;

        private void Awake()
        {
            healthCompo = GetComponent<HealthGuageCompo>();
            visual = transform.Find("Visual").GetComponent<Image>();
            dmgTxtPos = transform.Find("DmgTxtPos");
            hitVisual = visual.transform.GetChild(0).GetComponent<Image>();

            OnHeal = (float _value) =>
            {
                subStats.hp += _value;

                var _item = PoolingManager.Instance.Pop(PoolType.DmgText) as DamageText;
                _item.transform.SetParent(dmgTxtPos);
                _item.Use(_value, new(0.4f, 1, 0));

                healthCompo.HealthUpdate(subStats.hp, subStats.maxHp, true);
            };

            OnHit = (Attack _result) =>
            {
                subStats.hp -= _result.dmg;

                Sequence seq = DOTween.Sequence();
                seq.Append(hitVisual.DOFade(0.8f, 0.2f));
                seq.Join(visual.transform.DOScaleY(0.65f, 0.2f));
                seq.Append(hitVisual.DOFade(0f, 0.5f));
                seq.Join(visual.transform.DOScaleY(1, 0.5f));

                healthCompo.HealthUpdate(subStats.hp, subStats.maxHp, true);
            };
            OnHit += (Attack _result) => DieCheck();

            OnAttack = (Attack _result) =>
            {
                if (_result.target.characteristic.isNotBlood) HealEvent(_result.dmg * subStats.drain * 0.01f);
            };
        }

        public void Init(CharacterDataSO _so)
        {
            characterName.SetText(_so.itemName);

            characteristic = new();

            mainStats = _so.mainStat;
            subStats = StatSystem.ChangeSubStat(mainStats);

            idle = _so.visual;
            attack = _so.attackAnime;
            VisualUpdate(true);

            visual.color = Color.white;

            healthCompo.HealthUpdate(1, 1);

            if(synergies != null)
            {
                foreach (var item in synergies.Values)
                    PoolingManager.Instance.Push(PoolType.Synergy, item);
            }

            synergies = new();

            foreach (var _synergySO in _so.synergies) GetSynergy(_synergySO);
        }

        #region Synergy
        public void UseSynergy()
        {
            foreach (var _synergy in synergies.Values)
            {
                _synergy.UseSynergy();
            }

            healthCompo.HealthUpdate(subStats.hp, subStats.maxHp);
        }

        public void AddSynergy(SynergyType _type)
        {
             
        }

        private void GetSynergy(KeyValuePair<SynergyType, int> _data)
        {
            var _synergy = PoolingManager.Instance.Pop(PoolType.Synergy) as Synergy;
            _synergy.transform.SetParent(synergyParent);
            _synergy.Init(_data, team);

            synergies.Add(_data.Key, _synergy);
            _synergy.gameObject.SetActive(true);
        }
        #endregion

        #region Stat & Characteristic
        private float CalcValue(float _oldValue, float _newValue, Calculate _calc)
        {
            switch (_calc)
            {
                case Calculate.Plus:
                    return _oldValue + _newValue;
                case Calculate.Minus:
                    return _oldValue - _newValue;
                case Calculate.Multiply:
                    return _oldValue * _newValue;
                case Calculate.Divide:
                    return _oldValue / _newValue;
                case Calculate.Change:
                    return _newValue;
                case Calculate.Percent:
                    return _oldValue * (1 - _newValue * 0.01f);
            }
            return 0;
        }

        public float GetNowStat(SubStatEnum _stat) => StatSystem.GetSubStatRef(ref subStats, _stat);
        public int GetNowStat(MainStatEnum _stat) => StatSystem.GetMainStatRef(ref mainStats, _stat);

        public void AddStat(float _value, Calculate _calc, SubStatEnum _statEnum)
        {
            if(_statEnum == SubStatEnum.Hp)
            {
                subStats.hp = CalcValue(subStats.hp, _value, _calc);
                return;
            }

            if(_statEnum == SubStatEnum.MaxHp)
            {
                subStats.maxHp = CalcValue(subStats.maxHp, _value, _calc);
                return;
            }

            ref float _stat = ref StatSystem.GetSubStatRef(ref subStats, _statEnum);
            _stat = CalcValue(_stat, _value, _calc);
        }

        public void ChangeCharacteristic(CharacteristicEnum _enum, bool _value)
        {
            switch (_enum)
            {
                case CharacteristicEnum.NotBlood: characteristic.notBlood = _value;
                    break;

                case CharacteristicEnum.IsNotBlood: characteristic.isNotBlood = _value;
                    break;
            }
        }
        #endregion

        #region Character Action
        public void VisualUpdate(bool _idle) => visual.sprite = (_idle) ? idle : attack;
        
        public void Regeneration() => HealEvent(subStats.regen);
        public void SubscribeCounter() => OnHit += (Attack _attack) => { if (Random.Range(0, 100f) <= subStats.counter) BattleManager.Instance.AttackTimeReset(team); }; 
        public void AttackEvent(Attack _result)
        {
            if (_result.dmg > 0) OnAttack?.Invoke(_result);
        }

        public void HitEvent(Attack _result)
        {
            if (_result.dmg > 0) OnHit?.Invoke(_result);

            var _item = PoolingManager.Instance.Pop(PoolType.DmgText) as DamageText;
            _item.transform.SetParent(dmgTxtPos);
            _item.Use(_result);
        }

        public void HealEvent(float _value)
        {
            if (_value <= 0) return;

            OnHeal?.Invoke(_value);
        }

        private void DieEvent()
        {
            Debug.Log(gameObject.name + " »ç¸Á");
            visual.DOColor(Color.black, 0.45f);
            visual.DOFade(0, 0.7f);

            BattleManager.Instance.EndBattle(team);
        }

        public void DieCheck()
        {
            bool _isDie = subStats.hp <= 0;

            if (_isDie)
            {
                DieEvent();
            }
        }
        #endregion
    }
}