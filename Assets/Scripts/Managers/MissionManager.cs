using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Vuforia;

[Serializable]
public class Mission
{
    public string code; // ex) Main1-2, Sub4
    public string title;
    public string label;
    public string info;
}

[Serializable]
public class MissionList
{
    public Mission[] missions;
}

public class MissionManager : MonoBehaviour
{
    public TextAsset jsonFile; // 인스펙터에서 지정할 JSON 파일
    public Mission nowMission = new Mission();
    public Transform panelParentTransform;

    private Mission[] missions;

    void Start()
    {
        Init();
    }

    void Init()
    {
        // 게임매니저에서 현재 미션 코드 얻고 미션 검색하기
        nowMission.code = GameManager.Instance.nowMissionCode;
        SearchMission(nowMission.code);
    }

    // 미션 코드로 미션 검색하기
    void SearchMission(string code)
    {
        string jsonData = jsonFile.text;

        MissionList missionList = JsonUtility.FromJson<MissionList>(jsonData);
        missions = missionList.missions;

        nowMission = missions?.FirstOrDefault(m => m.code == code);
    }

    public void OnTargetFound(string missionCode)
    {
        if (missionCode == nowMission.code)
        {
            // 아이콘 회전

            // Resources 폴더에서 프리팹 불러오기
            GameObject panelPrefab = Resources.Load<GameObject>("MissionPanels/" + nowMission.code);

            if (panelPrefab != null)
            {
                // 생성
                GameObject panelInstance = Instantiate(panelPrefab, panelParentTransform);

                panelInstance.transform.localPosition = Vector3.zero;
                panelInstance.transform.localScale = Vector3.one;
            }
            else
            {
                Debug.LogError($"Failed to load prefab '{nowMission.code}' from Resources folder.");
            }
        }
    }
}
