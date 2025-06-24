using Shy.Unit;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Show = System.SerializableAttribute;

namespace Shy
{
    [Show]
    public struct Stat
    {
        public int maxHp;
        [HideInInspector]
        public int hp
        {
            get => Hp;
            set
            {
                if (value < 0) value = 0;
                if (value > maxHp) value = maxHp;
                Hp = value;
            }
        }
        [SerializeField] private int Hp;
        public int str;
        public int def;

        public static Stat operator +(Stat s1, Stat s2)
        {
            return new Stat
            {
                maxHp = s1.maxHp + s2.maxHp,
                hp = s1.hp + s2.hp,
                str = s1.str + s2.str,
                def = s1.def + s2.def
            };
        }
        public override string ToString() => $"[MaxHP : {maxHp} | Hp : {hp} | Strength : {str} | Defence : {def}]";
    }

    [Show]
    public struct GetStat
    {
        public string key;
        public StatEnum stat;
        public bool self;
    }

    [Show]
    public struct GetStack
    {
        public string key;
        public BuffType buff;
    }

    [Show]
    public struct PoolItem
    {
        public GameObject obj;
        public int spawnCnt;
        public PoolingType type;
    }

    [Show]
    public struct LevelByDice
    {
        public int level;
        public DiceSO[] dices;
    }

    public struct DiceData
    {
        public Character user;
        public ActionWay actionWay;
        public Team team;
        public int skillNum;
    }

    public struct TargetData
    {
        public Character user;
        public ActionWay actionWay;
        public TargetWay targetTeam;

        public TargetData(DiceData _d, SkillEventSO _s)
        {
            user = _d.user;
            targetTeam = _s.targetTeam;


            if (_s.actionWay == ActionWay.None) actionWay = _d.actionWay;
            else actionWay = _s.actionWay;

            Debug.Log(actionWay);
        }
    }

    namespace Anime
    {
        public struct AnimeData
        {
            public Character user;
            public SkillSOBase skillSO;
            public SkillEvent[] events;


            public AnimeData(Character _d, SkillSOBase _s, SkillEvent[] _e)
            {
                user = _d;
                skillSO = _s;
                events = _e;
            }

            public Sprite GetMotion(AnimeType _anime) => skillSO.GetMotionSprite(_anime, user);
            public SkillMotion GetSkillMotion() => skillSO.GetSkillMotion(user);
        }

    }

    namespace Info
    {
        public struct CharacterInfo : IInfoData
        {
            public CharacterSO so;
            public Character character;

            public CharacterInfo(Character _ch, CharacterSO _d)
            {
                character = _ch;
                so = _d;
            }
        }

        public struct DiceInfo : IInfoData
        {
            public void SetData(Transform _panel)
            {
            }
        }

        public struct BuffInfo : IInfoData
        {
            public Transform trm;
            public BuffType buffType;

            public BuffInfo(BuffType _buffType, Transform _trm)
            {
                buffType = _buffType;
                trm = _trm;
            }
        }
    }

    namespace Target
    {
        [Show]
        struct ActionTarget
        {
            public ActionWay way;
            public Sprite icon;
        }
    }

    namespace Dice
    {
        struct UserIcon
        {
            public Image userIcon, userMask, maskPaint;
            public void SetVariable(Transform _visual)
            {
                userIcon = _visual.Find("User Icon").GetComponent<Image>();
                userMask = _visual.Find("User Mask").GetComponent<Image>();
                maskPaint = userMask.transform.Find("Paint").GetComponent<Image>();
            }

            public void UpdateUser(Character _ch)
            {
                userIcon.sprite = userMask.sprite = _ch.GetIcon();
                maskPaint.color = _ch.posColor;

                IconVisible(true);
            }

            public void DeleteUser()
            {
                userIcon.sprite = userMask.sprite = null;
                IconVisible(false);
            }

            private void IconVisible(bool _show)
            {
                userIcon.gameObject.SetActive(_show);
                userMask.gameObject.SetActive(_show);
            }
        }
    }
}