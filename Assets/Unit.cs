using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public GameObject groundCheck_;

    public float SPEED_IMPULSE = 800;
    public float AIRBORNE_SPEED_FACTOR = 0.5f;
    public float JUMP_IMPULSE = 600;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
