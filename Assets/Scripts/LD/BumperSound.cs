using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumperSound : MonoBehaviour
{
    private AudioSource bumbSound;

    private void Start()
    {
        bumbSound = gameObject.GetComponent<AudioSource>();
    }

    public void PlaySoundBump()
    {
        if (!bumbSound.isPlaying)
            bumbSound.Play();
    }
}
