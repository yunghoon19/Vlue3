using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // 따라갈 오브젝트의 위치값
    public Transform target;

    // 카메라 이동속도
    public float cameraSpeed;

    // 카메라 이동 범위 Gizemo 값
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

        // 카메라 이동
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * cameraSpeed);

        // 카메라 이동 최소/최댓값 ( 카메라 이동 범위 제한 )
        float lx = size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + center.x, lx + center.x);

        float ly = size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + center.y, ly + center.y );

        transform.position = new Vector3(clampX, clampY, -10f);
    }
}
