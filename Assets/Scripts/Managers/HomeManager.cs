using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HomeManager : MonoBehaviour
{
    public TMP_Text gradetext;
    public GameObject popup;
    public TMP_Text popupMsg;
    public LoadSceneBtn loadscript;
    private int grade;

    // Start is called before the first frame update
    void Start()
    {
        ShowGrade();
    }


    public void ShowGrade()
    {
        grade = GameManager.Instance.playerData.status.Grade;
        switch (grade)
        {
            case 1:
                gradetext.text = "1학년 재학중";
                break;
            case 2:
                gradetext.text = "2학년 재학중";
                break;
            case 3:
                gradetext.text = "3학년 재학중";
                break;
            case 4:
                gradetext.text = "4학년 재학중";
                break;
        }
    }

    public void PopUp() //4학년일 때 팝업 메시지 수정
    {
        if (GameManager.Instance.playerData.status.Grade == 4)
            popupMsg.text = "졸업 가능 여부를 확인해볼까?";
        popup.gameObject.SetActive(true);
        // if (GameManager.Instance.IsCompleteAllMandatoryMissions())
        // {
        //     popup.gameObject.SetActive(true);
        // }
    }

    public void EndSemester()
    {
        if (GameManager.Instance.playerData.status.Grade < 4)
        {
            GameManager.Instance.playerData.IsSemester = false;
            loadscript.LoadSceneBtnOnClick("MissionList_VacationScene");
        }
        else
        {
            GameManager.Instance.playerData.IsEnd = true;
            if ((GameManager.Instance.playerData.status.Intelligence >= 90)
                && (GameManager.Instance.playerData.status.Health >= 70) &&
                (GameManager.Instance.playerData.status.Attractiveness >= 70))
            {
                loadscript.LoadSceneBtnOnClick("Ending_GoodScene");
            }
            else
            {
                loadscript.LoadSceneBtnOnClick("Ending_NormalScene");
            }
        }
    }
}
