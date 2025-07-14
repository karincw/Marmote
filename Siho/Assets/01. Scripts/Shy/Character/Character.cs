using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shy.Pooling;
using DG.Tweening;
using TMPro;

namespace Shy
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI characterName;
        public Team team;
        [SerializeField] private MainStat mainStats;
        [SerializeField] private SubStat subStats;
        internal Characteristic characteristic;

        private Transform dmgTxtPos;

        private HealthGuageCompo healthCompo;
        private Image visual, hitVisual;
        private Sprite idle, attack;

        [SerializeField] private Transform synergyParent;
        private Dictionary<SynergySO, Synergy> synergies;

        private void Awake()
        {
            healthCompo = GetComponent<HealthGuageCompo>();
            visual = transform.Find("Visual").GetComponent<Image>();
            dmgTxtPos = transform.Find("DmgTxtPos");
            hitVisual = visual.transform.GetChild(0).GetComponent<Image>();   
            subStats.hp = subStats.maxHp;
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

            healthCompo.HealthUpdate(subStats.hp, subStats.maxHp);

            if(synergies != null)
            {
                foreach (var item in synergies.Values)
                {
                    PoolingManager.Instance.Push(PoolType.Synergy, item.gameObject);
                }
            }

            synergies = new();

            foreach (var _synergySO in _so.synergies)
            {
                GetSynergy(_synergySO);
            }
        }

        #region Synergy
        public void UseSynergy()
        {
            foreach (var _synergy in synergies.Values)
            {
                _synergy.UseSynergy();
            }
        }

        private void GetSynergy(SynergySO _so)
        {
            Synergy _synergy;

            if(synergies.ContainsKey(_so))
            {
                _synergy = synergies[_so];
            }
            else
            {
                _synergy = PoolingManager.Instance.Pop(PoolType.Synergy).GetComponent<Synergy>();
                _synergy.transform.SetParent(synergyParent);
                _synergy.Init(_so, team);

                synergies.Add(_so, _synergy);
            }

            _synergy.SetValue();
            _synergy.gameObject.SetActive(true);
        }
        #endregion

        #region Editor
        internal void DataSave()
        {
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.EditorUtility.SetDirty(visual);
#endif
        }
        #endregion

        private float CalcValue(float _oldValue, float _newValue, Calculate _calc)
        {
            if (_calc == Calculate.Plus) return _oldValue + _newValue;
            if (_calc == Calculate.Minus) return _oldValue - _newValue;
            if (_calc == Calculate.Multiply) return _oldValue * _newValue;
            if (_calc == Calculate.Divide) return _oldValue / _newValue;
            if (_calc == Calculate.Change) return _newValue;
            return 0;
        }

        public float GetNowStat(SubStatEnum _stat) => StatSystem.GetSubStatRef(ref subStats, _stat);
        public int GetNowStat(MainStatEnum _stat) => StatSystem.GetMainStatRef(ref mainStats, _stat);

        public void AddStat(float _value, Calculate _calc, SubStatEnum _statEnum)
        {
            ref float _stat = ref StatSystem.GetSubStatRef(ref subStats, _statEnum);
            _stat = CalcValue(_stat, _value, _calc);
        }

        public void ChangeCharacteristic(CharacteristicEnum _enum)
        {
            switch (_enum)
            {
                case CharacteristicEnum.NotBlood: characteristic.notBlood = true;
                    break;

                case CharacteristicEnum.IsNotBlood: characteristic.isNotBlood = true;
                    break;
            }
        }

        #region Character Action
        public void VisualUpdate(bool _idle) => visual.sprite = (_idle) ? idle : attack;

        private void HealEvent(float _value)
        {
            if (_value <= 0) return;

            subStats.hp += _value;

            var _item = PoolingManager.Instance.Pop(PoolType.DmgText).GetComponent<DamageText>();
            _item.transform.SetParent(dmgTxtPos);
            _item.Use(_value, new Color(0.4f, 1, 0));

            healthCompo.HealthUpdate(subStats.hp, subStats.maxHp, true);
        }

        public void Drain(float _dmg)
        {
            float _value = _dmg * subStats.drain * 0.01f;
            HealEvent(_value);
        }

        public void Regeneration()
        {
            HealEvent(subStats.regen);
        }

        public bool Counter()
        {
            return Random.Range(0, 100f) <= subStats.counter;
        }

        public void HitEvent(Attack _result)
        {
            if(_result.dmg > 0)
            {
                subStats.hp -= _result.dmg;

                Sequence seq = DOTween.Sequence();
                seq.Append(hitVisual.DOFade(0.8f, 0.2f));
                seq.Append(hitVisual.DOFade(0f, 0.5f));

                var _item = PoolingManager.Instance.Pop(PoolType.DmgText).GetComponent<DamageText>();
                _item.transform.SetParent(dmgTxtPos);

                _item.Use(_result);
            }

            healthCompo.HealthUpdate(subStats.hp, subStats.maxHp, true);
        }

        private void DieEvent()
        {
            Debug.Log(gameObject.name + " »ç¸Á");
        }

        public bool DieCheck()
        {
            bool _isDie = subStats.hp <= 0;
            if (_isDie) DieEvent();
            return _isDie;
        }
        #endregion
    }
}
