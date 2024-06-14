using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MissionListSceneManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gradeText;
    [SerializeField] string gradeMission; // ?학년 미션

    // Start is called before the first frame update
    void Start()
    {
        gradeText.text = GameManager.Instance.playerData.status.Grade.ToString() + gradeMission;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
