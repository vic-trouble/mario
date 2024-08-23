using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : MonoBehaviour
{
    public GameObject groundCheck_;

    public float SPEED_IMPULSE = 800;
    public float AIRBORNE_SPEED_FACTOR = 0.5f;
    public float JUMP_IMPULSE = 600;
    public float RUN_SPEED = 2.5f;      // in tiles
    public float WALK_SPEED = 0.1f;     // in tiles

    private int direction_ = 1;

    public bool IsAirBorne()
    {
        var gcheck = groundCheck_.GetComponent<GroundCheck>();
        return !gcheck.GetHit();
    }

    public void Move(int direction, float deltaTime)
    {
        // apply movement
        direction_ = direction;
        float speedFactor = IsAirBorne() ? AIRBORNE_SPEED_FACTOR : 1;

        var body = gameObject.GetComponent<Rigidbody2D>();
        body.AddForce(new Vector2(direction * SPEED_IMPULSE * speedFactor * deltaTime, 0));

        // turn to the side of movement
        body.transform.localScale = new Vector3(direction, 1, 1);
    }

    public void Jump()
    {
        // no fly mode
        if (IsAirBorne())
        {
            return;
        }

        // jump
        var body = gameObject.GetComponent<Rigidbody2D>();
        body.AddForce(new Vector2(0, JUMP_IMPULSE));
    }

    private void Animate()
    {
        var body = gameObject.GetComponent<Rigidbody2D>();
        var animator = gameObject.GetComponent<Animator>();
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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Animate();
    }
}
