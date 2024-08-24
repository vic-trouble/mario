using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObject : MonoBehaviour
{
    protected void PlaySFX(AudioClip sfx)
    {
        if (sfx)
        {
            var audio = gameObject.GetComponent<AudioSource>();
            audio.PlayOneShot(sfx);
        }
    }
}
