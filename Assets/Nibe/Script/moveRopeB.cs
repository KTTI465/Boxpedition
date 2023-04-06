using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;
using static UnityEditor.PlayerSettings;

public class moveRopeB : MonoBehaviour
{
    private float xMovement, zMovement;
    private float movementSpeed = 0.1f;  //���E�p

    //�ڐG�������ǂ����̔���
    private bool moveOn = false;

    //�v���C���[��rigidbody�i�[�p�ϐ�
    new Rigidbody rigidbody;
    GameObject player;

    private float speed = 5.0f;

    // �~�{�^����������Ă��邩�ǂ������擾����
    bool ps4X = false;


    void Start()
    {
        //�v���C���[��������
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = player.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPS4X();

        if (moveOn == true && (Input.GetKey(KeyCode.Space) || ps4X))  //�o��
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, this.transform.position, speed * Time.deltaTime);
            //CharacterMovement();  //���E
        }
        else
        {
            if (moveOn == true)
            {
                moveOn = false;

                //�d�͂𕜊�������
                rigidbody.isKinematic = false;

                //�e�q�֌W������
                player.gameObject.transform.parent = null;
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            moveOn = true;

            //Rigidbody���~
            rigidbody.velocity = Vector3.zero;

            //�d�͂��~������
            rigidbody.isKinematic = true;

            //�e�q�֌W�ɂ���
            player.gameObject.transform.parent = this.gameObject.transform;
        }
    }

    private void CharacterMovement()
    {
        xMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        zMovement = Input.GetAxisRaw("Vertical") * movementSpeed;

        player.transform.Translate(-xMovement, 0, -zMovement);  //���E���邽�߂ɋt�����ɗ͉�����
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