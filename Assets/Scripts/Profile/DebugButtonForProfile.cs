using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugButtonForProfile : MonoBehaviour
{
    [SerializeField] private ProfileManager profileManager;
    public void IntelligenceUp(int num)
    {
        GameManager.Instance.playerData.status.Intelligence += num;
        profileManager.RefreshUI();
    }
    public void AttractivenessUp(int num)
    {
        GameManager.Instance.playerData.status.Attractiveness += num;
        profileManager.RefreshUI();
    }
    public void HealthUp(int num)
    {
        GameManager.Instance.playerData.status.Health += num;
        // profileManager.SetStatusProgressBar();
        profileManager.RefreshUI();
    }

    public void ResetPlayerData()
    {
        DataManager.Reset();
        profileManager.RefreshUI();
    }
}
