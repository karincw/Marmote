using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Shy.Pooling
{
    public class ParticleSystem : Pool
    {
        [Header("Particle Data")]
        [SerializeField] private Sprite sprite;
        [SerializeField] private Particle prefab;
        [SerializeField] private Color color;
        [Header("Use Data")]
        [SerializeField, Range(0, maxCount)] private int spawnValue;
        [SerializeField] private float removeDelay;
        [SerializeField] private Vector2 spawnDelay;
        [SerializeField] private ParticleClamp clamp;

        private const int maxCount = 15;

        private Particle[] totalParticles, useParticles;
        private int particleCnt;
        
        private void Awake()
        {
            totalParticles = new Particle[maxCount];

            for (int i = 0; i < maxCount; i++)
            {
                var obj = Instantiate(prefab, transform);
                obj.Init(clamp, (Particle _p) => StartCoroutine(FinishCheck(_p)));
                totalParticles[i] = obj;
                obj.gameObject.SetActive(false);
            }
        }

        [ContextMenu("Init")]
        public override void Init()
        {
            base.Init();

            useParticles = new Particle[spawnValue];

            for (int i = 0; i < spawnValue; i++)
            {
                useParticles[i] = totalParticles[i];
                useParticles[i].Init(sprite, color);
            }
        }

        [ContextMenu("asd")]
        public void Play()
        {
            gameObject.SetActive(true);

            particleCnt = useParticles.Length;
            foreach (var item in useParticles)
            {
                StartCoroutine(PlayParticle(item));
            }
        }

        private IEnumerator PlayParticle(Particle _particle)
        {
            yield return new WaitForSeconds(Random.Range(spawnDelay.x, spawnDelay.y));
            _particle.Play();
        }

        private IEnumerator FinishCheck(Particle _particle)
        {
            yield return new WaitForSeconds(removeDelay);

            _particle.gameObject.SetActive(false);

            if (--particleCnt == 0)
            {
                PoolingManager.Instance.Push(PoolType.DmgParticle, this);
            }
        }
    }
}
