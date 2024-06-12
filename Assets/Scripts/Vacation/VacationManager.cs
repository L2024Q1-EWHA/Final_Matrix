using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class VacationManager : MonoBehaviour
{
    //UI
    [SerializeField] private TMP_Text gradeText;
    [SerializeField] private Transform missionBtns;
    [SerializeField] private GameObject popUpPanel;
    [SerializeField] private TMP_Text popUpInfoTxt;

    //PlayerData
    [SerializeField] private int grade;
    [SerializeField] private int missionIndex;


    //Mission Info Text
    [TextArea]
    [SerializeField] private string[] missionInfoTxt;
    [SerializeField] private Color originTxtColor;

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
        popUpInfoTxt.text = missionInfoTxt[index];
        popUpPanel.SetActive(true);
    }

}
