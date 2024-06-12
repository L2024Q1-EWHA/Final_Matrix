using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    bool playingTutorial;
    GameObject[] tutoObjects;
    int tutoIndex;

    // Start is called before the first frame update
    void Start()
    {
        // 튜토리얼 플레이 여부 확인
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name))
        {
            playingTutorial = false;
            return;
        }
        else
        {
            playingTutorial = true;
            tutoObjects = new GameObject[transform.childCount];
            for (int i=0; i<transform.childCount; i++)
            {
                tutoObjects[i] = transform.GetChild(i).gameObject;
            }
            tutoIndex = 0;
            tutoObjects[tutoIndex].SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (playingTutorial)
            {
                if(tutoIndex < transform.childCount-1)
                {
                    tutoObjects[tutoIndex].SetActive(false);
                    tutoIndex++;
                    tutoObjects[tutoIndex].SetActive(true);
                }
                else
                {
                    tutoObjects[tutoIndex].SetActive(false);
                    playingTutorial = false;
                    PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
                }
            }
        }
    }
}
