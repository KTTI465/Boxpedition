using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grab_Area_Script : MonoBehaviour
{
    // �͂�ł��邩�̔���t���O
    public bool grabFlg = false;

    // ��������ɒ͂ޔ���ɂȂ�Ȃ��悤�ɂ���t���O
    bool firstFlg = false;

    // �͂񂾃I�u�W�F�N�g��rigidbody�i�[�p�ϐ�
    new Rigidbody rigidbody;

    // �������
    public float power = 10f;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {

        }
        else
        {
            //�����I�ɐe�q�֌W��������
            this.gameObject.transform.DetachChildren();
            grabFlg = false;
        }

        //�E�N���b�N�œ�����
        if (Input.GetMouseButtonDown(1) && grabFlg)
        {
            //����������v�Z
            var vec = (transform.position - transform.parent.position) + new Vector3(0f, 2f, 0f);
            //�������Z��L����
            rigidbody.isKinematic = false;
            //�����������addforce
            rigidbody.AddForce(vec.normalized * power, ForceMode.VelocityChange);

            grabFlg = false;
            firstFlg = true;
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        //P (push & pull) _ Object �̏���

        if (collision.gameObject.CompareTag("P_Object"))
        {
            rigidbody = collision.gameObject.GetComponent<Rigidbody>();

            // ���{�^����������Ă����畨�̂�e�q�֌W�ɂ���
            if (Input.GetMouseButton(0))
            {
                //������{�^�����������Ƃ��ɒ͂܂Ȃ��悤�ɂ���
                if (!firstFlg)
                {
                    //Rigidbody���~
                    rigidbody.velocity = Vector3.zero;

                    //�d�͂��~������
                    rigidbody.isKinematic = true;

                    //�e�q�֌W�ɂ���
                    collision.gameObject.transform.parent = this.gameObject.transform;

                    grabFlg = true;
                }
                else
                {
                    //�������Z�L��
                    rigidbody.isKinematic = false;
                    //�e�I�u�W�F�N�g�̉���
                    collision.gameObject.transform.parent = null;
                }
            }
            else
            {
                //�d�͂𕜊�������
                rigidbody.isKinematic = false;

                //�e�q�֌W������
                collision.gameObject.transform.parent = null;

                grabFlg = false;
                firstFlg = false;
            }
        }
    }
}
