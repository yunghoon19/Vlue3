using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneController : MonoBehaviour
{
    /* 다음 씬 이름 저장 */
    static string nextScene;

    /* 진행바 이미지 변수 */
    [SerializeField]
    Image progressBar;

    /* 로딩씬을 불러오는 함수 | 매개변수 : 이동할 씬 이름 */ 
    public static void LoadScene(string _sceneName)
    {
        nextScene = _sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    /* 코루틴 스타트 */
    private void Start()
    {
        StartCoroutine(LoadSceneProgress());
    }

    /* nextScene을 로드시키는 코루틴 함수 */
    IEnumerator LoadSceneProgress()
    {
        /* nextScene을 비동기 방식으로 로드 */
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);

        /* nextScene 로드를 기다림 true로 하면 바로 로드 */
        op.allowSceneActivation = false;

        /* 시간을 재줄 타이머 변수 */
        float timer = 0f;

        /* while( NOT 씬 로딩 완료 ) */
        while (!op.isDone)
        {
            yield return null;

            /* 씬 로딩이 0.9보다 낮을 시 진행바를 0.3까지 로드 */
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 0.3f, Time.deltaTime);
            }
            else
            {
                /* 진행바 속도 조절 | 뒤에 나눈 숫자값을 늘리면 진행바가 느려짐 */
                timer += Time.unscaledDeltaTime / 4;

                /* 진행바를 0.3 에서 1 까지 늘림 */
                progressBar.fillAmount = Mathf.Lerp(0.3f, 1f, timer);

                /* 진행바가 1까지 찼다면 다음 씬을 로드 : op.allowSceneActivation = true; */
                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
