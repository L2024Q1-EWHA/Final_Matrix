using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class FadeController : MonoBehaviour
{
    public Image fadeImage; // ���̵忡 ����� Image ������Ʈ
    public float fadeSpeed = 3f; // ���̵� �ӵ�

    // ���̵� ���� ����
    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    // ���̵� �ƿ��� �����ϰ�, ���� ��ȯ
    public void FadeOutAndLoadScene(string sceneName)
    {
        StartCoroutine(FadeOutCoroutine(sceneName));
    }

    IEnumerator FadeInCoroutine()
    {
        // ������ 1���� 0���� ����
        float alpha = fadeImage.color.a;
        while (alpha > 0f)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
    }

    IEnumerator FadeOutCoroutine(string sceneName)
    {
        // ������ 0���� 1�� ����
        float alpha = fadeImage.color.a;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeSpeed;
            fadeImage.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        // �� ��ȯ
        SceneManager.LoadScene(sceneName);
    }
}
