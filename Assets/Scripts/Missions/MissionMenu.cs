using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionMenu : MonoBehaviour
{
    [SerializeField] int thisMissionGrade; // ���� �̼��� 0
    [SerializeField] GameObject Buttons;
    [SerializeField] GameObject CheckImage;

    void Start()
    {
        // ���� �̼��� ��� �ش� �г��� �ƴϸ� ��Ȱ��ȭ
        if (this.CompareTag("MainMission"))
        {
            if(thisMissionGrade != GameManager.Instance.playerData.status.Grade)
            {
                gameObject.SetActive(false);
            }
            else // Ŭ������ �̼� �ݿ�
            {
                if (SearchMainClear(gameObject.name))
                {
                    SetClear();
                    transform.SetAsLastSibling();
                }
            }
        }
        // ���� �̼��� ��� Ŭ������ �̼��� Ŭ���� ǥ�� + �ǵڷ� ������
        else if (this.CompareTag("SubMission"))
        {
            if (SearchSubClear(gameObject.name))
            {
                SetClear();
                transform.SetAsLastSibling();
            }
        }
        else Debug.LogError("�̼� �±� ���� �ȵ�");
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
        string key = GameManager.Instance.playerData.status.Grade.ToString() + "�г�";
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
