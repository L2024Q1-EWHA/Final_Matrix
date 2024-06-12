using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionMenu : MonoBehaviour
{
    [SerializeField] int thisMissionGrade; // 서브 미션은 0
    [SerializeField] GameObject Buttons;
    [SerializeField] GameObject CheckImage;

    void Start()
    {
        // 메인 미션의 경우 해당 학년이 아니면 비활성화
        if (this.CompareTag("MainMission"))
        {
            if(thisMissionGrade != GameManager.Instance.playerData.status.Grade)
            {
                gameObject.SetActive(false);
            }
            else // 클리어한 미션 반영
            {
                if (SearchMainClear(gameObject.name))
                {
                    SetClear();
                    transform.SetAsLastSibling();
                }
            }
        }
        // 서브 미션의 경우 클리어한 미션은 클리어 표시 + 맨뒤로 보내기
        else if (this.CompareTag("SubMission"))
        {
            if (SearchSubClear(gameObject.name))
            {
                SetClear();
                transform.SetAsLastSibling();
            }
        }
        else Debug.LogError("미션 태그 설정 안됨");
    }

    bool SearchSubClear(string thisName)
    {
        if (GameManager.Instance.playerData.subMission[thisName[thisName.Length - 1] - '0' - 1])
        {
            return true;
        }
        return false;
    }

    bool SearchMainClear(string thisName)
    {
        string key = GameManager.Instance.playerData.status.Grade.ToString() + "학년";
        int missionIndex = thisName[thisName.Length - 1] - '0' - 1;
        if (GameManager.Instance.playerData.mandatoryMission[key][missionIndex])
        {
            return true;
        }
        return false;
    }

    void SetClear()
    {
        Buttons.SetActive(false);
        CheckImage.SetActive(true);
    }
}
