using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Shy.Event.BlackJack
{
    public class BlackJackDice : MonoBehaviour
    {
        private Animator anime;
        private Image face, img;
        private UnityAction rollFin;

        private void Awake()
        {
            anime = GetComponent<Animator>();
            img = GetComponent<Image>();
            face = transform.Find("Face").GetComponent<Image>();
            face.gameObject.SetActive(false);
        }

        public void Init(UnityAction _finishAction)
        {
            rollFin = _finishAction;
            img.enabled = false;
            face.gameObject.SetActive(false);
        }

        public void Roll(Sprite _sprite)
        {
            face.sprite = _sprite;
            img.enabled = true;
            
            anime.SetTrigger("Roll");
        }

        public void RollFin()
        {
            face.gameObject.SetActive(true);
            img.enabled = false;

            rollFin?.Invoke();
        }
    }
}