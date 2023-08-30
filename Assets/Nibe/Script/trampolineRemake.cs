using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using CharacterState;
using TrampolineState;

public class trampolineRemake : MonoBehaviour
{
    public float bounciness = 0.8f; // �����W���̒l
    private PhysicMaterialCombine frictionCombine = PhysicMaterialCombine.Maximum; // ���C�������[�h�̒l
    private PhysicMaterialCombine bounceCombine = PhysicMaterialCombine.Maximum; // �����������[�h�̒l

    private new Collider collider;
    private new Rigidbody rigidbody;


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


    // Start is called before the first frame update
    void Start()
    {
        this.collider = this.GetComponent<Collider>();
        this.rigidbody = this.GetComponent<Rigidbody>();

        var newMaterial = new PhysicMaterial();

        newMaterial.bounciness = bounciness;
        newMaterial.frictionCombine = frictionCombine;
        newMaterial.bounceCombine = bounceCombine;

        this.collider.sharedMaterial = newMaterial;



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

    void OnCollisionEnter(Collision other)
    {
        //�@�Փ˂����Q�[���I�u�W�F�N�g�̃^�O��Player�̂Ƃ��������s��
        if (other.gameObject.tag == "Player")
        {
            StateProcessor.State = StateSmallJump;
        }
    }

    void OnCollisionExit(Collision other)
    {
        //�@�Փ˂����Q�[���I�u�W�F�N�g�̃^�O��Player�̂Ƃ��������s��
        if (other.gameObject.tag == "Player")
        {
            StateProcessor.State = StateIdle;
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
