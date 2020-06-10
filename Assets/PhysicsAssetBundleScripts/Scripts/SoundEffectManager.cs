using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundEffectManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioClip grabSound;
    public AudioClip pauseSound;
    public AudioClip strikeSound;
    public AudioClip placingSound;
    AudioSource audioSource;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGrabSound()
    {
        GetComponent<AudioSource>().clip = grabSound;
        audioSource.Play();
    }

    public void PlayPauseSound()
    {
        GetComponent<AudioSource>().clip = pauseSound;
        audioSource.Play();
    }

    public void PlayStrikeSound()
    {
        GetComponent<AudioSource>().clip = strikeSound;
        audioSource.Play();
    }

    public void PlayPlacingSound()
    {
        GetComponent<AudioSource>().clip = placingSound;
        audioSource.Play();
    }
}
