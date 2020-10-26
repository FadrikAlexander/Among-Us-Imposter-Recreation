using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] AudioSource audioSourceWalk;
    public void PlayWalkingSound()
    {
        if (!audioSourceWalk.isPlaying)
        {
            audioSourceWalk.Play();
        }
    }
    public void StopWalking()
    {
        audioSourceWalk.Stop();
    }

    //-----------------------

    [SerializeField] AudioClip VentIn;
    [SerializeField] AudioSource audioSourceEffects;

    public void PlayVent()
    {
        audioSourceEffects.clip = VentIn;
        audioSourceEffects.Play();
    }
}
