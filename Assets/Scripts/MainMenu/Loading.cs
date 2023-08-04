using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    private AsyncOperation m_operation;
    public Image m_progressBar;

    // public void OnEnable()
    // {
    //     SceneManager.sceneLoaded += FinishLoading;
    //     StartCoroutine("ProgressBar");
    // }
    //
    // private IEnumerator ProgressBar()
    // {
    //     m_operation = SceneManager.LoadSceneAsync(AppScenes.GAME_SCENE, LoadSceneMode.Single);
    //     m_operation.allowSceneActivation = false;
    //
    //     while (!m_operation.isDone)
    //     {
    //         Debug.Log(m_operation.progress);
    //         //m_progressBar.fillAmount = m_operation.progress;
    //         yield return null;
    //     }
    // }
    //
    // private void FinishLoading(Scene scene, LoadSceneMode mode)
    // {
    //     SceneManager.sceneLoaded -= FinishLoading;
    //     m_operation.allowSceneActivation = true;
    // }

    public void OnEnable()
    {
        StartCoroutine("ProgressBar");
    }

    private IEnumerator ProgressBar()
    {
        float count = 0;
        m_progressBar.fillAmount = count;

        while (count < 1)
        {
            yield return new WaitForSeconds(0.2f);
            count += 0.1f;
            m_progressBar.fillAmount = count;
        }
        SceneManager.LoadSceneAsync(AppScenes.GAME_SCENE, LoadSceneMode.Single);
    }

    

}