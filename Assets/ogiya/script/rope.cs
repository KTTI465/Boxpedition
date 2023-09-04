using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;


// �^�[�U���p�̃��[�v

public class rope: MonoBehaviour
{
    public int targetX;
    public int targetY;
    public int targetZ;
    private float xMovement, zMovement;
    private float movementSpeed = 0.1f;  //���E�p

    //�@���̊p�x
    private float angle = 0f;
    //�@�����n�߂鎞�̎���
    private float startTime;
    //�@�U��q������p�x
    [SerializeField]
    private float limitAngle = 90f;

    //�@�i��ł������
    private int direction = 1;

    //�@���[�v���������ǂ����̔���
    private bool moveOn = false;

    //�@��ԊԊu
    [SerializeField]
    private float duration = 5f;

    public float angleX;
    public float angleY;
    public float angleZ;
    public bool panelOn = false;
    public bool button = false;

    //�v���C���[��rigidbody�i�[�p�ϐ�
    new Rigidbody rigidbody;
    GameObject player;
    public GameObject panel1;
    public GameObject panel2;

    [SerializeField] Transform target;
    private float speed = 5.0f;

    public float speed2 = 100;
    [SerializeField] private Transform P;
    [Range(0.0f, 180.0f)] public float arcAngle = 60.0f;
    float Travel;
    bool Throw = false;
    bool Ysafepoint = false;

    // �~�{�^����������Ă��邩�ǂ������擾����
    bool ps4X = false;
    // �Z�{�^����������Ă��邩�ǂ������擾����
    bool ps40 = false;

    Vector3 pivot;
    Vector3 FromVector;
    Vector3 ToVector;

    //���W�w��p�̃I�u�W�F�N�g
    public GameObject pointobj;
    public GameObject ropeobj;

    void Start()
    {
        startTime = Time.time;

        //�v���C���[��������
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = player.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerpos = player.transform.position;
        Vector3 setpos = pointobj.transform.position;
        Vector3 ropepos = ropeobj.transform.position;
        float arie = Vector3.Distance(setpos, ropepos);
        float Parie = Vector3.Distance(playerpos, ropepos);
        if(Parie <= 8f)
        {
            panel1.SetActive(true);
        }
        else if(Parie > 8f || panelOn == true)
        {
            panel1.SetActive(false);
        }
        //���W�w��p�I�u�W�F�N�g�ƈ��̋����ɂȂ���������𖞂���
        if (arie < 10f)
        {
            Ysafepoint = true;
        }
        if(arie > 10f)
        {
            Ysafepoint =false;
        }
        if(panelOn == true)
        {
            panel2.SetActive(true);
        }
        else
        {
            panel2.SetActive(false);
        }

        GetPS40();
        GetPS4X();

        //X�{�^������������Z�{�^����ł�����
        if(ps4X)
        {
            ps40 = false;
        }

        if (moveOn == true && (Input.GetKey(KeyCode.Space) || ps40))  //����
        {
            panelOn = true;
            //�@�o�ߎ��Ԃɍ��킹���������v�Z
            float t = (Time.time - startTime) / duration;

            //�@�X���[�Y�Ɋp�x���v�Z
            angle = Mathf.SmoothStep(angle, direction * limitAngle, t);

            //���[�v�̉��̂ق��Ɉړ�����
            player.transform.position = Vector3.MoveTowards(player.transform.position, target.position, speed * Time.deltaTime);

            //CharacterMovement();  //���E

        }
        else  //�~�܂�
        {
            if (moveOn == true)
            {
                panelOn = false;
                moveOn = false;

                //�d�͂𕜊�������
                rigidbody.isKinematic = false;

                //�e�q�֌W������
                player.gameObject.transform.parent = null;
                //�v���C���[�𔭎�
                if(Ysafepoint == true)
                {
                    Throw = true;
                }
            }
            if (angle >= -1f && angle <= 1f)
            {
                angle = 0f;
                direction = 1;
            }
            else
            {
                if (angle > 0f)
                {
                    angle -= 1f;  //���̈ʒu�ɖ߂�
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

        if (Throw == true)
        {
            // �ڕW�n�_��ݒ�
            Vector3 targetpos = new Vector3(targetX, targetY, targetZ);

            var targethalfAngle = Mathf.Tan(Mathf.Deg2Rad * arcAngle * 0.5f);
            var midPos = (playerpos + targetpos) * 0.5f;
            var half = Vector3.Distance(playerpos, midPos);

            pivot = midPos;
            pivot.y -= half / targethalfAngle;

            //���S����o���n�A���S����ړI�n�ւ̃x�N�g�������߂Ă���
            FromVector = playerpos - pivot;
            ToVector = targetpos - pivot;

            //�ړ��ʂ�0.0�Ƀ��Z�b�g���Ă���
            Travel = 0.0f;

            Travel += speed2 * Time.deltaTime;
            Travel += speed2 * Time.deltaTime;

            //�~�ʂ̒����Ŋ����āA�~�ʏ��i�s�������������߂�
            var t = Travel / (FromVector.magnitude * Mathf.Deg2Rad * arcAngle);

            if (t < 1.0f)
            {
                //FromVector��ToVector��i�s�����ŕ�Ԃ��APivot�𑫂��Č��݂̈ʒu�Ƃ���
                P.transform.position = Vector3.Slerp(FromVector, ToVector, t) + pivot;
            }
            else
            {
                //t��1.0�ɓ��B������ړ��I���Ƃ���
                P.transform.position = ToVector + pivot;
                Throw = false;
            }

        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            GetPS40();
            GetPS4X();

            if (Input.GetKey(KeyCode.Space) || ps40)
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

    void GetPS40()
    {
        if (Gamepad.current != null)
        {
            if (ps40 == false && Gamepad.current.buttonEast.isPressed)
            {
                ps40 = true;
            }
            else if (ps40 == true && Gamepad.current.buttonEast.isPressed)
            {
                ps40 = false;
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