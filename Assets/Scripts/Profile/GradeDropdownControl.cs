using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GradeDropdownControl : MonoBehaviour
{
    [Header("Dropdown")]
    [SerializeField]
    private TMP_Dropdown gradeDropdown;
    [SerializeField] private int currentGrade;

    [Header("Show ClearMission")]
    public GameObject clearMissionPrefab;
    public Transform gridPanel;

    void Start()
    {

        UpdateDropdownList();

    }

    public void UpdateDropdownList()
    {
        //현재 학년을 가져온다.
        currentGrade = GameManager.Instance.playerData.status.Grade;

        //드롭다운에 리스터 추가
        gradeDropdown.onValueChanged.AddListener(ShowClearMission);

        // 옵션 리스트를 생성
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        // 현재 학년까지 옵션을 추가
        for (int i = 0; i < currentGrade; i++)
        {
            if (i >= 4) break;
            string optionText = $"{i + 1}학년";
            options.Add(new TMP_Dropdown.OptionData(optionText));
        }
        //드롭다운에 옵션리스트 추가
        gradeDropdown.AddOptions(options);


        // 첫 번째 옵션을 선택하고 ShowClearMission 호출
        if (options.Count > 0)
        {
            gradeDropdown.value = 0;
            ShowClearMission(0);
        }
    }



    private void ShowClearMission(int optionIndex)
    {
        Debug.Log("선택한 학년 : " + (optionIndex + 1));

        //GridPanel 자식 모두 제거
        foreach (Transform child in gridPanel)
        {
            Destroy(child.gameObject);
        }

        string key = (optionIndex + 1).ToString() + "학년";

        if (GameManager.Instance.playerData.clearMissionList.TryGetValue(key, out List<string> clearMissionList))
        {
            foreach (string mission in clearMissionList)
            {
                GameObject missionItem = Instantiate(clearMissionPrefab, gridPanel, false);
                missionItem.GetComponentInChildren<TMP_Text>().text = mission;

            }
        }
        else
        {
            Debug.LogError("No mission list found");
        }

    }

}
