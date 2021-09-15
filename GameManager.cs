using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager inst;

    public bool PlayerMove;
    // 플레이어의 위치정보를 담고있다
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
