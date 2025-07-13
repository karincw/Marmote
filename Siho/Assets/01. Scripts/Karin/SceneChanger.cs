using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static SceneChanger instance;

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
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        _waitDelay = new WaitForSeconds(_delay);
        _sp.transform.localScale = new Vector2(Screen.height, Screen.height);
        _fadeMat = _sp.material;
        _currentFade = 2f;
    }

    private void Update()
    {
        _fadeMat.SetFloat(_fadeHash, _currentFade);
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
        LoadingComplete();
    }

    private void LoadingStart()
    {
        Debug.Log("SceneLoadingStart");
        _currentFade = 1.5f;
        DOTween.To(() => _currentFade, x => _currentFade = x, 0, 1f);
    }
    private void LoadingComplete()
    {
        Debug.Log("SceneLoadingEnd");
        _currentFade = 0;
        DOTween.To(() => _currentFade, x => _currentFade = x, 2f, 1f);
    }
}

