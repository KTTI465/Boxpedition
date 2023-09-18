using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterState;

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

    [SerializeField]
    private GameObject offScreenArrow;

    [SerializeField] AudioClip monstersSound;

    public MonsterStateProcessor MonsterStateProcessor = new();
    public MonsterStateIdle MonsterStateIdle = new();
    public MonsterStateRun MonsterStateRun = new();
    public MonsterStateJump MonsterStateJump = new();

    void Start()
    {
        //�v���C���[�ƃ����X�^�[�̃R���|�[�l���g�̎擾
        characterController = player.GetComponent<CharacterController>();
        playerAnimator = player.GetComponent<Animator>();
        monstersAnimator = GetComponent<Animator>();
        playerRb = player.GetComponent<Rigidbody>();
        //�J�������L����
        eventCamera.SetActive(false);

        MonsterStateProcessor.State = MonsterStateIdle;
    }

    void Update()
    {
        //�C�x���g��
        if (isEvent == true)
        {
            offScreenArrow.SetActive(false);
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

    //�C�x���g���n�܂�Ƃ��ɌĂԊ֐�
    public void StartEvent()
    {
        isEvent = true;
        playerRb.velocity = Vector3.zero;
        //�v���C���[�̑����s�ɂ���
        characterController.Switch = true;
        characterController.canJump = false;

        //�L�����N�^�[�̌����̐ݒ�
        player.transform.eulerAngles = transform.up;
        inEventPlayerPosition = new Vector3(transform.position.x - 3f, player.transform.position.y, transform.position.z - 40f);
        stepBackPosition = inEventPlayerPosition - new Vector3(0f, 0f, 15f);
        player.transform.position = inEventPlayerPosition;

        characterController.StateProcessor.State = characterController.StateIdle;
        playerAnimator.SetBool("walk", false);
        offScreenArrow.SetActive(false);
        eventCamera.SetActive(true);
    }

    public void MonstersSound()
    {
        if (existRedHero == false)
        {
            MonsterStateProcessor.State = MonsterStateJump;
        }
        else
        {
            MonsterStateProcessor.State = MonsterStateRun;
        }
    }

    public void PlayerStepBack()
    {
        isPlayerStepBack = true;
    }

    //�C�x���g���I���Ƃ��ɌĂԊ֐�
    public void EndEvent()
    {
        isEvent = false;
        //�L�����N�^�[�𑀍�\�ɂ���
        characterController.Switch = false;
        characterController.canJump = true;

        offScreenArrow.SetActive(true);
        eventCamera.SetActive(false);
        isPlayerStepBack = false;

        MonsterStateProcessor.State = MonsterStateIdle;
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

