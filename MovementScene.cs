using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScene : MonoBehaviour
{
    /* �̵��� �� �̸� ���� */
    public string sceneName;

    /* ��ư�� ������ ����Ǵ� �żҵ� */
    public void In_Button()
    {
        /* LoadingSceneController.LoadScene() �Լ��� �̵��� �� �̸��� �Ѱ��� */
        LoadingSceneController.LoadScene(sceneName);
    }
}
