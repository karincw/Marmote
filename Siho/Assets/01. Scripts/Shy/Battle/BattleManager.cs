using UnityEngine;
using System.Collections;
using Shy.Event;
using TMPro;
using UnityEngine.Events;

namespace Shy
{
    [RequireComponent(typeof(BattleEventManager))]
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance;

        [Header("Battle Variables")]
        public Character player, enemy;
        private float playerCurrentTime, enemyCurrentTime, regenerationTime;
        [SerializeField] private CanvasGroup battlePanel;

        private BattleEventManager eventManager;
        private bool nowFight = false;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }

            eventManager = GetComponent<BattleEventManager>();
            battlePanel.gameObject.SetActive(false);
        }

        private void Start()
        {
            eventManager.InitEvent(BeginBattle, EndBattle);
        }

        #region Characters
        public Character GetCharacter(Team _target) => (_target == Team.Player) ? player : enemy;
        public Character[] GetCharacters() => new[] { player, enemy };
        public Character[] GetCharacters(Team _target)
        {
            switch (_target)
            {
                case Team.Player:   return new[] { player };
                case Team.Enemy:    return new[] { enemy };
            }

            return new[] { player, enemy };
        }
        #endregion

        #region Battle System
        public void InitBattle(CharacterDataSO _enemy)
        {
            battlePanel.gameObject.SetActive(true);
            battlePanel.alpha = 0;

            player.Init(PlayerManager.Instance.GetPlayerData());
            enemy.Init(_enemy.Init());

            player.UseSynergy();
            enemy.UseSynergy();
            eventManager.HideAllUis();

            SequnceTool.Instance.FadeInCanvasGroup(battlePanel, 0.5f, () =>
            {
                SequnceTool.Instance.Delay(eventManager.BeginEvent, 0.5f);
            });
        }

        private void BeginBattle()
        {
            eventManager.HideAllUis();
            eventManager.eventMode = BattleEventMode.None;

            SetNextAttackTime(Team.All);
            nowFight = true;
            regenerationTime = Time.time + 1;
        }
        
        private void EndBattle(Team _winner)
        {
            nowFight = false;

            if (_winner == Team.Player)
                EndBattle();
            else
                SequnceTool.Instance.Delay(() => EndingManager.Instance.PlayerDead(enemy.transform), 1f);
        }

        private void EndBattle()
        {
            nowFight = false;
            SequnceTool.Instance.Delay(() =>
            {
                SequnceTool.Instance.FadeOutCanvasGroup(battlePanel, 0.5f, () => battlePanel.gameObject.SetActive(false));
            }, 1.4f);
        }

        private void Update()
        {
            if (nowFight)
            {
                if (playerCurrentTime < Time.time) Attack(player, enemy);
                else if (enemyCurrentTime < Time.time) Attack(enemy, player);

                if (nowFight && Time.time > regenerationTime)
                {
                    player.Regeneration();
                    enemy.Regeneration();
                    regenerationTime = Time.time + 1;
                }
            }
        }
        #endregion

        #region Attack
        internal void SurpriseAttack(Team _attacker)
        {
            if (_attacker == Team.Player)
                SurpriseAttack(player, enemy);
            else
                SurpriseAttack(enemy, player);
        }

        private void SurpriseAttack(Character _user, Character _target)
        {
            Attack result = new();

            _user.VisualUpdate(false);
            SequnceTool.Instance.Delay(() => _user.VisualUpdate(true), 0.35f);
            result.dmg = GetDamage(_user, _target);

            _target.HitEvent(result);

            if (_target.DieCheck())
                EndBattle(_user.team);
            else
                BeginBattle();
        }

        public void SetNextAttackTime(Team _team)
        {
            if (_team != Team.Player) enemyCurrentTime = Time.time + (1 / enemy.GetNowStat(SubStatEnum.AtkSpd));
            if (_team != Team.Enemy) playerCurrentTime = Time.time + (1 / player.GetNowStat(SubStatEnum.AtkSpd));
        }

        private void Attack(Character _user, Character _target)
        {
            Attack result = GetAttackData(_user, _target);

            _user.VisualUpdate(false);
            SequnceTool.Instance.Delay(() => _user.VisualUpdate(true), 0.5f);

            _target.HitEvent(result);

            if (!_target.characteristic.isNotBlood) _user.Drain(result.dmg);

            if (_target.DieCheck()) EndBattle(_user.team);

            if (_target.Counter())
            {
                switch (_target.team)
                {
                    case Team.Player:
                        playerCurrentTime = 0;
                        break;

                    case Team.Enemy:
                        enemyCurrentTime = 0;
                        break;
                }
            }

            SetNextAttackTime(_user.team);
        }
        #endregion

        #region Attack Value
        private float DefCalc(float _dmg, float _def, float _reduceDmg) => (_dmg - (_def * 0.5f)) * (1 - _reduceDmg);

        private float GetDamage(Character _user, Character _target)
        {
            float _dmg = _user.GetNowStat(SubStatEnum.Atk);
            _dmg *= (1 + _user.GetNowStat(SubStatEnum.AddDmg));

            _dmg = DefCalc(_dmg, _target.GetNowStat(SubStatEnum.Def), _target.GetNowStat(SubStatEnum.RedDmg));
            _dmg += _user.GetNowStat(SubStatEnum.TrueDmg);

            return _dmg;
        }

        private Attack GetAttackData(Character _user, Character _target)
        {
            Attack result = new();
            float hitValue = _user.GetNowStat(SubStatEnum.HitChance) - _target.GetNowStat(SubStatEnum.DodgeChance);

            if (Random.Range(0, 100f) > hitValue)
            {
                result.attackResult = AttackResult.Dodge;
                return result;
            }

            float dmg = GetDamage(_user, _target);

            if (dmg < 0)
            {
                result.attackResult = AttackResult.Block;
                return result;
            }

            if (Random.Range(0, 100f) <= _user.GetNowStat(SubStatEnum.CriChance))
            {
                result.attackResult = AttackResult.Critical;
                dmg *= _user.GetNowStat(SubStatEnum.CriDmg) * 0.01f;
            }

            result.attackResult = AttackResult.Normal;
            result.dmg = dmg;
            return result;
        }
        #endregion
    }
}