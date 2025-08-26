using TMPro;
using UnityEngine;
using DG.Tweening;
using Shy.Pooling;

namespace Shy
{
    public class DamageText : Pool
    {
        private TextMeshProUGUI tmp;

        private void Awake()
        {
            tmp = GetComponent<TextMeshProUGUI>();
            tmp.color = Color.clear;
        }

        private int GetLocalY() => -25 * transform.parent.childCount;

        public void Use(string _mes, Color _color)
        {
            float y = GetLocalY();

            transform.localPosition = new(0, y, 0);
            transform.localScale = Vector3.one;

            gameObject.SetActive(true);

            tmp.color = new(_color.r, _color.g, _color.b, 0);
            tmp.SetText(_mes);

            Sequence seq = DOTween.Sequence();
            seq.Append(tmp.DOFade(1, 0.4f));
            seq.Join(transform.DOLocalMoveY(150 + y, 0.4f));
            seq.AppendInterval(0.2f);
            seq.Append(tmp.DOFade(0, 0.2f));
            seq.OnComplete(() => PoolingManager.Instance.Push(PoolType.DmgText, this));
        }

        public void Use(float _value, Color _color) => Use((0.01f * Mathf.CeilToInt(_value * 100)).ToString(), _color);

        public void Use(Attack _attack)
        {
            Color _color = Color.white;

            switch (_attack.attackResult)
            {
                case AttackResult.Normal:
                    Use(_attack.dmg, _color);
                    return;

                case AttackResult.Critical:
                    Use(_attack.dmg, new(1, 0.87f, 0.35f));
                    return;

                case AttackResult.Dodge:
                    Use("DODGE", _color);
                    return;

                case AttackResult.Block:
                    Use("BLOCK", _color);
                    return;
            }
        }
    }
}
