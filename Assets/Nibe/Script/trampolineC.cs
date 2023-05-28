using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using static UnityEditor.PlayerSettings;

public class trampolineC : MonoBehaviour
{
    Rigidbody rigidBody;
    Rigidbody playerRigidBody;
    GameObject player;

    public float bigJumpPower;  //��W�����v
    public float smallJumpPower;  //���W�����v

    // �~�{�^����������Ă��邩�ǂ������擾����
    bool ps4X = false;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        //�v���C���[���^�O�Ō������ARigidbody���擾
        player = GameObject.FindGameObjectWithTag("Player");
        playerRigidBody = player.GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        //�@�Փ˂����Q�[���I�u�W�F�N�g�̃^�O��Player�̂Ƃ��������s��
        if (other.gameObject.tag == "Player")
        {
            GetPS4X();

            if (Input.GetKey(KeyCode.Space) || ps4X)  //�X�y�[�X�L�[�������Ă���Ƃ�
            {
                playerRigidBody.AddForce(Vector3.up * bigJumpPower, ForceMode.Impulse);  //��W�����v
            }
            else
            {
                playerRigidBody.AddForce(Vector3.up * smallJumpPower, ForceMode.Impulse);  //���W�����v
            }
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
}