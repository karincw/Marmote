using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Shy
{
    public class HealthGuageCompo : MonoBehaviour
    {
        [SerializeField] private Image healthGuage;

        public void HealthUpdate(float _nowHp, float _maxHp, bool _dot = false)
        {
            if (_dot)
                healthGuage.DOFillAmount(_nowHp / _maxHp, 0.3f);
            else
                healthGuage.fillAmount = _nowHp / _maxHp;
        }
    }
}
