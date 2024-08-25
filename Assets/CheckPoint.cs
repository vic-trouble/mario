using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : ActiveObject
{
    public AudioClip sfxWin;
    private bool hasWin = false;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        CollisionManager.Instance().HandleCheckPointReached(this, collider.gameObject);
    }

    public void Win()
    {
        if (!hasWin)
        {
            PlaySFX(sfxWin);
            hasWin = true;
        }
    }
}
