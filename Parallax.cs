using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length, startposition;
    public Transform targetPos;
    [Header("배경 이동 속도")]
    public float parallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        startposition = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float temp = (targetPos.position.x * (1 - parallaxEffect));
        float dist = (targetPos.position.x * parallaxEffect);

        transform.position = new Vector3(startposition + dist, transform.position.y, transform.position.z);

        if (temp > startposition + length)      startposition += length;
        else if (temp < startposition - length) startposition -= length;

    }
}
