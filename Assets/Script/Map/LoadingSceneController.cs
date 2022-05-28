using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private Image progressBar;

    private string loadSceneName;
    private float time = 0;

    public void LoadScene(string sceneName)
    {
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += OnSceneLoaded;
        loadSceneName = sceneName;
        StartCoroutine(LoadSceneProcess());
    }

    private IEnumerator LoadSceneProcess()
    {
        progressBar.fillAmount = 0f;

        //Fade될때까지 기달리기
        yield return StartCoroutine(PlayFadeOut());

        AsyncOperation op = SceneManager.LoadSceneAsync(loadSceneName);
        //씬 로딩이 끝나도 자동으로 씬을 로드하지 않음
        op.allowSceneActivation = false;

        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
            }
            else
            {
                timer += Time.unscaledTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.name == loadSceneName)
        {
            StartCoroutine(PlayFadeIn());
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    IEnumerator PlayFadeIn()
    {
        canvasGroup.blocksRaycasts = false;
        time = 0f;
        canvasGroup.alpha = 1;

        while (canvasGroup.alpha > 0f)
        {
            time += Time.deltaTime / 2;

            // 알파 값 계산.  
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, time);
            yield return null;
        }
    }

    IEnumerator PlayFadeOut()
    {
        
        time = 0f;
        canvasGroup.alpha = 0;

        while (canvasGroup.alpha < 1f)
        {
            time += Time.deltaTime / 2;

            // 알파 값 계산.  
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, time);
            yield return null;
        }
        canvasGroup.blocksRaycasts = true;
    }

        /*
        private IEnumerator Fade(bool isFadeIn)
        {
            float timer = 0f;
            gameObject.SetActive(true);
            while (timer <= 1f)
            {
                yield return null;
                timer += Time.unscaledTime * 3f;
                canvasGroup.alpha = isFadeIn ? Mathf.Lerp(0f, 1f, timer) : Mathf.Lerp(1f, 0f, timer);
            }

            if (!isFadeIn)
            {
                gameObject.SetActive(false);
            }
        }
        */
    }
