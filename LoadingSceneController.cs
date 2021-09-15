using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    /* ���� �� �̸� ���� */
    static string nextScene;

    /* ����� �̹��� ���� */
    [SerializeField]
    Image progressBar;

    /* �ε����� �ҷ����� �Լ� | �Ű����� : �̵��� �� �̸� */ 
    public static void LoadScene(string _sceneName)
    {
        nextScene = _sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    /* �ڷ�ƾ ��ŸƮ */
    private void Start()
    {
        StartCoroutine(LoadSceneProgress());
    }

    /* nextScene�� �ε��Ű�� �ڷ�ƾ �Լ� */
    IEnumerator LoadSceneProgress()
    {
        /* nextScene�� �񵿱� ������� �ε� */
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);

        /* nextScene �ε带 ��ٸ� true�� �ϸ� �ٷ� �ε� */
        op.allowSceneActivation = false;

        /* �ð��� ���� Ÿ�̸� ���� */
        float timer = 0f;

        /* while( NOT �� �ε� �Ϸ� ) */
        while (!op.isDone)
        {
            yield return null;

            /* �� �ε��� 0.9���� ���� �� ����ٸ� 0.3���� �ε� */
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 0.3f, Time.deltaTime);
            }
            else
            {
                /* ����� �ӵ� ���� | �ڿ� ���� ���ڰ��� �ø��� ����ٰ� ������ */
                timer += Time.unscaledDeltaTime / 4;

                /* ����ٸ� 0.3 ���� 1 ���� �ø� */
                progressBar.fillAmount = Mathf.Lerp(0.3f, 1f, timer);

                /* ����ٰ� 1���� á�ٸ� ���� ���� �ε� : op.allowSceneActivation = true; */
                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
