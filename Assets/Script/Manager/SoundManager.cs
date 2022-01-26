using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource bgAudio;
    public AudioMixer mixer;

    public void ChangeBg(AudioClip clip)
    {
        bgAudio.Stop();
        bgAudio.clip = clip;
        bgAudio.Play();
    }
    public void SetBackGroundMusicVolume(float volume)
    {
        mixer.SetFloat("BackGroundAudio",Mathf.Log10(volume)*40);
    }
    public void SetVFXMusicVolume(float volume)
    {
        mixer.SetFloat("VFX",Mathf.Log10(volume)*40);
    }
}
