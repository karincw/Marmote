using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Shy.Event;

namespace Shy
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance;

        public Character player, enemy;
        private float playerCurrentTime, enemyCurrentTime, regenerationTime;
        [SerializeField] private CanvasGroup battlePanel;

        private bool nowFight = false;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }

            battlePanel.gameObject.SetActive(false);
        }

        #region Characters
        public Character GetCharacter(Team _target) => (_target == Team.Player) ? player : enemy;
        public Character[] GetCharacters() => new[] { player, enemy };
        #endregion

        #region Event
        public void UserBattleEvent(BattleEvent _bEvent, int _per, int _result)
        {
            bool _success = _per >= _result;
            var so = SOManager.Instance.GetSO(_bEvent);

            EventManager.Instance.ShowBEventText(_success ? so.successMes : so.failMes);

            switch (_bEvent)
            {
                case BattleEvent.Run:
                    if (_success) 
                        GameMakeTool.Instance.Delay(EndBattle, 0.5f);
                    else
                        GameMakeTool.Instance.Delay(BeginBattle, 2f);
                    break;

                case BattleEvent.Surprise:
                    if (_success)
                    {
                        GameMakeTool.Instance.Delay(() => SurpriseAttack(player, enemy), 2f);
                    }
                    else
                    {
                        GameMakeTool.Instance.Delay(BeginBattle, 1.5f);
                    }
                    break;

                case BattleEvent.Talk:
                    if (_success)
                    {
                    }
                    else
                    {
                        GameMakeTool.Instance.Delay(() => BeginBattle(), 2f);
                    }
                    break;
            }
        }

        #endregion

        #region Battle
        public void InitBattle(CharacterDataSO _enemy)
        {
            battlePanel.gameObject.SetActive(true);
            battlePanel.alpha = 0;

            player.Init(PlayerManager.Instance.GetPlayerData());
            enemy.Init(_enemy.Init());

            player.UseSynergy();
            enemy.UseSynergy();

            EventManager.Instance.HideBattleUis();
            SynergyTooltipManager.Instance.Init();

            GameMakeTool.Instance.DOFadeCanvasGroup(battlePanel, 0.5f, () =>
            {
                GameMakeTool.Instance.Delay(EventManager.Instance.SetBEvent, 0.5f);
            });
        }

        private void BeginBattle()
        {
            EventManager.Instance.HideBEventText();

            SetNextAttackTime(Team.All);
            nowFight = true;
            regenerationTime = Time.time + 1;
        }
        
        private void EndBattle(Team _winner)
        {
            nowFight = false;

            if (_winner == Team.Player)
            {
                EndBattle();
            }
            else
            {
                EndingManager.Instance.PlayerDead(enemy.transform);
            }
        }

        private void EndBattle()
        {
            nowFight = false;
            StartCoroutine(BattlePanelOff());
        }

        private IEnumerator BattlePanelOff()
        {
            yield return new WaitForSeconds(3f);

            battlePanel.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (nowFight)
            {
                if (playerCurrentTime < Time.time)
                {
                    Attack(player, enemy);
                }
                else if (enemyCurrentTime < Time.time)
                {
                    Attack(enemy, player);
                }

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
        private void SurpriseAttack(Character _user, Character _target)
        {
            Debug.Log("Surprise Attack\nAttacker : " + _user.gameObject.name + " / Target : " + _target);
            Attack result = new();

            _user.VisualUpdate(false);
            GameMakeTool.Instance.Delay(() => _user.VisualUpdate(true), 0.35f);
            result.dmg = GetDamage(_user, _target);

            _target.HitEvent(result);

            if (_target.DieCheck())
            {
                EndBattle(_user.team);
            }
            else
            {
                BeginBattle();
            }
        }

        public void SetNextAttackTime(Team _team)
        {
            if (_team != Team.Player) enemyCurrentTime = Time.time + (1 / enemy.GetNowStat(SubStatEnum.AtkSpd));
            if (_team != Team.Enemy) playerCurrentTime = Time.time + (1 / player.GetNowStat(SubStatEnum.AtkSpd));
        }

        private float DefCalc(float _dmg, float _def, float _reduceDmg)
        {
            _dmg = _dmg - (_def * 0.5f);
            _dmg *= (1 - _reduceDmg);
            return _dmg;
        }

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

            if(Random.Range(0, 100f) > hitValue)
            {
                result.attackResult = AttackResult.Dodge;
                return result;
            }

            float dmg = GetDamage(_user, _target);

            if(dmg < 0)
            {
                result.attackResult = AttackResult.Block;
                return result;
            }

            if(Random.Range(0, 100f) <= _user.GetNowStat(SubStatEnum.CriChance))
            {
                result.attackResult = AttackResult.Critical;
                dmg *= _user.GetNowStat(SubStatEnum.CriDmg) * 0.01f;
            }

            result.attackResult = AttackResult.Normal;
            result.dmg = dmg;
            return result;
        }

        private void Attack(Character _user, Character _target)
        {
            Debug.Log("Attacker : " + _user.gameObject.name + " / Target : " + _target);

            Attack result = GetAttackData(_user, _target);

            Debug.Log("Attack " + result.attackResult + " : " + result.dmg);

            _user.VisualUpdate(false);
            GameMakeTool.Instance.Delay(() => _user.VisualUpdate(true), 0.5f);

            _target.HitEvent(result);

            if(!_target.characteristic.isNotBlood) _user.Drain(result.dmg);

            if (_target.DieCheck()) EndBattle(_user.team);

            if(_target.Counter())
            {
                Debug.Log("Counter");
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
    }
}