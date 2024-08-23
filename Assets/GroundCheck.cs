using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool hit_ = false;

    public bool GetHit()
    {
        return hit_;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        hit_ = true;
        //Debug.Log("hit");
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        hit_ = false;
        //Debug.Log("not hit");
    }
}
