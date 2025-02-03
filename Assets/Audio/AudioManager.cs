using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public AudioSource windSounds;
    public AudioSource bGMusic;

    public AudioSource winFail;
    public AudioClip winSuccess;
    public AudioClip loseFail;

    public void StartWindSound()
    {
        windSounds.Play();
    }
    
    public void StopWindSound()
    {
        windSounds.Stop();
    }

    public void StartBGMusic()
    {
        bGMusic.Play();
    }
    
    public void StopBGMusic()
    {
        bGMusic.Stop();
    }

    public void PlayOneShotWin()
    {
        winFail.PlayOneShot(winSuccess);
    }
    
    public void PlayOneShotFail()
    {
        winFail.PlayOneShot(loseFail);
    }
}
