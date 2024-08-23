using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    public float CAMERA_SPEED = 1;

    void Update()
    {
        if (target)
        {
            var heroPos = target.transform.position + offset;
            var t = Time.deltaTime * CAMERA_SPEED;
            transform.position = new Vector3(
                Mathf.Lerp(transform.position.x, heroPos.x, t),
                Mathf.Lerp(transform.position.y, heroPos.y, t), 
                transform.position.z
            );
        }
    }
}
