using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip winSound, moveSound;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayWinSound(){
        audioSource.PlayOneShot(winSound);
    }

    public void PlayMoveSound(){
        audioSource.PlayOneShot(moveSound);
    }
}
