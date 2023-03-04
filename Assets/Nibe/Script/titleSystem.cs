using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class titleSystem : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    public AudioClip titleSE;
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

        if (Input.GetKey(KeyCode.S))
        {
            StartGame();  //�Q�[���X�^�[�g
        }

        if (Input.GetKey(KeyCode.O))
        {
            OpenOption();  //�I�v�V�������J��
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            QuitGame();  //�Q�[���I��
        }
    }

    //�X�^�[�g�{�^��������������s����
    public void StartGame()
    {
        audioSource.PlayOneShot(titleSE);
        SceneManager.LoadScene("Stage1_Image");
    }

    //�I�v�V�����{�^��������������s����
    public void OpenOption()
    {
        audioSource.PlayOneShot(titleSE);
        SceneManager.LoadScene("optionScene");
    }

    //�Q�[���I���{�^��������������s����
    public void QuitGame()
    {
        audioSource.PlayOneShot(titleSE);
        Application.Quit();
    }
}
