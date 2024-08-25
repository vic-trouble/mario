using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : Unit
{
    public float RUN_SPEED = 2.5f;      // in tiles
    public float WALK_SPEED = 0.1f;     // in tiles

    public GameObject boomerangObject;
    public bool hasBoomerang = false;

    public AudioClip sfxJump_;
    public AudioClip sfxDie_;

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

                PlaySFX(sfxDie_);
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

                PlaySFX(sfxJump_);
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

    private new void Update()
    {
        base.Update();
        Animate();
    }

    public bool HasBoomerang()
    {
        return hasBoomerang;
    }

    public void ThrowBoomerang()
    {
        if (!HasBoomerang())
        {
            return;
        }

        hasBoomerang = false;

        var boomerang = Instantiate(boomerangObject).GetComponent<Boomerang>();
        var displacement = new Vector3(GetDirection(), 0.5f, 0);
        boomerang.transform.position = gameObject.transform.position + displacement;
        boomerang.Throw(GetDirection());
    }

    public void CollectBoomerang()
    {
        hasBoomerang = true;
    }
}
