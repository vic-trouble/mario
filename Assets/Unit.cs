using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject groundCheck_;

    public float SPEED_IMPULSE = 800;
    public float AIRBORNE_SPEED_FACTOR = 0.5f;
    public float JUMP_IMPULSE = 600;

    private bool isDead = false;
    private int direction_ = 1;

    public void Die()
    {
        isDead = true;

        // unit no longer collides with the world
        var collider = gameObject.GetComponent<Collider2D>();
        collider.enabled = false;
        var body = gameObject.GetComponent<Rigidbody2D>();
        body.simulated = false;
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
        body.transform.localScale = new Vector3(direction, 1, 1);
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

    protected void PlaySFX(AudioClip sfx)
    {
        if (sfx)
        {
            var audio = gameObject.GetComponent<AudioSource>();
            audio.PlayOneShot(sfx);
        }
    }

}
