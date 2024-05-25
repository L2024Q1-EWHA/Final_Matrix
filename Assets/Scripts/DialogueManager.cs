using UnityEngine;
using UnityEngine.UI; // UI ������Ʈ ���
using TMPro; // TextMeshPro ���
using UnityEngine.SceneManagement; // �� ����

public class DialogueManager : MonoBehaviour
{
    public FadeController fadeController;
    public TMP_Text scriptText; // ��縦 ǥ���ϱ� ���� ����

    public string[] dialogueLines; // ��縦 ������ �迭
    public int currentLine = 0; // ���� ����� ��ġ

    public void DisplayNextLine()
    {
        if (currentLine < dialogueLines.Length)
        {
            scriptText.text = dialogueLines[currentLine];
            currentLine++;
        }
        else // ��� ��簡 ��µ� ���
        {
            fadeController.FadeOutAndLoadScene("HomeScene"); // ���� ������ ��ȯ
        }
    }
}