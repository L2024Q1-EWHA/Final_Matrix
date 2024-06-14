using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionInfoPanel : MonoBehaviour
{
    public MissionManager missionManager;
    private Mission mission;
    public TextMeshProUGUI text;

    private void Start()
    {
        mission = missionManager.nowMission;
        if (mission == null)
        {
            Debug.Log("now mission is null");
        }
        text.text = mission.info;
    }

    private void OnEnable()
    {
        mission = missionManager.nowMission;
        if (mission == null)
        {
            Debug.Log("now mission is null");
        }
        text.text = mission.info;
    }


}
