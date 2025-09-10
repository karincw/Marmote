using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace Shy.Pooling
{
    public class Particle : MonoBehaviour
    {
        private Image image;
        private Rigidbody2D rigid;
        private ParticleClamp clamp;
        private UnityAction<Particle> removeEvent;

        private void Awake()
        {
            image = GetComponent<Image>();
            rigid = GetComponent<Rigidbody2D>();
        }

        public void Init(ParticleClamp _range, UnityAction<Particle> _removeEvent)
        {
            clamp = _range;
            removeEvent = _removeEvent;
        }

        public void Init(Sprite _sprite, Color _color)
        {
            image.sprite = _sprite;
            image.color = _color;
            transform.localPosition = Vector3.zero;
            rigid.gravityScale = 1;
            gameObject.SetActive(false);
        }

        public void Play()
        {
            gameObject.SetActive(true);
            rigid.AddForce(new(Random.Range(-1.5f, 1.5f), Random.Range(0.5f, 2f)), ForceMode2D.Impulse);
        }

        private void VelocityReset()
        {
            rigid.linearVelocity = Vector2.zero;
            rigid.gravityScale = 0;

            Vector3 pos = transform.position;
            pos.y = clamp.yMin.position.y;
            transform.position = pos;

            removeEvent?.Invoke(this);
        }

        private void FixedUpdate()
        {
            Vector3 pos = rigid.position;
            Vector3 vel = rigid.linearVelocity;

            if (pos.x >= clamp.xMax.position.x && vel.x > 0) vel.x = 0;
            if (pos.x <= clamp.xMin.position.x && vel.x < 0) vel.x = 0;

            rigid.linearVelocity = vel;

            if (pos.y <= clamp.yMin.position.y && vel.y < 0) VelocityReset();
        }
    }

}