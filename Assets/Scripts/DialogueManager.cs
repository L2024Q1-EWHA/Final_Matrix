using UnityEngine;
using UnityEngine.UI; // UI 컴포넌트 사용
using TMPro; // TextMeshPro 사용
using UnityEngine.SceneManagement; // 씬 관리

using System.Collections; //coroutine 위해 추가

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue")]
    public FadeController fadeController;
    public TMP_Text scriptText; // 대사를 표시하기 위한 변수
    [TextArea]
    public string[] dialogueLines; // 대사를 저장할 배열
    public int currentLine = 0; // 현재 대사의 위치


    [Header("Typing Effect")]
    private bool isTyping = false;
    private bool isDoneTyping = true;
    private bool stopTyping = false;
    private float waitTime = 0.03f;


    private void Start()
    {
        isTyping = false;
        isDoneTyping = true;
        stopTyping = false;

        DisplayNextLine();
    }

    public void DisplayNextLine()
    {
        //타이핑 효과 추가(0617)
        if (!isDoneTyping)
        {
            stopTyping = true;
        }
        else
        {
            if (currentLine < dialogueLines.Length)
            {
                //scriptText.text = dialogueLines[currentLine]; //타이핑 효과를 위해 주석 처리(0617)
                StartCoroutine(TypingText(dialogueLines[currentLine]));
                currentLine++;
            }
            else // 모든 대사가 출력된 경우
            {
                Debug.Log("인트로 완료. 첫 시작 flag 변경");
                GameManager.Instance.playerData.FirstPlay = false;
                fadeController.FadeOutAndLoadScene("HomeScene"); // 다음 씬으로 전환
            }
        }


    }



    public IEnumerator TypingText(string text)
    {
        if (isTyping) yield break; //중복 호출 방지

        isTyping = true;
        isDoneTyping = false;
        scriptText.text = ""; //초기화
        for (int i = 0; i < text.Length && !stopTyping; i++)
        {
            scriptText.text += text[i];
            //잠시 대기
            yield return new WaitForSeconds(waitTime);
        }
        if (!scriptText.text.Equals(text))
        {
            scriptText.text = text;
        }

        stopTyping = false;
        isDoneTyping = true;
        isTyping = false;
    }

}