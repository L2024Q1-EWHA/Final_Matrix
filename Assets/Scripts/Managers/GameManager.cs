using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
// 값을 설정할 때마다 OnStatusChanged 이벤트 호출
// 프로퍼티 생성
public class Status
{
    [SerializeField] private int grade;
    [SerializeField] private int intelligence;
    [SerializeField] private int attractiveness;
    [SerializeField] private int health;

    public int Grade
    {
        get => grade;
        set
        {
            if (grade != value)
            {
                grade = value;
                OnStatusChanged?.Invoke();
            }



        }
    }

    public int Intelligence
    {
        get => intelligence;
        set
        {
            int newValue = Mathf.Clamp(value, 0, 100); //0~100 사이 값으로 제한
            if (intelligence != newValue)
            {
                intelligence = newValue;
                Debug.Log($"지성 스탯 업데이트 : {intelligence}");
                OnStatusChanged?.Invoke();

            }

        }
    }

    public int Attractiveness
    {
        get => attractiveness;
        set
        {
            int newValue = Mathf.Clamp(value, 0, 100);//0~100 사이 값으로 제한
            if (newValue != attractiveness)
            {
                attractiveness = newValue;
                Debug.Log($"매력 스탯 업데이트 : {attractiveness}");
                OnStatusChanged?.Invoke();

            }

        }
    }
    public int Health
    {
        get => health;
        set
        {
            int newValue = Mathf.Clamp(value, 0, 100);//0~100 사이 값으로 제한
            if (health != newValue)
            {
                health = newValue;
                Debug.Log($"건강 스탯 업데이트 : {health}");
                OnStatusChanged?.Invoke();
            }

        }
    }

    /// <summary>
    /// Status 변경 시 호출되는 이벤트
    /// </summary>
    public event Action OnStatusChanged;

}

[Serializable]
public class PlayerData
{
    public Status status;
    public Dictionary<string, List<string>> clearMissionList;
    public Dictionary<string, bool[]> mandatoryMission;

    /// <summary>
    /// PlayerData 변경 시 호출되는 이벤트
    /// </summary>
    public event Action OnDataChanged;


    /// <summary>
    /// 생성자: Status 초기화 및 Status 변경 시 OnDataChanged 이벤트 호출하도록 설정
    /// </summary>
    public PlayerData()
    {
        status = new Status();
        clearMissionList = new Dictionary<string, List<string>>();
        status.OnStatusChanged += () =>
        {
            OnDataChanged?.Invoke(); // Status 변경 시 OnDataChanged 호출
        };
        OnDataChanged += DataManager.SavePlayerData;
    }


    /// <summary>
    /// 플레이어가 수행한 미션을 PlayerData의 clearMissionList에 추가,
    /// 필수 미션이면 mandatoryMission에 반영하는 메소드
    /// </summary>
    /// <param name="missionCode">수행한 미션 코드(string)</param> 
    /// <param name="grade">'n'학년의 n 값(int)</param>
    /// <param name="missionLabel">수행한 미션 라벨(string)</param> 
    public void AddClearMission(string missionCode, int grade, string missionLabel)
    {
        string key = grade.ToString() + "학년";
        if (missionCode.StartsWith("Main"))
        {
            int missionIndex = missionCode[missionCode.Length - 1] - '0' - 1;
            mandatoryMission[key][missionIndex] = true;

        }
        clearMissionList[key].Add(missionLabel);
        OnDataChanged?.Invoke(); // 미션 리스트가 변경될 때 OnDataChanged 호출
    }


}


public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "GameManager";
                    instance = go.AddComponent<GameManager>();
                    instance.gameObject.AddComponent<LoadScene>(); // 씬 로드 기능도 가지고 있기
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            playerData = new PlayerData();
            Debug.Log("GameManager Awake");
            DataManager.LoadPlayerData();
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(gameObject);
    }
    #endregion

    public string nowMissionCode; // 현재 미션 정보
    public PlayerData playerData;



    /// <summary>
    /// 미션 코드 설정하기 (AR 인증 화면으로 이동 시에 호출)
    /// </summary>
    /// <param name="code"></param>
    public void SetMissionCode(string code)
    {
        nowMissionCode = code;
    }

    #region Status Up and Down
    /// <summary>
    /// num으로 학년 바꾸기 (1~4)
    /// </summary>
    /// <param name="num"></param>
    public void ChangeGrade(int num)
    {
        playerData.status.Grade = num;
    }

    public void IntelligenceStatusUpDown(int num)
    {

        playerData.status.Intelligence += num;
    }
    public void AttractivenessStatusUpDown(int num)
    {
        playerData.status.Attractiveness += num;
    }
    public void HealthStatusUpDown(int num)
    {
        playerData.status.Health += num;
    }
    #endregion



    /// <summary>
    /// 현재 필수 미션 리스트를 모두 수행했는지의 여부를 확인할 수 있는 메소드
    /// </summary>
    /// <returns>현재 학년의 필수 미션을 모두 클리어했다면 true, 아니면 false 반환</returns>
    public bool IsCompleteAllMandatoryMissions()
    {
        string key = playerData.status.Grade + "학년";
        if (playerData.mandatoryMission.TryGetValue(key, out bool[] currentMandatoryMissions))
        {
            foreach (bool flag in currentMandatoryMissions)
            {
                if (flag == false)
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }

    }
}
