using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using CharacterState;
using static UnityEditor.PlayerSettings;
using TrampolineState;

public class trampolineC : MonoBehaviour
{
    Rigidbody rigidBody;
    Rigidbody playerRigidBody;
    GameObject player;

    private string _preStateName;

    public TrampolineStateProcessor StateProcessor { get; set; } = new TrampolineStateProcessor();
    public TrampolineStateSmallJump StateSmallJump { get; set; } = new TrampolineStateSmallJump();
    public TrampolineStateBigJump StateBigJump { get; set; } = new TrampolineStateBigJump();
    public TrampolineStateIdle StateIdle { get; set; } = new TrampolineStateIdle();

    public float bigJumpPower;  //��W�����v
    public float smallJumpPower;  //���W�����v

    // �~�{�^����������Ă��邩�ǂ������擾����
    bool ps4X = false;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        StateSmallJump.ExecAction = SmallJump;
        StateBigJump.ExecAction = BigJump;
        StateIdle.ExecAction = Idle;

        StateProcessor.State = StateIdle;

        //�v���C���[���^�O�Ō������ARigidbody���擾
        player = GameObject.FindGameObjectWithTag("Player");
        playerRigidBody = player.GetComponent<Rigidbody>();
    }

    void Update()
    {

        //�X�e�[�g�̒l���ύX���ꂽ����s�������s��
        if (StateProcessor.State.GetStateName() != _preStateName)
        {
            _preStateName = StateProcessor.State.GetStateName();
            StateProcessor.Execute();
        }
    }

    void OnTriggerStay(Collider other)
    {
        //�@�Փ˂����Q�[���I�u�W�F�N�g�̃^�O��Player�̂Ƃ��������s��
        if (other.gameObject.tag == "Player")
        {
            GetPS4X();

            if (Input.GetKey(KeyCode.Space) || ps4X)  //�X�y�[�X�L�[�������Ă���Ƃ�
            {
                //Debug.Log("��");
                StateProcessor.State = StateBigJump;
                playerRigidBody.AddForce(Vector3.up * bigJumpPower, ForceMode.Impulse);  //��W�����v
            }
            else
            {
                //Debug.Log("��");
                StateProcessor.State = StateSmallJump;
                playerRigidBody.AddForce(Vector3.up * smallJumpPower, ForceMode.Impulse);  //���W�����v
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        //�@�Փ˂����Q�[���I�u�W�F�N�g�̃^�O��Player�̂Ƃ��������s��
        if (other.gameObject.tag == "Player")
        {
                StateProcessor.State = StateIdle;   
        }
    }

    void GetPS4X()
    {
        if (Gamepad.current != null)
        {
            if (Gamepad.current.buttonSouth.isPressed)
            {
                ps4X = true;
            }
            else
            {
                ps4X = false;
            }
        }
    }

    public void SmallJump()
    {
        Debug.Log("TrampolineState��SmallJump�ɏ�ԑJ�ڂ��܂����B");
    }
    public void BigJump()
    {
        Debug.Log("TrampolineState��BigJump�ɏ�ԑJ�ڂ��܂����B");
    }
    public void Idle()
    {
        Debug.Log("TrampolineState��Idle�ɏ�ԑJ�ڂ��܂����B");
    }
}