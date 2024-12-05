using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx : SingletonMonobehavior<SceneManagerEx>
{
    public CanvasGroup FadeCanvas;
    public GameObject LoadingObject;
    public TMP_Text LoadingTxt;
    public float FadeDuration = 2;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// �� ������ �մϴ�. ���̵� �ƿ��� �� ���Ŀ� �ε��� ���۵˴ϴ�. 
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName)
    {
        LoadSceneAndFadeStart(sceneName);
    }

    private void LoadSceneAndFadeStart(string sceneName)
    {
        if(FadeCanvas != null)
        {
            FadeCanvas.DOFade(1, FadeDuration).OnStart(()=>FadeCanvas.blocksRaycasts = true).OnComplete(() => { StartCoroutine(LoadSceneCoroutine(sceneName)); });
        }
        else
        {
            Debug.LogError("FadeCanvas�� null �Դϴ�.");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        FadeCanvas.DOFade(0, FadeDuration).OnStart(() => { LoadingObject.SetActive(false); }).OnComplete(() => { FadeCanvas.blocksRaycasts = false; });
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        LoadingObject.SetActive(true);
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        ao.allowSceneActivation = false;

        float timer = 0;
        float percent = 0;

        while (!ao.isDone)
        {
            yield return null;

            timer += Time.deltaTime;
            if(percent >= 90)
            {
                percent = Mathf.Lerp(percent, 100, timer);
                if(percent == 100)
                {
                    ao.allowSceneActivation = true;
                }
            }
            else
            {
                percent = Mathf.Lerp(percent, ao.progress * 100f, timer);
                if (percent >= 90) timer = 0;
            }
            LoadingTxt.text = percent.ToString("0") + "%";
        }
    }
}
