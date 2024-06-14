using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private string previousSceneName; // 이전 씬 이름 저장할 변수

    /// <summary>
    /// 해당 이름의 Scene으로 이동
    /// </summary>
    /// <param name="sceneName"></param>
    public void SceneChange(string sceneName)
    {
        StartCoroutine(LoadGameSceneAsync(sceneName));
    }

    private IEnumerator LoadGameSceneAsync(string sceneName)
    {
        previousSceneName = SceneManager.GetActiveScene().name;

        // 비동기 씬 로딩
        AsyncOperation asyncLaod = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLaod.isDone)
        {
            yield return null;
        }
    }

    /// <summary>
    /// 이전 씬으로 이동
    /// </summary>
    public void SceneChangeToPreviousScene()
    {
        if (previousSceneName == null || previousSceneName == "")
        {
            //=========임시 수정==============//
            previousSceneName = "HomeScene";
            StartCoroutine(LoadGameSceneAsync(previousSceneName));
            //=============================//
            Debug.LogError("There is no previous scene");
        }
        else StartCoroutine(LoadGameSceneAsync(previousSceneName));
    }
}
