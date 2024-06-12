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
}

public class VacationManager : MonoBehaviour
{
    [Header("UI")]
    //UI
    [SerializeField] private TMP_Text gradeText;
    [SerializeField] private Transform missionBtns;
    [SerializeField] private GameObject popUpPanel;
    [SerializeField] private GameObject clearPanel;
    [SerializeField] private GameObject miniGamePanel;
    [SerializeField] private TMP_Text popUpInfoTxt;

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


    // public StatusChangeInfo[] statusChangeInfos;


    // Start is called before the first frame update
    void Start()
    {
        DataManager.LoadPlayerData();
        //학년 반영
        grade = GameManager.Instance.playerData.status.Grade;
        gradeText.text = grade.ToString() + gradeText.text.Substring(1, gradeText.text.Length - 1).Trim();

        //버튼 상태 업데이트
        UpdateButtonState();

        //팝업 패널을 Inactive 상태로 설정
        popUpPanel.SetActive(false);
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


    public void PopUpPanel(int index)
    {
        missionIndex = index;
        popUpInfoTxt.text = vacationMission[index].missionInfoTxt;
        popUpPanel.SetActive(true);
    }

}
