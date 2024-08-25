using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : ActiveObject
{
    public GameObject groundCheck_;

    public float SPEED_IMPULSE = 800;
    public float AIRBORNE_SPEED_FACTOR = 0.5f;
    public float JUMP_IMPULSE = 600;
    public float TERMINAL_SPEED = 5;

    private bool isDead = false;
    private int direction_ = 1;

    protected int GetDirection()
    {
        return direction_;
    }

    public void Die()
    {
        isDead = true;

        // unit no longer collides with the world
        var collider = gameObject.GetComponent<Collider2D>();
        collider.enabled = false;
        var body = gameObject.GetComponent<Rigidbody2D>();
        body.simulated = false;
    }

    protected void Respawn(Vector3 position)
    {
        isDead = false;

        gameObject.transform.position = position;

        var collider = gameObject.GetComponent<Collider2D>();
        collider.enabled = true;
        var body = gameObject.GetComponent<Rigidbody2D>();
        body.simulated = true;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public bool IsAirBorne()
    {
        var gcheck = groundCheck_.GetComponent<GroundCheck>();
        return !gcheck.GetHit();
    }

    public void Move(int direction, float deltaTime)
    {
        if (isDead)
        {
            return;
        }

        // apply movement
        direction_ = direction;
        float speedFactor = IsAirBorne() ? AIRBORNE_SPEED_FACTOR : 1;

        var body = gameObject.GetComponent<Rigidbody2D>();
        body.AddForce(new Vector2(direction * SPEED_IMPULSE * speedFactor * deltaTime, 0));

        // turn to the side of movement
        body.transform.localScale = new Vector3(Mathf.Abs(body.transform.localScale.x) * direction, body.transform.localScale.y, 1);
    }

    public void Jump()
    {
        if (isDead)
        {
            return;
        }

        // no fly mode
        if (IsAirBorne())
        {
            return;
        }

        // jump
        var body = gameObject.GetComponent<Rigidbody2D>();
        body.AddForce(new Vector2(0, JUMP_IMPULSE));
    }

    protected void Update()
    {
        var body = gameObject.GetComponent<Rigidbody2D>();
        if (!IsDead() && body.velocity[1] < -TERMINAL_SPEED)
        {
            Die();
        }
    }
}
