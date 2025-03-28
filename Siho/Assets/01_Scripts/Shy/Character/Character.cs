using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shy
{
    public class Character : MonoBehaviour
    {
        public float maxHp = 5;
        public float hp = 0;
        public int atk = 3;
        public int spd = 3;

        public CharacterSO data;
        public List<int> nums;
        public List<Character> targets;

        private Image healthBar;

        public bool isDie { get; private set; }

        private void Awake()
        {
            healthBar = transform.GetChild(0).GetComponent<Image>();
        }

        private void Update()
        {
            healthBar.fillAmount = hp / maxHp;
        }

        public void Init(CharacterSO _value)
        {
            data = _value;
            maxHp = _value.hp;
            hp = maxHp;
        }

        public void UseAction(UnityAction<SkillSO> _action)
        {
            if (isDie) return;

            SkillSO skill = data.skills[nums[0]];
            int value = skill.GetValue();

            Debug.Log(gameObject.name + $"의 {nums[0]}번째 행동 / 값 : " + value);

            nums.RemoveAt(0);

            _action.Invoke(skill);
        }

        public void OnDie()
        {
            isDie = true;
        }

        [ContextMenu("Set Data")]
        public void SetData()
        {
            hp = data.hp;
            atk = data.atk;
            spd = data.spd;
        }

        internal bool CheckDice() => nums.Count > 0;
    }
}
