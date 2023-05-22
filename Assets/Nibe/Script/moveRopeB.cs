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
    private bool grabbingRope = false;

    //����Ă��邩�ǂ����̔���
    private bool climbing = false;
    //�����Ă��邩�ǂ����̔���
    private bool climbingDown = false;

    //�v���C���[��rigidbody�i�[�p�ϐ�
    new Rigidbody rigidbody;
    GameObject player;

    //�v���C���[�����[�v�����X�s�[�h
    public float moveSpeed = 5.0f;

    //�v���C���[�����[�v���ړ����鋗��
    public float ropeMoveDistance = 10f;

    //Player��CharacterController���i�[
    private CharacterController characterController;

    //���[�v����������̈ʒu
    private Vector3 climbPos;

    //�L�����N�^�[�̈ʒu��␳����l
    private float positionCorrection = 1.2f;

    //���[�v�̏ォ�牺�܂ł̋���
    private float ropeDistance;

    //Ray����������̂������Ƃ��̃��[�v�̒���
    public float length;

    //���̈ʒu
    private Vector3 underPosition;

    //���[�v��͂�ł��锻�������Collider���i�[
    public CapsuleCollider grabRopeCollider;

    //���[�v�ɂ��߂锻�������Collider���i�[
    public CapsuleCollider moveOnCollider;

    //Player�̃W�����v�p��ray���Ƃ߂�I�u�W�F�N�g���i�[
    public GameObject rayStopper;

    private GameObject _rayHitObject;

    // �~�{�^����������Ă��邩�ǂ������擾����
    bool ps4X = false;
    // y�{�^����������Ă��邩�ǂ������擾����
    bool ps4Y = false;

    void Start()
    {
        //�v���C���[��������
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = player.gameObject.GetComponent<Rigidbody>();
        characterController = player.gameObject.GetComponent<CharacterController>();

        //���̈ʒu�Ɖ��܂ł̋�����ݒ�
        if (Physics.Raycast(transform.position, Vector3.up * -1f, out RaycastHit hit))
        {
            underPosition = hit.point;
        }
        else
        {
            //Ray����������̂������Ƃ���underPosition�̎w��
            underPosition = transform.position - (transform.up * length) + (transform.up * positionCorrection);
        }
        ropeDistance = Vector3.Distance(transform.position, underPosition);

        //Collider�̑傫����ʒu���w��
        moveOnCollider.center = -(transform.position - new Vector3(0, transform.up.y * (ropeDistance / 2), 0));
        grabRopeCollider.center = -(transform.position - new Vector3(0, transform.up.y * (ropeDistance / 2), 0));
        moveOnCollider.height = ropeDistance + moveOnCollider.radius * 1.5f;
        grabRopeCollider.height = ropeDistance;

        //���C���\�̕ύX
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    // Update is called once per frame
    void Update()
    {
        GetPS4X();
        GetPS4Y();
        if (moveOn == true)  //�o��
        {
            if (characterController.rayHitObject != null &&
                characterController.rayHitObject == gameObject && 
                (Input.GetKeyDown(KeyCode.Space) || ps4X))
            {
                if (grabbingRope == false)
                {
                    grabbingRope = true;

                    //Rigidbody���~
                    rigidbody.velocity = Vector3.zero;
                }
            }
        }
        else if (moveOn == false && grabbingRope)
        {
            //�d�͂𕜊�������
            rigidbody.isKinematic = false;
            grabbingRope = false;
        }

        if (grabbingRope == true)
        {
            //�d�͂��~������
            rigidbody.isKinematic = true;

            rayStopper.transform.position = player.transform.position - player.transform.up * 1.3f;

            //��
            if (player.transform.position.y < transform.position.y + positionCorrection)
            {
                if (climbing == false && climbingDown == false &&
                    Input.GetKeyDown(KeyCode.Space) || ps4X)
                {
                    climbPos = new Vector3(player.transform.position.x, player.transform.position.y + ropeMoveDistance, player.transform.position.z);
                    climbing = true;
                }
            }
            else if (player.transform.position.y >= transform.position.y + positionCorrection &&
                climbing == true)
            {
                climbPos = player.transform.position;
                characterController.enabled = true;
                climbing = false;
            }

            //��
            if (player.transform.position.y > underPosition.y + positionCorrection)
            {
                if (climbing == false && climbingDown == false &&
                    Input.GetKeyDown(KeyCode.LeftShift) || ps4Y)
                {
                    climbPos = new Vector3(player.transform.position.x, player.transform.position.y - ropeMoveDistance, player.transform.position.z);
                    climbingDown = true;
                }
            }
            else if (player.transform.position.y <= underPosition.y + positionCorrection &&
                climbingDown == true)
            {
                climbPos = player.transform.position;
                characterController.enabled = true;
                climbingDown = false;
            }

            if (climbing || climbingDown)
            {
                characterController.enabled = false;
                if (player.transform.position == climbPos)
                {
                    climbing = false;
                    climbingDown = false;
                }
                else
                {
                    player.transform.position = Vector3.MoveTowards(player.transform.position, climbPos, moveSpeed * Time.deltaTime);
                }
            }
            else
            {
                characterController.enabled = true;
            }
        }
        else
        {
            rayStopper.transform.position = new Vector3(0.0f, transform.position.y - 0.1f, 0.0f);
        }
        //CharacterMovement();  //���E
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

    void GetPS4Y()
    {
        if(Gamepad.current != null)
        {
            if(Gamepad.current.buttonWest.isPressed)
            {
                ps4Y = true;
            }
            else
            {
                ps4Y = false;
            }
        }           
    }
}