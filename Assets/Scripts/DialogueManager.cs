using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;


public class DialogueManager : MonoBehaviour, IPointerClickHandler
{
    public TMP_Text textScripts; // TextScripts ������Ʈ�� �����ϱ� ���� ����
    public Image dialogueImage;
    public FadeController fadeController;

    // ��ȭ ������ ������ �迭
    private string[] dialogues = {
        "��, ��ǻ�Ͱ��а� 20�й� ��ȭ��.\n������ ��ٸ��� ��ٸ��� ������!\n�б��� 5�⵿���̳� �ٳ�����...\n������ ���� ������ ���� �ƾ�!\n�ų���~",
        "�׷��� �̰� ���� ���̾�!\nä��, �ʼ�����, �ʼ�����,\n���� ����, ���� ���� ���\n��ȭ���ڴ��б� ��ǻ�Ͱ��а�\n�л��̶�� ������� ��������� ���� �������ߴٱ�?!\n�׷� ���� ������ ���Ѵٱ�?!!",
        "����...\n1�г����� ���ư� �� �ִٸ�...\n������ ���ǵ� ���, ���赵 ġ��,\n������ �б� ��Ȱ�� ����ٵ�...!",
        "��, �׷��� �� ���� ����?"
    };

    public Sprite[] dialogueImages;
    private int currentDialogueIndex = 0; // ���� ��ȭ�� �ε���

    public void OnPointerClick(PointerEventData eventData)
    {
        currentDialogueIndex++; // ���� ��ȭ�� �̵�

        if (currentDialogueIndex < dialogues.Length)
        {
            // ���� ǥ���� ��ȭ�� ���������� ��ȭ ���� ������Ʈ
            textScripts.text = dialogues[currentDialogueIndex];
        }
        else
        {
            // ��� ��ȭ�� ������ ���̵� �ƿ��ϰ� ���ο� ������ ��ȯ
            fadeController.FadeOutAndLoadScene("HomeScene");
        }
    }

    void Start()
    {
        // �ʱ� ��ȭ ���� ����
        textScripts.text = dialogues[currentDialogueIndex];
        dialogueImage.sprite = dialogueImages[currentDialogueIndex];
    }
}
