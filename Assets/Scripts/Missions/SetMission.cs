using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMission : MonoBehaviour
{
    public void SetMissionCode(string code)
    {
        GameManager.Instance.nowMissionCode = code;
    }
}
