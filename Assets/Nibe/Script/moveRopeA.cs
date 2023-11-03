using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;


// �^�[�U���p�̃��[�v

public class moveRopeA : MonoBehaviour
{
    private float xMovement, zMovement;
    private float movementSpeed = 0.1f;  // ���E�p

    // ���̊p�x
    private float angle = 0f;
    // �����n�߂鎞�̎���
    private float startTime;
    // �U��q������p�x
    [SerializeField]
    private float limitAngle = 90f;

    // �i��ł������
    private int direction = 1;

    // ���[�v���������ǂ����̔���
    private bool moveOn = false;

    // ��ԊԊu
    [SerializeField]
    private float duration = 5f;

    public float angleX;
    public float angleY;
    public float angleZ;

    // �v���C���[��rigidbody�i�[�p�ϐ�
    new Rigidbody rigidbody;
    GameObject player;

    [SerializeField] Transform target;
    private float speed = 5.0f;

    // �~�{�^����������Ă��邩�ǂ������擾����
    bool ps4X = false;


    void Start()
    {
        startTime = Time.time;

        //�v���C���[��������
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = player.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetPS4X();

        if (moveOn == true && (Input.GetKey(KeyCode.Space) || ps4X))  //����
        {
            // �o�ߎ��Ԃɍ��킹���������v�Z
            float t = (Time.time - startTime) / duration;

            // �X���[�Y�Ɋp�x���v�Z
            angle = Mathf.SmoothStep(angle, direction * limitAngle, t);

            // ���[�v�̉��̂ق��Ɉړ�����
            player.transform.position = Vector3.MoveTowards(player.transform.position, target.position, speed * Time.deltaTime);

            //CharacterMovement();  //���E
        }
        else  //�~�܂�
        {
            if(moveOn == true)
            {
                moveOn = false;

                //�d�͂𕜊�������
                rigidbody.isKinematic = false;

                //�e�q�֌W������
                player.gameObject.transform.parent = null;
            }

            if(angle >= -1f && angle <= 1f)
            {
                angle = 0f;
                direction = 1;
            }
            else
            {
                if (angle > 0f)
                {
                    angle -= 0.1f;  //���̈ʒu�ɖ߂�
                }

                if (angle < 0f)
                {
                    angle += 0.1f;  //���̈ʒu�ɖ߂�
                }
            }
        }


        //�@�p�x��ύX
        transform.localEulerAngles = new Vector3(angleX * angle, angleY * angle, angleZ * angle);
        //�@�p�x���w�肵���p�x��1�x�̍��ɂȂ����甽�]
        if (Mathf.Abs(Mathf.DeltaAngle(angle, direction * limitAngle)) < 1f)
        {
            direction *= -1;
            startTime = Time.time;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            GetPS4X();

            if (Input.GetKey(KeyCode.Space) || ps4X)
            {
                moveOn = true;

                startTime = Time.time;

                //Rigidbody���~
                rigidbody.velocity = Vector3.zero;

                //�d�͂��~������
                rigidbody.isKinematic = true;

                //�e�q�֌W�ɂ���
                player.gameObject.transform.parent = this.gameObject.transform;
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