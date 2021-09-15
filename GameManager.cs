using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public bool PlayerMove;
    // �÷��̾��� ��ġ������ ����ִ�
    private Transform playerTransform;

    private void Awake()
    {
        if (inst)
        {
            Destroy(gameObject);
            return;
        }
        inst = this;
        DontDestroyOnLoad(gameObject);
    }
    public static GameManager Inst
    {
        get
        {
            if (null == inst)
            {
                return null;
            }
            return inst;
        }
    }

    public void SetPlayerTransform(Transform _playerTransform)
    {
        playerTransform = _playerTransform;
    }

    public Transform GetPlayerTransform()
    {
        return playerTransform;
    }
}
