using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grab_Area_Script : MonoBehaviour
{
    //�͂�ł��邩�̔���t���O
    public bool grabFlg = false;

    //��������ɒ͂ޔ���ɂȂ�Ȃ��悤�ɂ���t���O
    bool firstFlg = false;

    //�͂񂾃I�u�W�F�N�g��rigidbody�i�[�p�ϐ�
    Rigidbody rigidbody;

    //�������
    public float power = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
        }
        else
        {
            //���܂ɔ���o�O�Őe�q�֌W��������Ȃ����߁A�����G�ɐe�q�֌W��������
            //(���̏ꍇ�d�͕͂������Ȃ������H)
            this.gameObject.transform.DetachChildren();

            //�����I�ɔ����true�ɂ���
            grabFlg = false;
        }

        if (Input.GetMouseButtonDown(1) && grabFlg)
        {
            var vec = (transform.parent.position - transform.position) + new Vector3(0f, 1f, 0f);

            Debug.Log(vec.normalized);

            rigidbody.AddForce(vec.normalized * power);

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

                    //�����true�ɂ���
                    grabFlg = true;
                }
                else
                {
                    rigidbody.isKinematic = false;

                    collision.gameObject.transform.parent = null;
                }
            }
            else
            {
                //�d�͂𕜊�������
                rigidbody.isKinematic = false;

                //�e�q�֌W������
                collision.gameObject.transform.parent = null;

                //�͂ޔ����false�ɂ���
                grabFlg = false;

                //�����锻���false�ɂ���
                firstFlg = false;
            }
        }
    }
}
