using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject character_;
    public GameObject groundCheck_;     // TODO: encapsulate in the unit

    public float SPEED = 300;
    public float AIRBORNE_SPEED_FACTOR = 0.5f;
    public float JUMP_HEIGHT = 300;
    public float RUN_SPEED = 300;
    public float WALK_SPEED = 100;
    private bool jumpingAction_ = false;

    private bool IsAirBorne()
    {
        var gcheck = groundCheck_.GetComponent<GroundCheck>();
        return !gcheck.GetHit();
    }

    void FixedUpdate()
    {
        var body = character_.GetComponent<Rigidbody2D>();

        // apply movement
        int direction = 1;  // TODO: encapsulate in Unit class
        float speedFactor = IsAirBorne() ? AIRBORNE_SPEED_FACTOR : 1;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            body.AddForce(new Vector2(SPEED * speedFactor * Time.deltaTime, 0));
            direction = 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            body.AddForce(new Vector2(-SPEED * speedFactor * Time.deltaTime, 0));
            direction = -1;
        }

        // turn to the side of movement
        body.transform.localScale = new Vector3(direction, 1, 1);

        // animate
        var animator = character_.GetComponent<Animator>();
        if (IsAirBorne())
        {
            if (body.velocity[1] > 0 && !animator.GetBool("jump up"))
            {
                animator.SetBool("jump up", true);
                animator.SetBool("walk", false);
                animator.SetBool("run", false);
            }
            else if (body.velocity[1] < 0 && !animator.GetBool("fall down"))
            { 
                animator.SetBool("jump up", false);
                animator.SetBool("fall down", true);
                animator.SetBool("walk", false);
                animator.SetBool("run", false);
            }
        }
        else
        {
            float speed = Mathf.Abs(body.velocity[0]);
            if (speed >= RUN_SPEED)
            {
                animator.SetBool("walk", false);
                animator.SetBool("run", true);
                animator.SetBool("jump up", false);
                animator.SetBool("fall down", false);
            }
            else if (speed >= WALK_SPEED)
            {
                animator.SetBool("walk", true);
                animator.SetBool("run", false);
                animator.SetBool("jump up", false);
                animator.SetBool("fall down", false);
            }
            else
            {
                animator.SetBool("walk", false);
                animator.SetBool("run", false);
                animator.SetBool("jump up", false);
                animator.SetBool("fall down", false);
            }
        }
    }

    void Update()
    {
        var body = character_.GetComponent<Rigidbody2D>();

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (/*!isAirborne && */ !jumpingAction_)
            {
                jumpingAction_ = true;
                body.AddForce(new Vector2(0, JUMP_HEIGHT));
            }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            jumpingAction_ = false;
        }
    }
}
