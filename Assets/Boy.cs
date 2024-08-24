using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : Unit
{
    public float RUN_SPEED = 2.5f;      // in tiles
    public float WALK_SPEED = 0.1f;     // in tiles

    private void Start()
    {
        groundCheck_.GetComponent<GroundCheck>().SetTriggerEnterAction(HandleGroundCheckCollision);
    }

    private void HandleGroundCheckCollision(Collider2D collider)
    {
        CollisionManager.Instance().HandleBoyJumpsOn(this, collider.gameObject);
    }

    private void Animate()
    {
        var animator = gameObject.GetComponent<Animator>();
        if (IsDead())
        {
            if (!animator.GetBool("die"))
            {
                animator.SetBool("die", true);
                animator.SetBool("walk", false);
                animator.SetBool("run", false);
                animator.SetBool("jump up", false);
                animator.SetBool("fall down", false);
            }
            return;
        }

        var body = gameObject.GetComponent<Rigidbody2D>();
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
        Animate();
    }
}
