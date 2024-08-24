using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Action<Collider2D> triggerEnterAction_ = (collider) => {};
    private bool hit_ = false;

    public void SetTriggerEnterAction(Action<Collider2D> triggerEnterAction)
    {
        triggerEnterAction_ = triggerEnterAction;
    }

    public bool GetHit()
    {
        return hit_;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        hit_ = true;
        triggerEnterAction_(collider);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        hit_ = false;
    }
}
