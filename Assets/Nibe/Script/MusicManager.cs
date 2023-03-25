using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static Unity.VisualScripting.Member;


// BGMÇ‚SEÇÃê›íË 

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] AudioSource bgmAudioSource;
    [SerializeField] AudioSource seAudioSource;

    [SerializeField] AudioClip bgm1;

    [SerializeField] AudioClip se1;


    public void SetBGM()
    {
        audioMixer.SetFloat("BGM", PlayerPrefs.GetFloat("BGM"));
    }

    public void SetSE()
    {
        audioMixer.SetFloat("SE", PlayerPrefs.GetFloat("SE"));
    }

    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }

    public void StopSE()
    {
        seAudioSource.Stop();
    }

    public void PlayBGM1()
    {
        bgmAudioSource.PlayOneShot(bgm1);
    }

    public void PlaySE1()
    {
        seAudioSource.PlayOneShot(se1);
    }
}