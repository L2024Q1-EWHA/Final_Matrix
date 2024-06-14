using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneBtn : MonoBehaviour
{
    /// <summary>
    /// 버튼 클릭시 해당 씬으로 이동
    /// </summary>
    /// <param name="nextScene"></param>
    public void LoadSceneBtnOnClick(string nextScene)
    {
        GameManager.Instance.GetComponent<LoadScene>().SceneChange(nextScene);
    }

    public void LoadPreviousSceneBtnOnClick()
    {
        GameManager.Instance.GetComponent<LoadScene>().SceneChangeToPreviousScene();
    }

    public void EndGame()
    {
        Application.Quit();
    }

    /// <summary>
    /// Cover 화면에서 '시작하기' 버튼을 눌렀을 때, 진행 상황에 따라 '인트로', '홈화면', '방학 미션 화면'으로 이동
    /// </summary>
    public void LoadPlayScene()
    {
        if (GameManager.Instance.playerData.FirstPlay == true)
        {
            GameManager.Instance.GetComponent<LoadScene>().SceneChange("IntroScene");
        }
        else
        {
            if (GameManager.Instance.playerData.IsSemester == true)
            {
                GameManager.Instance.GetComponent<LoadScene>().SceneChange("HomeScene");
            }
            else
            {
                GameManager.Instance.GetComponent<LoadScene>().SceneChange("MissionList_VacationScene");
            }
        }
    }
}
