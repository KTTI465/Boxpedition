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

    //���[�v��͂�ł��邩�ǂ����̔���
    private bool isGrabRope = false;

    //����Ă��邩�ǂ����̔���
    private bool isClimbing = false;

    //�v���C���[��rigidbody�i�[�p�ϐ�
    new Rigidbody rigidbody;
    GameObject player;

    //�v���C���[�����[�v�����X�s�[�h
    public  float moveSpeed = 5.0f;

    //�v���C���[�����[�v����鋗��
    public float climbDistance = 10f;

    private CharacterController characterController;
    private Vector3 climbPos;

    // �~�{�^����������Ă��邩�ǂ������擾����
    bool ps4X = false;

    void Start()
    {
        //�v���C���[��������
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = player.gameObject.GetComponent<Rigidbody>();
        characterController = player.gameObject.GetComponent<CharacterController>();

        //for(int i = 0; dist < distace)
    }

    // Update is called once per frame
    void Update()
    {
        GetPS4X();
        if (moveOn == true)  //�o��
        {
            GameObject _rayHitObject = characterController.rayHitObject.collider.gameObject;
            if (_rayHitObject == gameObject && (Input.GetKeyDown(KeyCode.Space) || ps4X))
            {

                if (isGrabRope == false)
                {
                    isGrabRope = true;

                    //Rigidbody���~
                    rigidbody.velocity = Vector3.zero;
                }
            }
        }

        if (isClimbing == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) || ps4X)
            {
                climbPos = new Vector3(player.transform.position.x, player.transform.position.y + climbDistance, player.transform.position.z);
                isClimbing = true;
            }
        }



        if (isGrabRope == true)
        {
            //�d�͂��~������
            rigidbody.isKinematic = true;

            characterController.enabled = false;
            //CharacterMovement();  //���E

            if (player.transform.position.y > transform.position.y)
            {
                climbPos = player.transform.position;
                isClimbing = false;
                characterController.enabled = true;
            }

            if (isClimbing)
            {
                if (player.transform.position == climbPos)
                {
                    isClimbing = false;
                }
                else
                {
                    player.transform.position = Vector3.MoveTowards(player.transform.position, climbPos, moveSpeed * Time.deltaTime);
                }
            }
        }
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            moveOn = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            moveOn = false;
            if (isGrabRope && player.transform.position.y > transform.position.y)
            {
                //�d�͂𕜊�������
                rigidbody.isKinematic = false;
                isGrabRope = false;
            }
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