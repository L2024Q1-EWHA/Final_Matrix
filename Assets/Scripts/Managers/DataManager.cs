using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Newtonsoft.Json;


// 저장/불러오기 처리
public static class DataManager
{
    public static string filePath;
    public static string playerDataFile = "PlayerData.json";
    public static string playerDataInitFile = "PlayerData_Init";
    public static void Save()
    {



    }


    public static void Load() { }


    public static void SavePlayerData()
    {
        //직렬화
        string toJsonData = JsonConvert.SerializeObject(GameManager.Instance.playerData);

        filePath = Path.Combine(Application.persistentDataPath, playerDataFile);

        File.WriteAllText(filePath, toJsonData);
        Debug.Log($"로컬에 playerData 저장 완료");

    }


    public static void LoadPlayerData()
    {
        filePath = Path.Combine(Application.persistentDataPath, playerDataFile);
        Debug.Log(filePath);
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            Debug.Log("플레이어 데이터 불러오기");

            GameManager.Instance.playerData = JsonConvert.DeserializeObject<PlayerData>(jsonString);
        }
        else
        {
            Debug.Log($"로컬에 저장된 데이터 없음. 초기화 메소드 호출");
            InitializePlayerData();
        }
    }

    public static void InitializePlayerData()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(playerDataInitFile);
        if (jsonFile != null)
        {
            GameManager.Instance.playerData = JsonConvert.DeserializeObject<PlayerData>(jsonFile.text);
            SavePlayerData();
        }
        else
        {
            Debug.Log($"Can't find init file : {playerDataInitFile}");
        }

    }

    public static void Reset()
    {
        filePath = Path.Combine(Application.persistentDataPath, playerDataFile);
        File.Delete(filePath);

        InitializePlayerData();
    }

}
