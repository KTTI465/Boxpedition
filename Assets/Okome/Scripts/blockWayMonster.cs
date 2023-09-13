using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockWayMonster : MonoBehaviour
{
    [SerializeField] GameObject player;
    CharacterController characterController;
    Animator playerAnimator;
    Rigidbody playerRb;

    [SerializeField] GameObject eventCamera;

    Animator monstersAnimator;

    //�v���C���[�����т��ĉ������Ă���Ƃ�
    bool isPlayerStepBack;

    //�C�x���g����������
    bool isEvent;

    //�����J������
    public bool hasOpenedWay;
    //�ԃq�[���[�����邩
    public bool existRedHero;

    //�C�x���g�������̃v���C���[�̈ʒu
    Vector3 inEventPlayerPosition;
    Vector3 stepBackPosition;

    [System.NonSerialized]
    public bool firstThreaten = false;

    [SerializeField] AudioClip monstersSound;
    AudioSource audioSource;
    void Start()
    {
        //�v���C���[�ƃ����X�^�[�̃R���|�[�l���g�̎擾
        characterController = player.GetComponent<CharacterController>();
        playerAnimator = player.GetComponent<Animator>();
        monstersAnimator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerRb = player.GetComponent<Rigidbody>();
        //�J�������L����
        eventCamera.SetActive(false);
    }

    void Update()
    {
        //�C�x���g��
        if (isEvent == true)
        {
            //���т��ĉ������Ă���Ƃ�
            if (isPlayerStepBack == true)
            {
                //�v���C���[�̌����̐ݒ�ƈړ�
                player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, Quaternion.Euler(0f, 180f, 0f), 900f * Time.deltaTime);
                player.transform.position = Vector3.MoveTowards(player.transform.position, stepBackPosition, 10f * Time.deltaTime);
                //�v���C���[�ɕ����A�j���[�V������������
                playerAnimator.SetBool("walk", true);
            }
            else
            {
                //�C�x���g���ɕ����Ă��Ȃ����ɕ����A�j���[�V���������Ȃ��悤�ɂ���
                playerAnimator.SetBool("walk", false);
            }

        }
    }

    public void StartEvent()
    {
        //�C�x���g���n�܂�Ƃ��Ƀv���C���[�̈ړ���s�ɂ��āA�ʒu��������Œ肵�ăJ������؂�ւ���
        isEvent = true;
        playerRb.velocity = Vector3.zero;
        characterController.enabled = false;
        //�L�����N�^�[�̌����̐ݒ�
        player.transform.eulerAngles = transform.up;
        inEventPlayerPosition = new Vector3(transform.position.x - 3f, player.transform.position.y, transform.position.z - 40f);
        stepBackPosition = inEventPlayerPosition - new Vector3(0f, 0f, 10f);
        player.transform.position = inEventPlayerPosition;
        characterController.StateProcessor.State = characterController.StateIdle;
        playerAnimator.SetBool("walk", false);
        eventCamera.SetActive(true);
    }

    public void MonstersSound()
    {
        Debug.Log("sound");
        audioSource.PlayOneShot(monstersSound);
    }

    public void PlayerStepBack()
    {
        isPlayerStepBack = true;
    }

    public void EndEvent()
    {
        //�C�x���g���I��������ɃJ������؂�ւ��A�ړ����ł���悤�ɂ���
        isEvent = false;
        characterController.enabled = true;
        eventCamera.SetActive(false);
        isPlayerStepBack = false;
    }

    public void OpenWay()
    {
        hasOpenedWay = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //�����J���Ă��Ȃ���
        if (hasOpenedWay == false)
        {
            if (other.gameObject == player.gameObject)
            {
                //�ԃq�[���[�����Ȃ��Ȃ�
                if (existRedHero == false)
                {
                    monstersAnimator.SetTrigger("threaten");
                    if (firstThreaten == false)
                    {
                        firstThreaten = true;
                    }
                }
                else
                {
                    monstersAnimator.SetTrigger("openWay");
                }
            }
        }
    }
}

