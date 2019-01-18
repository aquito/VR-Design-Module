using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSelector : MonoBehaviour
{
    public AudioClip[] audioClips;

    public AudioSource audioSource;
    
    
    public void PlaySFX(string clipName)
    {
        if(clipName == "ready")
        {
            // audioSource.AudioClip = audioClips[0];
            // audioSource.Play();
            // audioClips[].

        } else if(clipName == "fail")
        {

        } else if(clipName == "win")
        {

        }
    }
}
