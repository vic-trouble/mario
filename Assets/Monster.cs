using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Unit
{
    public GameObject faceHitCheck;

    public bool GetFaceHit()
    {
        return faceHitCheck.GetComponent<GroundCheck>().GetHit();
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
    }

    private void Animate()
    {
        var animator = gameObject.GetComponent<Animator>();

        if (IsDead())
        {
            if (!animator.GetBool("die"))
            {
                animator.SetBool("die", true);
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
                animator.SetBool("run", false);
            }
            else if (body.velocity[1] < 0 && !animator.GetBool("fall down"))
            {
                animator.SetBool("jump up", false);
                animator.SetBool("fall down", true);
                animator.SetBool("run", false);
            }
        }
        else
        {
            float speed = Mathf.Abs(body.velocity[0]);
            if (speed >= 0.01)
            {
                animator.SetBool("run", true);
                animator.SetBool("jump up", false);
                animator.SetBool("fall down", false);
            }
            else
            {
                animator.SetBool("run", false);
                animator.SetBool("jump up", false);
                animator.SetBool("fall down", false);
            }
        }
    }

}
