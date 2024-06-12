using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionInfoPopup : MonoBehaviour
{
    public TextAsset jsonFile; // �ν����Ϳ��� ������ JSON ����
    private Mission[] missions;
    Mission mission = new Mission();

    [Header("UI")]
    public TextMeshProUGUI labelText;
    public TextMeshProUGUI contentText;
    public TextMeshProUGUI infoText;
    public Image image;

    // �̼� �˻� �� �Ҵ�
    public void MissionInfoSet(string missionCode)
    {
        string jsonData = jsonFile.text;
        string path = "AR_Targets/" + missionCode;

        // �˻�
        MissionList missionList = JsonUtility.FromJson<MissionList>(jsonData);
        missions = missionList.missions;

        mission = missions?.FirstOrDefault(m => m.code == missionCode);

        // �Ҵ�
        labelText.text = mission.label;
        contentText.text = mission.title;
        infoText.text = mission.info;
        image.sprite = Resources.Load<Sprite>(path);
        Debug.Log(path);
    }   
}
