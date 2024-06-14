using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] private Slider healthProgressBar;
    [SerializeField] private Slider attractivenessProgressBar;
    [SerializeField] private Slider intelligenceProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        //status 수치 반영
        healthProgressBar.value = GameManager.Instance.playerData.status.Health;
        attractivenessProgressBar.value = GameManager.Instance.playerData.status.Attractiveness;
        intelligenceProgressBar.value = GameManager.Instance.playerData.status.Intelligence;
    }

}
