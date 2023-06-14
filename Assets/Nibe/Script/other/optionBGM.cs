using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class optionBGM : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    public AudioClip bgm;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        //Componentを取得
        audioSource = GetComponent<AudioSource>();

        //BGMを再生する
        audioSource.PlayOneShot(bgm);
    }

    // Update is called once per frame
    void Update()
    {
        //audioMixerに代入
        audioMixer.SetFloat("BGM", PlayerPrefs.GetFloat("BGM"));
    }
}
