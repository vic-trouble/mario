using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject character_;
    public float SPEED = 300;
    public float RUN_SPEED = 300;
    public float WALK_SPEED = 100;

    void FixedUpdate()
    {
        var body = character_.GetComponent<Rigidbody2D>();

        int direction = 1;
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

        body.transform.localScale = new Vector3(direction, 1, 1);

        var animator = character_.GetComponent<Animator>();
        float speed = Mathf.Abs(body.velocity[0]);
        if (speed >= RUN_SPEED)
        {
            animator.SetBool("walk", false);
            animator.SetBool("run", true);
        }
        else if (speed >= WALK_SPEED)
        {
            animator.SetBool("walk", true);
            animator.SetBool("run", false);
        }
        else
        {
            animator.SetBool("walk", false);
            animator.SetBool("run", false);
        }
    }
}
