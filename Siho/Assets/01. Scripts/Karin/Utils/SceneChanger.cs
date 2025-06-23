using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace karin
{
    public class SceneChanger : MonoSingleton<SceneChanger>
    {
        [SerializeField] private SpriteRenderer _sp;
        [SerializeField] private float _delay;
        private Material _fadeMat;

        private WaitForSeconds _waitDelay;
        private int _fadeHash = Shader.PropertyToID("_Radius");
        private float _currentFade;

        public static Action<string> OnSceneChange;
        public static Action<string> OnSceneChanged;

        private void Awake()
        {
            if (Instance == null) { _instance = this; }
            DontDestroyOnLoad(gameObject);
            _waitDelay = new WaitForSeconds(_delay);
            _fadeMat = _sp.material;
            _currentFade = 1.5f;
        }

        public void LoadScene(string sceneName)
        {
            OnSceneChange?.Invoke(sceneName);
            LoadingStart();
            StartCoroutine("LoadSceneAsync", sceneName);
        }

        private IEnumerator LoadSceneAsync(string sceneName)
        {
            yield return _waitDelay;
            AsyncOperation asyncOper = SceneManager.LoadSceneAsync(sceneName);

            while (!asyncOper.isDone)
            {
                if (asyncOper.progress >= 0.9f)
                {
                    asyncOper.allowSceneActivation = true;
                }
                yield return null;
                _sp.transform.position = Camera.main.transform.position + Camera.main.transform.forward;
                _sp.transform.rotation = Camera.main.transform.rotation;
            }
            OnSceneChanged?.Invoke(sceneName);
            yield return _waitDelay;
            LoadingComplete();
        }

        private void Update()
        {
            _fadeMat.SetFloat(_fadeHash, _currentFade);
        }

        private void LoadingStart()
        {
            Debug.Log("SceneLoadingStart");
            _currentFade = 1.5f;
            DOTween.To(() => _currentFade, x => _currentFade = x, 0, 0.4f);
        }
        private void LoadingComplete()
        {
            Debug.Log("SceneLoadingEnd");
            _currentFade = 0;
            DOTween.To(() => _currentFade, x => _currentFade = x, 1.5f, 0.4f);
        }
    }
}