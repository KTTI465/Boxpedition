using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class stageSelectSystem : MonoBehaviour
{
    public GameObject stage1Text;
    public GameObject stage1Image;
    public GameObject stage2Text;
    public GameObject stage2Image;


    [SerializeField] AudioMixer audioMixer;

    public AudioClip stageSelectSE;
    AudioSource audioSource;

    public bool DontDestroyEnabled = true;


    int stageNum = 1;
    int moveCount = 0;
    bool moveLeft = false;
    bool moveRight = false;


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


        if (moveLeft == true)
        {
            if (moveCount <= 100)
            {
                if (stageNum == 1)
                {

                }
                else if (stageNum == 2)
                {
                    stage1Text.transform.position += new Vector3(10.0f, 0, 0);
                    stage1Image.transform.position += new Vector3(10.0f, 0, 0);
                    stage2Text.transform.position += new Vector3(10.0f, 0, 0);
                    stage2Image.transform.position += new Vector3(10.0f, 0, 0);
                }

                moveCount++;
            }
            else
            {
                moveCount = 0;
                stageNum--;
                moveLeft = false;
            }
        }

        if (moveRight == true)
        {
            if (moveCount <= 100)
            {
                if (stageNum == 1)
                {
                    stage1Text.transform.position -= new Vector3(10.0f, 0, 0);
                    stage1Image.transform.position -= new Vector3(10.0f, 0, 0);
                    stage2Text.transform.position -= new Vector3(10.0f, 0, 0);
                    stage2Image.transform.position -= new Vector3(10.0f, 0, 0);
                }
                else if (stageNum == 2)
                {

                }

                moveCount++;
            }
            else
            {
                moveCount = 0;
                stageNum++;
                moveRight = false;
            }
        }


        if (Input.GetKey(KeyCode.S))
        {
            StartGame();  //�Q�[���X�^�[�g
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            StageLeft();  //�����̃X�e�[�W�Ɉڂ�
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            StageRight();  //�E���̃X�e�[�W�Ɉڂ�
        }
    }


    //�X�^�[�g�{�^��������������s����
    public void StartGame()
    {
        if (moveLeft == false && moveRight == false)
        {
            if (stageNum == 1)
            {
                audioSource.PlayOneShot(stageSelectSE);
                SceneManager.LoadScene("Stage1_Image");
            }
            else if (stageNum == 2)
            {
                audioSource.PlayOneShot(stageSelectSE);
            }
        }
    }


    //�����̃{�^��������������s����
    public void StageLeft()
    {
        if (moveLeft == false && moveRight == false)
        {
            audioSource.PlayOneShot(stageSelectSE);

            if (stageNum != 1)
            {
                moveLeft = true;
            }
        }
    }


    //�E���̃{�^��������������s����
    public void StageRight()
    {
        if (moveLeft == false && moveRight == false)
        {
            audioSource.PlayOneShot(stageSelectSE);

            if (stageNum != 2)
            {
                moveRight = true;
            }
        }
    }
}