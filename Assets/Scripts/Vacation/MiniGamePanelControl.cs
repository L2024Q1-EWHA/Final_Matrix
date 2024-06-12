using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGamePanelControl : MonoBehaviour
{
    [SerializeField] private MiniGameControl miniGameControl;

    private void OnEnable()
    {
        miniGameControl.ResetFirePosition();
        miniGameControl.StopMove = false;
    }
    private void OnDisable()
    {
        miniGameControl.StopMove = true;
    }
}
