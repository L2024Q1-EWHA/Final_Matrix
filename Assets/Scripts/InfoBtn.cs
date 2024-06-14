using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoBtn : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public void InfoBtnOnClick()
    {
        if (panel.activeSelf) panel.SetActive(false);
        else panel.SetActive(true);
    }
}
