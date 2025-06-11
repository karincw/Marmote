using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace karin
{
    public class SceneChanger : MonoSingleton<SceneChanger>
    {
        [SerializeField] private CanvasGroup _group;
        [SerializeField] private float _delay;

        private WaitForSeconds _waitDelay;

        private void Awake()
        {
            if (Instance == null) { _instance = this; }
            _waitDelay = new WaitForSeconds(_delay);
            DontDestroyOnLoad(this.gameObject);
        }

        public void LoadScene(string sceneName)
        {
            LoadingStart();
            StartCoroutine("LoadSceneAsync", sceneName);
        }
        public void LoadScene(int sceneIdx)
        {
            LoadingStart();
            StartCoroutine("LoadSceneAsync", sceneIdx);
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
            }
            yield return _waitDelay;
            LoadingComplete();
        }
        private IEnumerator LoadSceneAsync(int sceneIdx)
        {
            yield return _waitDelay;
            AsyncOperation asyncOper = SceneManager.LoadSceneAsync(sceneIdx);

            while (!asyncOper.isDone)
            {
                if (asyncOper.progress >= 0.9f)
                {
                    asyncOper.allowSceneActivation = true;
                }
                yield return null;
            }
            yield return _waitDelay;
            LoadingComplete();
        }

        private void LoadingComplete()
        {
            _group.DOFade(0, 0.5f);
            _group.interactable = false;
            _group.blocksRaycasts = false;
        }
        private void LoadingStart()
        {
            Debug.Log("SceneLoadingStart");
            _group.DOFade(1, 0.5f);
            _group.interactable = true;
            _group.blocksRaycasts = true;
        }
    }
}