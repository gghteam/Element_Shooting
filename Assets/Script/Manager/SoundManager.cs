using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField]
    private AudioSource bgAudio;
    [SerializeField]
    private AudioSource attckAudio;
    [SerializeField]
    private AudioClip attackclip;
    public AudioMixer mixer;

    private void Awake() {
        if(Instance != null)
        {
            Debug.LogError("Multiple SoundManager is running");
        }
        Instance = this;
    }
    public void AttackSound()
    {
        attckAudio.clip = attackclip;
        attckAudio.Play();
    }
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
