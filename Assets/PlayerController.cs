using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject character_;

    private bool jumpingAction_ = false;

    void FixedUpdate()
    {
        var boy = character_.GetComponent<Boy>();

        if (Input.GetKey(KeyCode.RightArrow))
        {
            boy.Move(1, Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            boy.Move(-1, Time.deltaTime);
        }
    }

    void Update()
    {
        var body = character_.GetComponent<Rigidbody2D>();

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (!jumpingAction_)
            {
                var boy = character_.GetComponent<Boy>();
                boy.Jump();

                jumpingAction_ = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            jumpingAction_ = false;
        }
    }
}
