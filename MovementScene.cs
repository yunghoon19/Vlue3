using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScene : MonoBehaviour
{
    /* 이동할 씬 이름 저장 */
    public string sceneName;

    /* 버튼을 누를때 실행되는 매소드 */
    public void In_Button()
    {
        /* LoadingSceneController.LoadScene() 함수에 이동할 씬 이름을 넘겨줌 */
        LoadingSceneController.LoadScene(sceneName);
    }
}
