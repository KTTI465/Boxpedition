using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class StageSelectBGM : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    public AudioClip bgm;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        //Component‚ğæ“¾
        audioSource = GetComponent<AudioSource>();

        //BGM‚ğÄ¶‚·‚é
        audioSource.PlayOneShot(bgm);
    }

    // Update is called once per frame
    void Update()
    {
        //audioMixer‚É‘ã“ü
        audioMixer.SetFloat("BGM", PlayerPrefs.GetFloat("BGM"));
    }
}