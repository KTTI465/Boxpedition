using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampolineC : MonoBehaviour
{
    Rigidbody rigidBody;
    Rigidbody playerRigidBody;
    GameObject player;

    public float bigJumpPower;  //��W�����v
    public float smallJumpPower;  //���W�����v


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
            if (Input.GetKey(KeyCode.Space))  //�X�y�[�X�L�[�������Ă���Ƃ�
            {
                playerRigidBody.AddForce(Vector3.up * bigJumpPower, ForceMode.Impulse);  //��W�����v
            }
            else
            {
                playerRigidBody.AddForce(Vector3.up * smallJumpPower, ForceMode.Impulse);  //���W�����v
            }
        }
    }
}