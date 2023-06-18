using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class camera : MonoBehaviour
{
    public GameObject playerObject;         //�ǔ� �I�u�W�F�N�g
    public Vector2 rotationSpeed;           //��]���x

    //player�̃I�u�W�F�N�g���i�[
    private GameObject Parent;

    //���̃J�����̃��[�J�����W���i�[
    private Vector3 Position;

    //�J�����𐳖ʂɖ߂����Ƃ��̈ʒu
    private Vector3 firstPosition;


    private Vector3 prePosition;
    private Vector3 currentPosition;

    //�J�����ƃv���C���[���ł��߂�����
    public float nearestCameraPosition = 3.0f;

    //�J���������̂ɓ������Ă�����
    private RaycastHit Hit;

    //�J�����ƃv���C���[�̋������i�[
    private float Distance;

    //�J�����������郌�C��[���i�[
    public LayerMask Mask;

    //�J�������I�u�W�F�N�g������Ă��邩�̔���
    private bool avoidWall;

    void Start()
    {
        Parent = transform.root.gameObject;
        firstPosition = transform.localPosition;
        if (transform.root.gameObject.CompareTag("Player"))
            gameObject.transform.parent = null;
        Position = transform.position;       
        Distance = Vector3.Distance(Parent.transform.position, transform.position);
        currentPosition = Parent.transform.position;
    }

    void FixedUpdate()
    {
        //�q�I�u�W�F�N�g����O���ăJ�������ړ�����悤�ɂ���
        prePosition = currentPosition;
        currentPosition = Parent.transform.position;
        transform.position = transform.position + (currentPosition - prePosition);

        bool stickPressed = Gamepad.current.rightStickButton.isPressed;
        if (Input.GetKey(KeyCode.H) || (stickPressed == true))
        {
            Vector3 bhindAngle = currentPosition + (Parent.transform.up * firstPosition.y) + (Parent.transform.forward * firstPosition.z);
            transform.position = bhindAngle;
        }
    }

    void Update()
    {
        Rotate();
        AvoidWall();
    }

    void Rotate()
    {
        float cal = PlayerPrefs.GetFloat("Sensi");  //�}�E�X���x���擾���Ă�i�d�v�j
        transform.LookAt(Parent.transform);
        float rotX = Input.GetAxis("Mouse X") * rotationSpeed.x;
        float rotY = Input.GetAxis("Mouse Y") * rotationSpeed.y;

        if (transform.forward.y * -1f > 0.8f && rotY < 0)
        {
            rotY = 0;
        }
        if (transform.forward.y * -1f < -0.8f && rotY > 0)
        {
            rotY = 0;
        }

        // �Q�[���p�b�h���ڑ�����Ă��Ȃ��Ƃ�
        if (Gamepad.current == null)
        {
            //�v���C���[�𒆐S�ɉ�]����
            transform.RotateAround(playerObject.transform.position, transform.up, rotX * cal);
            transform.RotateAround(playerObject.transform.position, transform.right, -rotY * cal);
        }
        else
        {
            // �E�X�e�B�b�N�̓��͂��󂯎��
            var v = Gamepad.current.rightStick.ReadValue();

            if (transform.forward.y * -1f > 0.8f && v.y < 0)
            {
                v.y = 0;
            }
            if (transform.forward.y * -1f < -0.8f && v.y > 0)
            {
                v.y = 0;
            }

            if (rotY == 0)
            {
                //�v���C���[�𒆐S�ɉ�]����
                transform.RotateAround(playerObject.transform.position, transform.up, v.x * 0.5f * cal);
                transform.RotateAround(playerObject.transform.position, transform.right, -v.y * 0.5f * cal);
            }
            else
            {
                //�v���C���[�𒆐S�ɉ�]����
                transform.RotateAround(playerObject.transform.position, transform.up, rotX * cal);
                transform.RotateAround(playerObject.transform.position, transform.right, -rotY * cal);
            }
        }
    }

    void AvoidWall()
    {
        //�ǂ�����Ă��Ȃ��Ƃ�
        if (avoidWall == false)
        {
            Position = transform.position;
        }
        //�v���C���[�ƃJ�����̊ԂɃI�u�W�F�N�g������Ƃ�
        if (Physics.SphereCast(Parent.transform.position, 0.5f, (transform.position - Parent.transform.position�@).normalized, out Hit, Distance, Mask))
        {      
            avoidWall = true;
            if (Hit.distance < nearestCameraPosition + 0.1f)
            {
                //�J�����ƃv���C���[�̋������߂��Ƃ��J���������ɖ��܂�Ȃ��悤��
                transform.position = Parent.transform.position + (transform.position - Parent.transform.position).normalized * nearestCameraPosition;
            }
            else
                //�J�����̈ʒu���ړ�
                transform.position = Parent.transform.position + (transform.position - Parent.transform.position).normalized * Hit.distance;
        }
        else//�v���C���[�ƃJ�����̊ԂɃI�u�W�F�N�g���Ȃ��Ƃ�
        {
            //�J���������̈ʒu�܂ňړ�����
            transform.localPosition = Vector3.Lerp(transform.position, Position, 2);
            if (avoidWall == true)
            {
                avoidWall = false;
            }
        }
    }
}



