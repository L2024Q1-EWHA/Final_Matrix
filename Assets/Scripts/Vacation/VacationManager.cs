using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System;

public enum StateType
{
    intelligence,
    attractiveness,
    health
}

[Serializable]
public class StatusChangeInfo
{
    public StateType stateType;
    public int changeValue;
}

[Serializable]
public class VacationMission
{
    public string missionName;
    [TextArea]
    public string missionInfoTxt;
    [ArrayElementTitle("stateType")]
    public StatusChangeInfo[] statusChangeInfos;

    [TextArea]
    public string clearMessage;
}

public class VacationManager : MonoBehaviour
{
    [Header("UI")]
    //UI
    [SerializeField] private TMP_Text gradeText;
    [SerializeField] private Transform missionBtns;
    //패널
    [SerializeField] private GameObject popUpPanel;
    [SerializeField] private GameObject clearPanel;
    [SerializeField] private GameObject miniGamePanel;

    //popUpPanel UI Element
    [SerializeField] private TMP_Text popUpInfoTxt;


    //clearPanel UI Element
    [SerializeField] private TMP_Text missionTitle_Text;
    [SerializeField] private TMP_Text missionInfo_Text;
    [SerializeField] private GameObject status_section;
    [SerializeField] private Button ok_btn;


    [Header("Player Data")]
    //PlayerData
    [SerializeField] private int grade;
    [SerializeField] private int missionIndex;

    [Header("Mission Info & Data")]
    //Mission Info Text
    [TextArea]
    [SerializeField] private string[] missionInfoTxt;
    [SerializeField] private Color originTxtColor;
    [ArrayElementTitle("missionName")]
    [SerializeField] private VacationMission[] vacationMission;


    // Start is called before the first frame update
    void Start()
    {
        DataManager.LoadPlayerData();
        //학년 반영
        grade = GameManager.Instance.playerData.status.Grade;
        gradeText.text = grade.ToString() + gradeText.text.Substring(1, gradeText.text.Length - 1).Trim();

        //버튼 상태 업데이트
        UpdateButtonState();

        //팝업 패널, clear panel, minigame panel을 Inactive 상태로 설정
        popUpPanel.SetActive(false);
        clearPanel.SetActive(false);
    }

    public void UpdateButtonState()
    {
        originTxtColor = missionBtns.GetComponentInChildren<TMP_Text>().color;
        for (int i = 0; i < missionBtns.transform.childCount; i++)
        {
            missionBtns.GetChild(i).GetComponent<Button>().interactable = !GameManager.Instance.playerData.VacationMissionState(i);
            missionBtns.GetChild(i).GetComponentInChildren<TMP_Text>().color = GameManager.Instance.playerData.VacationMissionState(i) ? Color.grey : originTxtColor;
        }
    }


    public void ShowPopUpPanel(int index)
    {
        missionIndex = index;
        popUpInfoTxt.text = vacationMission[index].missionInfoTxt;

        //패널 활성화
        popUpPanel.SetActive(true);
    }

    public void ShowClearPanel()
    {
        //팝업 패널, 미니 게임 패널 비활성화
        popUpPanel.SetActive(false);

        missionTitle_Text.text = vacationMission[missionIndex].missionName + " 완료";
        missionInfo_Text.text = vacationMission[missionIndex].clearMessage;

        //스탯 정보 반영


        //패널 활성화
        clearPanel.SetActive(true);


    }

    public void ShowMiniGamePanel()
    {

    }




}
