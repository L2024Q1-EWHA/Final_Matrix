using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearMission : MonoBehaviour
{
    [SerializeField] private MissionManager missionManager;

    private void Start()
    {
        if (missionManager == null)
        {
            missionManager = GameObject.FindObjectOfType<MissionManager>();
        }
    }

    /// <summary>
    /// 현재 미션 정보(GameManager의 nowMission)를 기반으로 미션 인증 완료 데이터 저장
    /// </summary>
    public void MissionClear()
    {
        //미션 코드
        string missionCode = missionManager.nowMission.code;
        //미션 라벨
        string missionLabel = missionManager.nowMission.label;
        //현재 학년 
        int currentGrade = GameManager.Instance.playerData.status.Grade;



        //클리어한 미션 반영
        GameManager.Instance.playerData.AddClearMission(missionCode, currentGrade, missionLabel);
    }
}
