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
        //Component���擾
        audioSource = GetComponent<AudioSource>();

        //BGM���Đ�����
        audioSource.PlayOneShot(bgm);
    }

    // Update is called once per frame
    void Update()
    {
        //audioMixer�ɑ��
        audioMixer.SetFloat("BGM", PlayerPrefs.GetFloat("BGM"));
    }
}