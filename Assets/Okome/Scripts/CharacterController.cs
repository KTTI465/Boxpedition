using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class CharacterController : MonoBehaviour
{
    private Rigidbody rb;
    private float xMovement, zMovement;
    private float movementSpeed = 0.1f;
    [SerializeField] private Camera playerCam;
    public GameObject cam;
    private Quaternion cameraRot, characterRot;
    private float sensitivity = 1f;

    //�������邽�߂�box��Prefab���i�[���邽�߂̕ϐ�
    [SerializeField]
    private GameObject box;

    //Player�̎q�I�u�W�F�N�g�ɂȂ��Ă���box���i�[���邽�߂̕ϐ�
    private GameObject connectingBox;

    //1��ڂ̃W�����v����Ƃ��̗͂��w�肷�邽�߂̕ϐ�
    public float firstJumpPower;

    //2��ڂ̃W�����v����Ƃ��̗͂��w�肷�邽�߂̕ϐ�
    public float secondJumpPower;

    //Raycast�̒������i�[���邽�߂̕ϐ�
    private float jumpDistance;

    //��i�W�����v���������𔻒肷��
    public bool doubleJumped = false;

    //connectingBox�̏��Player������悤�ʒu�𒲐����邽�߂̕ϐ�
    //connectingBox��Player�̑傫���Ŏ���Œ������K�v
    private float enterBoxMove = 1f;

    public RaycastHit rayHitObject;

    public LayerMask layerMask;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
        cameraRot = Quaternion.Euler(0, 0, 0);

        //connectingBox�����������Ƃ��ɌĂяo��
        if (connectingBox == null)
        {
            //connectingBox �Ƃ���box��Player�Ɠ����ʒu�ƌ����Ő���
            connectingBox = Instantiate(box, transform.position, transform.rotation);

            //Player��connectingBox �̏�ɗ���悤�Ɉړ�
            transform.position = new Vector3(transform.position.x, transform.position.y + enterBoxMove, transform.position.z);

            //���̃I�u�W�F�N�g��connectingBox �̐e�I�u�W�F�N�g�ɂ���
            connectingBox.transform.parent = gameObject.transform;

        }
    }

    void Update()
    {
        CharacterJump();
        ray();
    }

    private void FixedUpdate()
    {
        CharacterMovement();
        CharacterRotate();
    }

    private void CharacterMovement()
    {
        xMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        zMovement = Input.GetAxisRaw("Vertical") * movementSpeed;

        transform.Translate(xMovement, 0, zMovement);
    }

    private void CharacterRotate()
    {
        //float cal = PlayerPrefs.GetFloat("Sensi");
        float cal = 1f;

        //�}�E�X�̉������̓����~ sensitivity�ŉ������̉�]�������Ă���B
        float xRot = Input.GetAxis("Mouse X") * sensitivity;

        // �Q�[���p�b�h���ڑ�����Ă��Ȃ��Ƃ�
        if (Gamepad.current == null)
        {
            characterRot *= Quaternion.Euler(0, xRot * 2.0f * cal, 0);
        }
        else
        {
            // �E�X�e�B�b�N�̓��͂��󂯎��
            var v = Gamepad.current.rightStick.ReadValue();

            if (xRot == 0)
            {
                characterRot *= Quaternion.Euler(0, v.x * 2.0f * cal, 0);
            }
            else
            {
                characterRot *= Quaternion.Euler(0, xRot * 2.0f * cal, 0);
            }
        }

        transform.localRotation = characterRot;
    }

    private void CharacterJump()
    {
        //�n�ʂ�Ray���t���Ă��邩�̔���
        bool isGround;

        //connectingBox������Ƃ�
        if (connectingBox)
        {
            //connectingBox�����邱�Ƃ��������Ă�Player���n�ʂɂ��Ă��邩�𔻒肷��Ray�̒����@
            //�l�͕ύX����K�v����i���͖��ߍ��݂Ŏ����ł��Ă��Ȃ��̂ł��̒l�j
            //����Player�̑傫������ł��������K�v
            jumpDistance = 2.1f;

            //Player����o�Ă���Ray��connectingBox�������悤��layer���w��(box��layer)
            int layerMask = connectingBox.layer;

            isGround = Physics.Raycast(transform.position, Vector3.up * -1f, jumpDistance, layerMask);
        }
        else
        {
            //connectingBox�������Ƃ���Player���n�ʂɂ��Ă��邩�𔻒肷��Ray�̒���
            //Player�̑傫������Œ������K�v
            jumpDistance = 1.1f;

            isGround = Physics.Raycast(transform.position, Vector3.up * -1f, jumpDistance);
        }

        // �~�{�^����������Ă��邩�ǂ������擾����
        var ps4X = false;

        if (Gamepad.current != null)
        {
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                ps4X = true;
            }
        }

        //�X�y�[�X�L�[�i�~�{�^���j���������Ƃ��ɃW�����v����
        if (Input.GetKeyDown(KeyCode.Space) || ps4X)
        {
            //�n�ʂɂ��Ă�����
            if (isGround == true)
            {
                rb.velocity = Vector3.up * firstJumpPower;
            }
            //�󒆂ɂ���Ƃ�����i�W�����v�����Ă��Ȃ���
            else if (isGround == false && doubleJumped == false)
            {
                rb.velocity = Vector3.up * secondJumpPower;

                //box�ɂ��Ă���X�N���v�g�̃R���[�`�����g���A�P�b��ɔ���������悤�ɂ���
                IEnumerator destroyTimer = connectingBox.GetComponent<Box>().DestroyBox();
                StartCoroutine(destroyTimer);

                //box�ɂ͐e�ɒǏ]�����邽�߂�Rigidbody�����Ă��Ȃ��̂ŉ��ɗ�����悤��Rigidbody������
                connectingBox.AddComponent<Rigidbody>();

                //connectingBox�����ɗ�����悤�ɂ��̃I�u�W�F�N�g�̎q����͂���
                connectingBox.transform.parent = null;

                //�i�[����Ă���connectingBox���͂���
                connectingBox = null;

                //��i�W�����v���������true�ɂ���
                doubleJumped = true;
            }
        }

        //��i�W�����v��������̎��n�ʂɂ����ꍇ
        if (isGround == true && doubleJumped == true)
        {
            //��i�W�����v���������false�ɂ���
            doubleJumped = false;

            //connectingBox�������Ƃ�
            if (connectingBox == null)
            {
                //connectingBox�Ƃ��ĐV����box��Player�Ɠ����ʒu�ƌ����ɐ���
                connectingBox = Instantiate(box, transform.position, transform.rotation);

                //connectingBox�̏��Player������悤�Ɉʒu�𒲐�
                transform.position = new Vector3(transform.position.x, transform.position.y + enterBoxMove, transform.position.z);

                //connectingBox�̐e�I�u�W�F�N�g�ɂ��̃I�u�W�F�N�g���w��
                connectingBox.transform.parent = gameObject.transform;

            }
        }
    }

    public void ray()
    {
       
        //int layerMask = ~gameObject.layer;
        Physics.Raycast(playerCam.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out rayHitObject, 100f,layerMask);
        Debug.Log(rayHitObject.collider.gameObject.name);
    }
}
