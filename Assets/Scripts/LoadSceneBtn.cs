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
        GameManager gameManager = GameManager.Instance;
        if (gameManager.playerData.FirstPlay == true)
        {
            Debug.Log("첫 시작. 인트로 씬으로 이동");
            gameManager.GetComponent<LoadScene>().SceneChange("IntroScene");
        }
        else
        {
            if (gameManager.playerData.IsEnd == true)
            {
                Debug.Log("엔딩 씬으로 이동");
                if ((gameManager.playerData.status.Intelligence >= 90)
                && (gameManager.playerData.status.Health >= 70) &&
                (gameManager.playerData.status.Attractiveness >= 70))
                {
                    gameManager.GetComponent<LoadScene>().SceneChange("Ending_GoodScene");
                }
                else
                {
                    gameManager.GetComponent<LoadScene>().SceneChange("Ending_NormalScene");
                }
            }
            else if (gameManager.playerData.IsSemester == true)
            {
                Debug.Log("학기 중. 홈 씬으로 이동");
                gameManager.GetComponent<LoadScene>().SceneChange("HomeScene");
            }
            else
            {
                Debug.Log("방학 중. 방학 씬으로 이동");
                gameManager.GetComponent<LoadScene>().SceneChange("MissionList_VacationScene");
            }
        }
    }
}
