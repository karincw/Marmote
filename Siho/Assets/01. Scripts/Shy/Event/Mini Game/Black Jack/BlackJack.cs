using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Shy.Event.BlackJack
{
    public class BlackJack : MiniGame
    {
        [Header("Name Card")]
        [SerializeField] private BlackJackCard playerCard;
        [SerializeField] private BlackJackCard enemyCard;

        [Header("Dice")]
        [SerializeField][Range(1, 6)] private int maxCnt;
        [SerializeField] private Sprite[] diceEyes;

        [Header("Buttons")]
        [SerializeField] private BlackJackButton playBt;
        [SerializeField] private BlackJackButton stopBt, stayBt, exitBt;

        [Header("Tmps")]
        [SerializeField] private TextMeshProUGUI tmp;
        [SerializeField] private TextMeshProUGUI rewardTmp;

        private int rewardValue;
        private bool gameOff;
        private UnityAction initAction;

        protected override void Start()
        {
            base.Start();

            playBt.onClickEvent = Play;
            stopBt.onClickEvent = Stop;
            exitBt.onClickEvent = GetReward;
            stayBt.onClickEvent = Stay;

            initAction = () => RewardUpdate(gameOff ? 3 : -1);
            initAction += () => SequnceTool.Instance.Delay(() =>
            {
                ButtonState(false);
                if(gameOff)
                {
                    stopBt.LockChange(false);
                    gameOff = false;
                }
            }, 0.5f);

            playerCard.SetEvent(() => SequnceTool.Instance.Delay(() =>
            {
                if (playerCard.OverValueCheck())
                {
                    ButtonState(true);
                    GameResult(Team.Enemy);
                    return;
                }

                if (playerCard.OverCntCheck())
                {
                    Stop();
                    return;
                }

                ButtonState(false);
            }, 0.5f));
            enemyCard.SetEvent(EnemyAI);

            gameOff = true;
        }

        public override void Init()
        {
            base.Init();

            playerCard.Init();
            enemyCard.Init();

            if (gameOff)
            {
                rewardValue = 0;
                rewardTmp.SetText("0");
            }

            tmp.gameObject.SetActive(false);

            ButtonState(true);
            StayBtAble(false);
            ExitBtAble(!gameOff);

            if (gameOff)
                SequnceTool.Instance.FadeInCanvasGroup(canvasGroup, 0.5f, initAction);
            else
                initAction.Invoke();
        }

        #region Button Actions
        private void Play()
        {
            ButtonState(true);
            ExitBtAble(false);
            Roll(Team.Player);
        }
        private void Stop()
        {
            ButtonState(true);
            EnemyAI();
        }

        private void Stay()
        {
            Init();
        }

        private void Exit()
        {
            if (gameOff) return;

            gameOff = true;
            ButtonUseables(false);

            SequnceTool.Instance.Delay(() =>
            {
                SequnceTool.Instance.FadeOutCanvasGroup(canvasGroup, 0.15f, () => canvasGroup.gameObject.SetActive(false));
            }, 0.5f);
        }
        #endregion

        #region Button State
        private void ButtonState(bool _isLock)
        {
            playBt.LockChange(!_isLock);
            stopBt.LockChange(!_isLock);
            exitBt.LockChange(!_isLock);
            stayBt.LockChange(!_isLock);
        }

        private void ButtonUseables(bool _useable)
        {
            playBt.useable = _useable;
            stopBt.useable = _useable;
            exitBt.useable = _useable;
            stayBt.useable = _useable;
        }

        private void StayBtAble(bool _onStay)
        {
            stayBt.gameObject.SetActive(_onStay);
            playBt.gameObject.SetActive(!_onStay);
        }

        private void ExitBtAble(bool _onExit)
        {
            exitBt.gameObject.SetActive(_onExit);
            stopBt.gameObject.SetActive(!_onExit);
        }
        #endregion

        private void Roll(Team _turn)
        {
            int diceValue = Random.Range(0, maxCnt);

            if (_turn == Team.Player)
            {
                playerCard.Roll(diceEyes[diceValue], ++diceValue);
            }
            else if (_turn == Team.Enemy)
            {
                enemyCard.Roll(diceEyes[diceValue], ++diceValue);
            }
        }

        #region Result
        private void GameResult()
        {
            int v = playerCard.value - enemyCard.value;

            if (v > 0) GameResult(Team.Player);
            else if (v < 0) GameResult(Team.Enemy);
            else
            {
                if (playerCard.cnt > enemyCard.cnt) GameResult(Team.Enemy);
                else GameResult(Team.Player);
            }
        }

        private void GameResult(Team _winner)
        {
            string _message = (_winner == Team.Player) ? "WIN" : "LOSE";
            tmp.SetText(_message);
            tmp.gameObject.SetActive(true);

            if (_winner == Team.Enemy)
            {
                rewardValue = 0;
                rewardTmp.SetText("0");
            }

            StayBtAble(true);
            ExitBtAble(true);

            exitBt.LockChange(true);
            stayBt.LockChange(_winner == Team.Player);
        }

        private void RewardUpdate(int _setValue = -1)
        {
            if (_setValue == -1)
                rewardValue *= 2;
            else
                rewardValue = _setValue;

            SequnceTool.Instance.DOCountUp(rewardTmp, rewardValue, 0.15f, new());
        }

        private void GetReward()
        {
            MapManager.instance.money.Value += rewardValue;
            Exit();
        }
        #endregion

        public void EnemyAI()
        {
            SequnceTool.Instance.Delay(() =>
            {
                if (enemyCard.OverValueCheck())
                {
                    GameResult(Team.Player);
                }
                else if (enemyCard.OverCntCheck() == false)
                {
                    if (playerCard.value > enemyCard.value)
                    {
                        Roll(Team.Enemy);
                    }
                    else
                    {
                        GameResult(Team.Enemy);
                    }
                }
                else
                {
                    GameResult();
                }
            }, 0.5f);
            
        }
    }
}
