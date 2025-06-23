using karin;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Shy.Anime;
using Shy.Unit;
using Shy.Dice;

namespace Shy
{
    public class BattleManager : MonoBehaviour
    {
        #region 변수
        public static BattleManager Instance;

        [Header("Character")]
        public List<Character> minions, enemies;
        private Dictionary<Character, List<DiceUi>> enemyDiceDic = new();

        [Header("Dice")]
        [SerializeField] private DiceUi dicePrefab;
        private int diceLoop;
        private List<DiceUi> dices = new(), enDices = new();
        [SerializeField] private RectTransform hand;

        [Header("Other")]
        [SerializeField] private GameObject endBtn;
        private UnityAction buffEvent;
        [SerializeField] private RewardCanvas rewardPanel;

        private List<SkillEvent> skillEvents;
        #endregion

        #region Init
        private void Awake()
        {
            if (Instance != null) Destroy(this);
            else Instance = this;
        }

        private void Start()
        {
            rewardPanel.Close();
            Init();
        }

        public void Init()
        {
            var enemyDatas = DataLinkManager.Instance.EnemyData.ToArray();
            var _characterDatas = DataManager.Instance.minions;

            Transform spawn = hand.Find("Groups");

            buffEvent = null;

            CharacterInit(Team.Player, DataManager.Instance.minions);
            CharacterInit(Team.Enemy, enemyDatas.Select(enemy => enemy.data).ToArray());

            foreach (var _character in _characterDatas)
            {
                DiceUi _dUi = Instantiate(dicePrefab, spawn);
                dices.Add(_dUi);
                _dUi.Init(_character.DiceSO, Team.Player);
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                List<DiceUi> _enemyDiceList = new List<DiceUi>();

                foreach (DiceSO dice in enemyDatas[i].GetDices())
                {
                    DiceUi _dUi = Instantiate(dicePrefab, spawn);
                    enDices.Add(_dUi);
                    dices.Add(_dUi);
                    _dUi.Init(dice, Team.Enemy);
                    _enemyDiceList.Add(_dUi);
                }
                enemyDiceDic.Add(enemies[i], _enemyDiceList);
            }

            endBtn.SetActive(false);
            buffEvent += () => StartCoroutine(TurnStart(1f));
            StartCoroutine(TurnStart(0));
        }

        private void CharacterInit(Team _team, CharacterSO[] _so)
        {
            List<Character> characterList = (_team == Team.Player ? minions : enemies);

            for (int i = 0; i < characterList.Count;)
            {
                if (i >= _so.Length || _so[i] == null)
                {
                    characterList[i].Init(null);
                    characterList.RemoveAt(i);
                    continue;
                }

                characterList[i].Init(_so[i]);
                buffEvent += characterList[i].BuffCheck;
                i++;
            }
        }
        #endregion

        #region Turn
        public IEnumerator TurnStart(float _delay)
        {
            yield return new WaitForSeconds(_delay);

            for (int i = 0; i < dices.Count;)
            {
                if (dices[i].DiceDieCheck()) dices.RemoveAt(i);
                else i++;
            }
            
            for (int i = 0; i < 10; i++)
            {
                int rand = Random.Range(0, dices.Count);
                (dices[0], dices[rand]) = (dices[rand], dices[0]);
            }

            for (int i = 0; i < dices.Count; i++) dices[i].transform.SetSiblingIndex(i);

            hand.sizeDelta = new Vector2(180 + 180 * dices.Count, 200);
            float sec = 0.065f / dices.Count;

            for (int i = 0; i <= dices.Count * 10; i++)
            {
                yield return new WaitForSeconds(sec);
                if(i % 10 == 8) dices[i / 10].RollDice();
            }
        }

        public bool CanEnemySelectDice(Character _ch)
        {
            if (_ch is not Enemy _enemy) return true;
            
            int useCnt = enDices.Count(_dice => _dice.CharacterCheck(_enemy));
            return _enemy.actionValue > useCnt;
        }

        public void EndCheck()
        {
            foreach (DiceUi _dice in enDices)
            {
                if (_dice.CharacterCheck(null))
                {
                    endBtn.SetActive(false);
                    return;
                }
            }

            endBtn.SetActive(true);
        }

        public void TurnEnd()
        {
            diceLoop = 0;

            for (int i = 0; i < dices.Count; i++) dices[i].UserCheck();

            endBtn.SetActive(false);
            StartCoroutine(DiceDelay());
        }
        #endregion

        #region Dice
        public int GetEnemyDiceCount(Character _ch) => enemyDiceDic[_ch].Count;

        public void CheckDiceAllFin(DiceUi _dice)
        {
            if (_dice != dices[dices.Count - 1]) return;
        }

        private void UseDice()
        {
            DiceData dData = dices[diceLoop].GetData();
            SkillSOBase skillSO = dData.user.GetSkill(dData.skillNum - 1);
            dData.user.SetSkillAnime(dData.skillNum);

            skillEvents = new List<SkillEvent>();

            SkillSubscribe(skillSO, dData);

            AnimeData _anime = new AnimeData(dData.user, skillSO, skillEvents.ToArray());

            SkillMotionManager.Instance.UseSkill(_anime);
        }

        public IEnumerator NextAction()
        {
            if(++diceLoop >= dices.Count)
            {
                yield return new WaitForSeconds(0.65f);
                buffEvent.Invoke();
            }
            else
            {
                StartCoroutine(DiceDelay());
            }
        }

        private IEnumerator DiceDelay()
        {
            yield return new WaitForSeconds(0.18f);

            if (dices[diceLoop].CharacterCheck(null)) StartCoroutine(NextAction());
            else
            {
                yield return new WaitForSeconds(0.8f);
                UseDice();
            }
        }
        #endregion

        #region Skill
        public List<Character> GetCharacters(Team _team) => (_team == Team.Player) ? minions : enemies;
        
        private void SkillSubscribe(SkillSOBase _skillSO, DiceData _dData)
        {
            var _skill = _skillSO;

            if (_skillSO is NormalSkillSO _nSkill) _skill = _nSkill;
            else if (_skillSO is UpgradableSkillSO _uSkill) _skill = _uSkill;

            foreach (SkillEventSO _event in _skill.GetSkills(_dData.user))
            {
                TargetData _tData = new TargetData(_dData, _event);

                switch (_event)
                {
                    case ValueEventSO _vEvent:
                        skillEvents.Add(new ValueEvent(_tData, _vEvent));
                        break;

                    case BuffSkillEventSO _bEvent:
                        skillEvents.Add(new BuffEvent(_tData, _bEvent.life, _bEvent.bufftype));
                        break;
                }
            }
        }
        #endregion

        public int SetDamage(int _baseDamage, Character _target, int _defPer)
        {
            // defPer = 방어력 무시률
            float def = _target.GetNowStat(StatEnum.Def) * (1 - _defPer * 0.01f), 
                damage = _baseDamage - def * 0.5f;

            //추가 피해량
            damage += damage * _target.GetBonusStat(StatEnum.AdditionalDmg) * 0.01f;

            //피해감소량
            damage = damage * (1 - _target.GetBonusStat(StatEnum.ReductionDmg) * 0.01f);
            return Mathf.RoundToInt(damage);
        }

        public void HealthUiVisible(bool _show)
        {
            foreach (var item in minions) item.HealthVisibleEvent(_show);
            foreach (var item in enemies) item.HealthVisibleEvent(_show);
        }

        public void CharacterDie(Character _ch)
        {
            bool isEnemy = enemies.Contains(_ch);

            if (isEnemy)
            {
                enemies.Remove(_ch);
            }
            else
            {
                minions.Remove(_ch);
            }

            buffEvent -= _ch.BuffCheck;

            if(enemies.Count == 0 || minions.Count == 0)
            {
                Debug.Log("Battle Finish");
                foreach (var _minion in minions)
                {
                    _minion.BattleFinish();
                }
                rewardPanel.Open(isEnemy, 10, 10);
            }

            for (int i = 0; i < dices.Count; i++)
            {
                dices[i].CharacterDeadCheck(_ch);
            }
             
            if (isEnemy)
            {
                foreach (DiceUi dice in enemyDiceDic[_ch])
                {
                    dice.DiceDie();
                    enDices.Remove(dice);
                }
            }
        }
    }
}
