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
    private AudioSource bombAudio;
    [SerializeField]
    private AudioSource bossAudio;
    [SerializeField]
    private AudioClip attackclip;
    [SerializeField]
    private AudioClip bombclip;
    public AudioMixer mixer;

    [SerializeField]
    private AudioClip toturialBgAudio;
    [SerializeField]
    private AudioClip dungenBgAudio;
    [SerializeField]
    private AudioClip bossBgAudio;
    [SerializeField]
    private AudioClip meleeClip;

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

    public void BombSound()
    {
        bombAudio.clip = bombclip;
        bombAudio.Play();
    }

    public void MeleeSound()
    {
        bossAudio.clip = meleeClip;
        bossAudio.Play();
    }

    public void SetTutorialAudio()
    {
        bgAudio.clip = toturialBgAudio;
        bgAudio.Play();
    }

    public void SetDungenAudio()
    {
        bgAudio.clip = dungenBgAudio;
        bgAudio.Play();
    }

    public void SetBossAudio()
    {
        bgAudio.clip = bossBgAudio;
        bgAudio.Play();
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
