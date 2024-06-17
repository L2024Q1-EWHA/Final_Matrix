using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndSemester : MonoBehaviour
{
    private Button endButton;
    private TMP_Text buttonText;

    private void Start()
    {
        endButton = GetComponent<Button>();
        buttonText = GetComponentInChildren<TMP_Text>();
        //이벤트 리스터 연결

        //학년에 따른 text 설정
        if (GameManager.Instance.playerData.status.Grade == 4)
        {
            buttonText.text = "졸업하기";
        }
        else
        {
            buttonText.text = "종강하기";
        }

        //버튼 interactable 설정
        SetInteractable();
    }

    /// <summary>
    /// 종강하기/졸업하기 버튼 interactable 설정하는 메소드
    /// 모든 필수 미션을 클리어해야 interactable이 활성화된다
    /// </summary>
    private void SetInteractable()
    {
        bool flag = GameManager.Instance.IsCompleteAllMandatoryMissions();
        endButton.interactable = flag;
        endButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().color = flag ? Color.white : Color.gray;
    }

}
