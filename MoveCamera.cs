using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // ���� ������Ʈ�� ��ġ��
    public Transform target;

    // ī�޶� �̵��ӵ�
    public float cameraSpeed;

    // ī�޶� �̵� ���� Gizemo ��
    public Vector2 center;
    public Vector2 size;

    float height;
    float width;

    void Awake()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }

    void LateUpdate()
    {

        // ī�޶� �̵�
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * cameraSpeed);

        // ī�޶� �̵� �ּ�/�ִ� ( ī�޶� �̵� ���� ���� )
        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y );

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
