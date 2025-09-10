using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shy.Pooling;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;
using ParticleSystem = Shy.Pooling.ParticleSystem;

namespace Shy
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI characterName;
        public Team team;
        [SerializeField] private MainStat mainStats;
        [SerializeField] private SubStat totalSubStat;
        private SubStat addSubStat, multiSubStat;
        private Characteristic characteristic;

        private Transform dmgTxtPos;

        private HealthGuageCompo healthCompo;
        private Image visual, hitVisual;
        private Sprite idle, attack;

        [SerializeField] private Transform synergyParent, particleParent;
        private Dictionary<SynergyType, Synergy> synergies;

        public event UnityAction<Attack> OnAttack, OnHit;
        public event UnityAction<float> OnHeal;

        private void Awake()
        {
            healthCompo = GetComponent<HealthGuageCompo>();
            visual = transform.Find("Visual").GetComponent<Image>();
            dmgTxtPos = transform.Find("DmgTxtPos");
            hitVisual = visual.transform.GetChild(0).GetComponent<Image>();

            InitAction();          
        }

        #region Actions
        private void InitAction()
        {
            OnHeal = (float _value) =>
            {
                totalSubStat.hp += _value;

                var _item = PoolingManager.Instance.Pop(PoolType.DmgText) as DamageText;
                _item.transform.SetParent(dmgTxtPos);
                _item.Use(_value, new(0.4f, 1, 0));

                healthCompo.HealthUpdate(totalSubStat.hp, totalSubStat.maxHp, true);
            };

            OnHit = (Attack _result) =>
            {
                totalSubStat.hp -= _result.dmg;

                var _item = PoolingManager.Instance.Pop(PoolType.DmgParticle) as ParticleSystem;
                _item.transform.SetParent(particleParent);
                _item.Init();
                _item.Play();

                Sequence seq = DOTween.Sequence();
                seq.Append(hitVisual.DOFade(0.8f, 0.2f));
                seq.Join(visual.transform.DOScaleY(0.65f, 0.2f));
                seq.Append(hitVisual.DOFade(0f, 0.5f));
                seq.Join(visual.transform.DOScaleY(1, 0.5f));

                healthCompo.HealthUpdate(totalSubStat.hp, totalSubStat.maxHp, true);
            };
            OnHit += (Attack _result) => DieCheck();

            OnAttack = (Attack _result) =>
            {
                if (_result.target.characteristic.isNotBlood == false) HealEvent(_result.dmg * totalSubStat.drain * 0.01f);
            };
        }
        #endregion

        public void Init(CharacterDataSO _so)
        {
            characterName.SetText(_so.itemName);

            characteristic = new();

            mainStats = _so.mainStat;
            addSubStat = StatSystem.ChangeSubStat(mainStats);
            multiSubStat.Reset(1);
            CheckSubstat();

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

            SequnceTool.Instance.Delay(() =>
            {
                foreach (var _synergySO in _so.synergies) AddSynergy(_synergySO);
            }, 0.8f);
        }

        #region Synergy
        public void AddSynergy(SynergyType _type)
        {
            if (synergies.ContainsKey(_type))
            {
                synergies[_type].Add();
            }
            else
            {
                AddSynergy(new KeyValuePair<SynergyType, int>(_type, 1));
            }
        }

        private void AddSynergy(KeyValuePair<SynergyType, int> _data)
        {
            var _synergy = PoolingManager.Instance.Pop(PoolType.Synergy) as Synergy;
            _synergy.transform.SetParent(synergyParent);
            _synergy.Init(_data, team);

            synergies.Add(_data.Key, _synergy);
            _synergy.Show();
        }
        #endregion

        #region Stat & Characteristic
        public float GetNowStat(SubStatEnum _stat) => StatSystem.GetSubStatRef(ref totalSubStat, _stat);
        public int GetNowStat(MainStatEnum _stat) => StatSystem.GetMainStatRef(ref mainStats, _stat);
        private void CheckSubstat() => totalSubStat = addSubStat * multiSubStat;

        public void ChangeStat(float _value, Calculate _calc, SubStatEnum _statEnum)
        {
            if (_calc == Calculate.Plus || _calc == Calculate.Minus)
                StatSystem.ChangeStat(ref addSubStat, _value, _calc, _statEnum);
            else
                StatSystem.ChangeStat(ref multiSubStat, _value, _calc, _statEnum);

            CheckSubstat();
            healthCompo.HealthUpdate(totalSubStat.hp, totalSubStat.maxHp);
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
        
        public void Regeneration() => HealEvent(totalSubStat.maxHp * (1 - totalSubStat.regen * 0.01f));
        public void SubscribeCounter() => OnHit += (Attack _attack) => 
        {
            if (Random.Range(0, 100f) < totalSubStat.counter)
            {
                Debug.Log("counter " + gameObject.name);
                BattleManager.Instance.AttackTimeReset(team);
            }
        }; 
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
            bool _isDie = totalSubStat.hp <= 0;

            if (_isDie)
            {
                DieEvent();
            }
        }
        #endregion
    }
}