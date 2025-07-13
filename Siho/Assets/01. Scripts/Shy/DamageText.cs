using TMPro;
using UnityEngine;
using DG.Tweening;
using Shy.Pooling;

namespace Shy
{
    public class DamageText : MonoBehaviour
    {
        private TextMeshProUGUI tmp;

        private void Awake()
        {
            tmp = GetComponent<TextMeshProUGUI>();
            tmp.color = Color.clear;
        }

        public void Use(string _mes, Color _color)
        {
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
            gameObject.SetActive(true);

            Sequence seq = DOTween.Sequence();
            tmp.color = new Color(_color.r, _color.g, _color.b, 0);
            tmp.SetText(_mes);

            seq.Append(tmp.DOFade(1, 0.4f));
            seq.Join(tmp.transform.DOLocalMoveY(100, 0.4f));
            seq.AppendInterval(0.2f);
            seq.Append(tmp.DOFade(0, 0.2f));
            seq.OnComplete(() => PoolingManager.Instance.Push(PoolType.DmgText, gameObject));
        }

        public void Use(float _value, Color _color) => Use((0.01f * Mathf.CeilToInt(_value * 100)).ToString(), _color);

        public void Use(Attack _attack)
        {
            Color _color = Color.white;
            string _mes = "";

            switch (_attack.attackResult)
            {
                case AttackResult.Normal:
                    Use(_attack.dmg, _color);
                    return;

                case AttackResult.Critical:
                    Use(_attack.dmg, new Color(1, 0.87f, 0.35f));
                    return;

                case AttackResult.Dodge:
                    _mes = "DODGE";
                    break;

                case AttackResult.Block:
                    _mes = "BLOCK";
                    break;
            }
            Use(_mes, _color);
        }
    }
}
