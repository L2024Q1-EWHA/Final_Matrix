using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using System;

public enum StatusType
{
    intelligence,
    attractiveness,
    health
}

[Serializable]
public class StatusChangeInfo
{
    public StatusType statusType;
    public int changeValue;
}

[Serializable]
public class VacationMission
{
    public string missionName;
    public string label;
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
    [SerializeField] private GameObject status_prefab;
    [SerializeField] private Sprite[] ArrowImages; //up, down


    [Header("Player Data")]
    //PlayerData
    [SerializeField] private int grade;
    [SerializeField] private int missionIndex;

    [Header("Mission Info & Data")]
    [SerializeField] private Color originTxtColor;
    [ArrayElementTitle("missionName")]
    [SerializeField] private VacationMission[] vacationMission;


    // Start is called before the first frame update
    void Start()
    {
        //DataManager.LoadPlayerData(); //테스트를 위해 임시 추가;
        //학년 반영
        grade = GameManager.Instance.playerData.status.Grade;
        gradeText.text = grade.ToString() + gradeText.text.Substring(1, gradeText.text.Length - 1).Trim();

        //버튼 상태 업데이트
        UpdateButtonState();

        //팝업 패널, clear panel, minigame panel을 Inactive 상태로 설정
        popUpPanel.SetActive(false);
        clearPanel.SetActive(false);
    }


    /// <summary>
    /// 방학 미션 바튼 상태 업데이트(interactable)
    /// </summary>
    public void UpdateButtonState()
    {
        originTxtColor = missionBtns.GetComponentInChildren<TMP_Text>().color;
        for (int i = 0; i < missionBtns.transform.childCount; i++)
        {
            missionBtns.GetChild(i).GetComponent<Button>().interactable = !GameManager.Instance.playerData.VacationMissionState(i);
            missionBtns.GetChild(i).GetComponentInChildren<TMP_Text>().color = GameManager.Instance.playerData.VacationMissionState(i) ? Color.grey : originTxtColor;
        }
    }

    /// <summary>
    /// 팝업 패널 활성화
    /// </summary>
    /// <param name="index">방학 미션 index</param>
    public void ShowPopUpPanel(int index)
    {
        missionIndex = index;
        popUpInfoTxt.text = vacationMission[index].missionInfoTxt;

        //패널 활성화
        popUpPanel.SetActive(true);
    }

    /// <summary>
    /// 미션 성공 패널 활성화 & 스탯 업데이트 & 학년 업데이트 & 미션 상태 업데이트
    /// </summary>
    public void ShowClearPanel()
    {
        //미션 상태 업데이트 (방학 미션 & 수행 미션 추가)
        if (missionIndex != 3)
        {
            GameManager.Instance.playerData.UpdateVacationMissionState(missionIndex, true);
        }

        GameManager.Instance.playerData.AddClearMission(grade, vacationMission[missionIndex].label);

        //스탯 업데이트 & 스탯 정보 반영

        //1)Status_section 자식 모두 제거(초기화)
        foreach (Transform child in status_section.transform)
        {
            Destroy(child.gameObject);
        }
        //2) 스탯 업데이트 & 스탯 정보 반영
        foreach (StatusChangeInfo statusChangeInfo in vacationMission[missionIndex].statusChangeInfos)
        {
            UpdateStatus(statusChangeInfo);
        }

        //학년 업데이트
        GameManager.Instance.ChangeGrade(++grade);


        //팝업 패널 비활성화
        popUpPanel.SetActive(false);

        missionTitle_Text.text = vacationMission[missionIndex].missionName + " 완료";
        missionInfo_Text.text = vacationMission[missionIndex].clearMessage;

        //스탯 정보 반영 (위에서 처리)

        //패널 활성화
        clearPanel.SetActive(true);


    }


    /// <summary>
    /// 플레이어 스탯 데이터 업데이트 & 스탯 UI 반영
    /// </summary>
    /// <param name="statusChangeInfo"></param>
    private void UpdateStatus(StatusChangeInfo statusChangeInfo)
    {

        //UI 반영
        GameObject status_UI = Instantiate(status_prefab, status_section.transform, false); //프리랩 생성
        //UI 설정을 위한 변수 선언
        TMP_Text status_UI_Type = status_UI.transform.GetChild(0).GetComponent<TMP_Text>();
        TMP_Text status_UI_Value = status_UI.transform.GetChild(1).GetComponent<TMP_Text>();
        Image status_UI_ArrowImg = status_UI.transform.GetComponentInChildren<Image>();

        //ui 화살표 이미지 변경
        status_UI_ArrowImg.sprite = statusChangeInfo.changeValue >= 0 ? ArrowImages[0] : ArrowImages[1];
        //ui value 변경
        status_UI_Value.text = statusChangeInfo.changeValue >= 0 ? statusChangeInfo.changeValue.ToString("D2") : statusChangeInfo.changeValue.ToString(); //2자리로 표현

        //스탯 데이터 업데이트 & 스탯 type UI 변경
        switch (statusChangeInfo.statusType)
        {
            case StatusType.intelligence:
                status_UI_Type.text = "지성";
                GameManager.Instance.IntelligenceStatusUpDown(statusChangeInfo.changeValue);
                break;
            case StatusType.attractiveness:
                status_UI_Type.text = "매력";
                GameManager.Instance.AttractivenessStatusUpDown(statusChangeInfo.changeValue);
                break;
            case StatusType.health:
                status_UI_Type.text = "건강";
                GameManager.Instance.HealthStatusUpDown(statusChangeInfo.changeValue);
                break;
        }
    }

    public void ShowMiniGamePanel()
    {
        popUpPanel.SetActive(false);
        clearPanel.SetActive(false);
        miniGamePanel.SetActive(true);

    }

    public void EndVacation()
    {
        Debug.Log("방학 끝. flag 업데이트");
        GameManager.Instance.playerData.IsSemester = true;
    }






}
