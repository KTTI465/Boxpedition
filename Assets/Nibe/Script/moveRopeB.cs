using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

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
    public float moveSpeed;

    //�v���C���[�����[�v���ړ����鋗��
    public float ropeMoveDistance;

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

    //���[�v��͂�ł���Ƃ��̑O��ړ��̑���
    public float forwardMoveSpeed;

    //���[�v��͂�ł���Ƃ��̍��E�ړ��̑���
    public float sideMoveSpeed;

    //���̈ʒu
    private Vector3 underPosition;

    [SerializeField]//���[�v��͂�ł��锻�������Collider���i�[
    private CapsuleCollider grabRopeCollider;

    [SerializeField]//���[�v�ɂ��߂锻�������Collider���i�[
    private CapsuleCollider moveOnCollider;

    //���[�v��͂񂾎��̋������i�[����
    private float grabDist;

    [SerializeField] //Player�̃W�����v�p��ray���Ƃ߂�I�u�W�F�N�g���i�[
    private GameObject rayStopper;

    private GameObject _rayHitObject;

    [SerializeField]//�L�[�{�[�h�}�E�X����̂Ƃ��̃C���^���N�g�̉摜
    private GameObject interactImageKeyboardMouse;

    [SerializeField]//�p�b�h����̂Ƃ��̃C���^���N�g�̉摜
    private GameObject interactImageGamepad;

    private GameObject interactImage;

    [SerializeField]//�L�[�{�[�h�}�E�X����̂Ƃ��̏��̉摜
    private GameObject climbUpImageKeyboardMouse;

    [SerializeField]//�p�b�h����̂Ƃ��̏��̉摜
    private GameObject climbUpImageGamepad;

    private GameObject climbUpImage;

    [SerializeField]//�L�[�{�[�h�}�E�X����̂Ƃ��̉����̉摜
    private GameObject climbDownImageKeyboardMouse;

    [SerializeField]//�p�b�h����̂Ƃ��̉����̉摜
    private GameObject climbDownImageGamepad;

    private GameObject climbDownImage;

    // �~�{�^����������Ă��邩�ǂ������擾����
    bool ps4X = false;
    // y�{�^����������Ă��邩�ǂ������擾����
    bool ps4Y = false;

    [SerializeField]
    private Animator charaAnimator;

    void Start()
    {
        //�v���C���[��������
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = player.gameObject.GetComponent<Rigidbody>();
        characterController = player.gameObject.GetComponent<CharacterController>();

        //���̈ʒu�Ɖ��܂ł̋�����ݒ�
        if(length > 0)
        {
            //Ray����������̂������Ƃ���underPosition�̎w��
            underPosition = transform.position - (transform.up * length) + (transform.up * positionCorrection);
        }
        else if(length == 0 && Physics.Raycast(transform.position, transform.up * -1f, out RaycastHit hit))
        {
            underPosition = hit.point;
        }
        ropeDistance = Vector3.Distance(transform.position, underPosition);

        //Collider�̑傫����ʒu���w��
        moveOnCollider.center = - new Vector3(0, transform.up.y * (ropeDistance / 2), 0);
        grabRopeCollider.center = - new Vector3(0, transform.up.y * (ropeDistance / 2), 0);
        moveOnCollider.height = ropeDistance + moveOnCollider.radius * 1.5f;
        grabRopeCollider.height = ropeDistance;

        //���C���\�̕ύX
        gameObject.layer = LayerMask.NameToLayer("IgnoreCameraRay");
    }

    // Update is called once per frame
    void Update()
    {
        GetPS4XY();
        ImageChange();

        if (grabbingRope == true)
        {
            //�L�����N�^�[�𑀍�ł��Ȃ��悤��
            characterController.enabled = false;

            if (grabDist == 0)
            {
                //�͂񂾎��_�ł̋����𑪂�A����ȏ㗣�ꂽ�烍�[�v�𗣂��悤�ɂ���
                grabDist = Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
                    new Vector2(player.transform.position.x, player.transform.position.z));
            }        
            moveOnCollider.radius = grabDist -0.5f;

            if(interactImage == true)
            {
                interactImage.SetActive(false);
            }
            //�d�͂��~������
            rigidbody.isKinematic = true;

            //�W�����v�p�̃��C���Ƃ߂�I�u�W�F�N�g�̈ʒu��ݒ�
            rayStopper.transform.position = player.transform.position - player.transform.up * 1.3f;

            charaAnimator.SetBool("climb", false); // �A�j���[�V�����؂�ւ�



            //��̐����ʒu��艺�ɂ���Ƃ�
            if (player.transform.position.y < transform.position.y + positionCorrection)
            {
                //���[�v�œ����Ă��Ȃ��Ƃ�
                if (climbing == false && climbingDown == false)
                {
                    //���̉摜��\��
                    climbUpImage.SetActive(true);

                    //���{�^���������Ə��
                    if (Input.GetKeyDown(KeyCode.Space) || ps4X)
                    {
                        climbPos = new Vector3(player.transform.position.x, player.transform.position.y + ropeMoveDistance, player.transform.position.z);
                        climbing = true;
                    } 
                }
                else
                {
                    //���̉摜���\��
                    climbUpImage.SetActive(false);
                }
            }
            //��̐����ʒu����ɂ���Ƃ�����Ă����Ȃ��悤�ɂ���
            else if (player.transform.position.y >= transform.position.y + positionCorrection &&
                climbing == true)
            {             
                climbPos = player.transform.position;
                characterController.enabled = true;
                climbing = false;
            }

            //���̐����ʒu����ɂ���Ƃ�
            if (player.transform.position.y > underPosition.y + positionCorrection)
            {
                //���[�v�œ����Ă��Ȃ��Ƃ�
                if (climbing == false && climbingDown == false)
                {
                    //����̉摜��\��
                    climbDownImage.SetActive(true);

                    //�����{�^���������Ɖ����
                    if (Input.GetKeyDown(KeyCode.LeftShift) || ps4Y)
                    {
                        climbPos = new Vector3(player.transform.position.x, player.transform.position.y - ropeMoveDistance, player.transform.position.z);
                        climbingDown = true;
                    }
                }
                else
                {
                    //����̉摜���\��
                    climbDownImage.SetActive(false);
                }
            }
            //���̐����ʒu��艺�ɂ���Ƃ��͉���Ă����Ȃ��悤�ɂ���
            else if (player.transform.position.y <= underPosition.y + positionCorrection &&
                climbingDown == true)
            {
                climbPos = player.transform.position;
                characterController.enabled = true;
                climbingDown = false;
            }

            //�o���Ă���Ƃ��܂��͍~��Ă���Ƃ�
            if (climbing || climbingDown)
            {
                //�ړ��ʒu�Ɠ����ʒu�ɂȂ�����ړ�����bool��false�ɂ���
                if (player.transform.position == climbPos)
                {
                    climbing = false;
                    climbingDown = false;
                }
                else
                {
                    //�ړ���̈ʒu�Ɠ����ʒu�Ŗ����Ƃ��͈ړ�������
                    player.transform.position = Vector3.MoveTowards(player.transform.position, climbPos, moveSpeed * Time.deltaTime);

                    if (climbing)
                    {
                        charaAnimator.SetBool("climb", true); // �A�j���[�V�����؂�ւ�
                    }
                }
            }
            else
            {
                Debug.Log("W");
                //�o���Ă��Ȃ��Ƃ��܂��͍~��Ă��Ȃ��Ƃ��̓��[�v�ő���ł���悤��
                float xMovement = Input.GetAxisRaw("Horizontal") * -forwardMoveSpeed;
                float zMovement = Input.GetAxisRaw("Vertical");
                if(Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
                    new Vector2(player.transform.position.x, player.transform.position.z)) <= 1f)
                {
                    zMovement = Mathf.Clamp(zMovement, -1, 0);
                }
                player.transform.position = player.transform.position + (player.transform.forward * (zMovement/(10 * sideMoveSpeed)));
                player.transform.LookAt(new Vector3(transform.position.x, player.transform.position.y, transform.position.z));
                player.transform.RotateAround(new Vector3(transform.position.x,player.transform.position.y, transform.position.z), transform.up, xMovement);
            }
        }
        else
        {
            //�L�����N�^�[�����[�v��͂�ł��Ȃ�����
            //�W�����v�p�̃��C���Ƃ߂�I�u�W�F�N�g�̈ʒu����ԏ�ɐݒ�
            if(gameObject.transform.root == true)
            rayStopper.transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
            //�L�����N�^�[�𑀍�ł���悤�ɂ���
           // characterController.enabled = true;

            //�͂ނ��Ƃ��ł���͈͂����ɖ߂�
            moveOnCollider.radius = 3.0f;
            grabDist = 0;
        }

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

                    charaAnimator.SetBool("climbStay", true); // �A�j���[�V�����؂�ւ�
                }
            }
        }
        else if (moveOn == false && grabbingRope)
        {
            //�d�͂𕜊�������
            rigidbody.isKinematic = false;
            grabbingRope = false;
            interactImage.SetActive(false);
            climbUpImage.SetActive(false);
            climbDownImage.SetActive(false);

            charaAnimator.SetBool("climbStay", false); // �A�j���[�V�����؂�ւ�
        }
        //CharacterMovement();  //���E
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            //�͂ނ��Ƃ��ł���悤��
            moveOn = true;
            interactImage.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            moveOn = false;
            interactImage.SetActive(false);
            climbUpImage.SetActive(false);
            climbDownImage.SetActive(false);
        }
    }

    private void CharacterMovement()
    {
        xMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        zMovement = Input.GetAxisRaw("Vertical") * movementSpeed;

        player.transform.Translate(-xMovement, 0, -zMovement);  //���E���邽�߂ɋt�����ɗ͉�����
    }

    void GetPS4XY()
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

            if (Gamepad.current.buttonWest.isPressed)
            {
                ps4Y = true;
            }
            else
            {
                ps4Y = false;
            }
        }
    }

    void ImageChange()
    {
        if (Gamepad.current != null) //�p�b�h����̂Ƃ�
        {
            //�p�b�h����̃C���^���N�g�̉摜��ݒ�
            if (interactImage != interactImageGamepad)
            {
                interactImage = interactImageGamepad;
            }
            //�p�b�h����̃��[�v�����̉摜��ݒ�
            if (climbUpImage != interactImageGamepad)
            {
                interactImage = interactImageGamepad;
            }
            //�p�b�h����̃��[�v�������̉摜��ݒ�
            if (climbDownImage != interactImageGamepad)
            {
                interactImage = interactImageGamepad;
            }
        }
        else //�L�[�{�[�h�}�E�X����̂Ƃ�
        {
            //�L�[�{�[�h�}�E�X����̃C���^���N�g�̉摜��ݒ�
            if (interactImage != interactImageKeyboardMouse)
            { 
                interactImage = interactImageKeyboardMouse;
            }
            //�L�[�{�[�h�}�E�X����̃��[�v�����̉摜��ݒ�
            if (climbUpImage !=  climbUpImageGamepad)
            {
                climbUpImage = climbUpImageKeyboardMouse;
            }
            //�L�[�{�[�h�}�E�X����̃��[�v�������̉摜��ݒ�
            if (climbDownImage != climbDownImageKeyboardMouse)
            {
                climbDownImage = climbDownImageKeyboardMouse;
            }
        }
    }
}