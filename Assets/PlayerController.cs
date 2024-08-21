using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject character_;
    public float SPEED = 300;

    void FixedUpdate()
    {
        var body = character_.GetComponent<Rigidbody2D>();

        int direction = 0;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            body.AddForce(new Vector2(SPEED * Time.deltaTime, 0));
            direction = 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            body.AddForce(new Vector2(-SPEED * Time.deltaTime, 0));
            direction = -1;
        }

        //body.transform.localScale = new Vector3(direction, 1, 1);
    }
}
