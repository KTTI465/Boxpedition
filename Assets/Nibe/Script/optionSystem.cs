using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class optionSystem : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    public AudioClip optionSE;
    AudioSource audioSource;

    public bool DontDestroyEnabled = true;


    // Start is called before the first frame update
    void Start()
    {
        // Component���擾
        audioSource = GetComponent<AudioSource>();

        if (DontDestroyEnabled)
        {
            // Scene��J�ڂ��Ă��I�u�W�F�N�g�������Ȃ��悤�ɂ���
            DontDestroyOnLoad(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //audioMixer�ɑ��
        audioMixer.SetFloat("SE", PlayerPrefs.GetFloat("SE"));

        if (Input.GetKey(KeyCode.Escape))
        {
            ReturnTitle();  //�^�C�g���ɖ߂�
        }
    }

    //�@�^�C�g���֖߂�{�^��������������s����
    public void ReturnTitle()
    {
        audioSource.PlayOneShot(optionSE);
        SceneManager.LoadScene("titleScene");
    }
}
