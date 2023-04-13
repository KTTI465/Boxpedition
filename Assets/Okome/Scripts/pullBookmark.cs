using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pullBookmark : MonoBehaviour
{
    //�͂�ł��邩�̔���t���O
    public bool grabFlg;

    //�͂񂾃I�u�W�F�N�g��rigidbody�i�[�p�ϐ�
    new Rigidbody rigidbody;

    private void Start()
    {
        grabFlg = false;
    }
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == ("bookmark"))
        {
            //�����true�ɂ���
            grabFlg = true;
            rigidbody = other.gameObject.GetComponent<Rigidbody>();

            // ���{�^����������Ă����畨�̂�e�q�֌W�ɂ���
            if (Input.GetMouseButton(1) && grabFlg == true)
            {
                //Rigidbody���~
                rigidbody.velocity = Vector3.zero;

                //�����Ă���Ƃ��ɉ��ɗ����Ȃ��悤�ɂ���
                rigidbody.constraints = RigidbodyConstraints.FreezePositionY;
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

                //�e�q�֌W�ɂ���
                other.gameObject.transform.parent = gameObject.transform;

                Vector3 localPos = other.gameObject.transform.localPosition;
                if (other.gameObject.transform.localPosition.y < -0.3)
                {
                    localPos.y = -0.3f;
                }
                other.gameObject.transform.localPosition = localPos;
            }
            else
            {

                //�e�q�֌W������
                other.gameObject.transform.parent = null;

                //���ɗ�����悤�ɂ���
                rigidbody.constraints = RigidbodyConstraints.None;
                rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

                //�͂ޔ����false�ɂ���
                grabFlg = false;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == ("bookmark"))
        {
            //�x�Ǝ�̋����������Ȃ����痣���悤�ɂ���         
            grabFlg = false;
            other.gameObject.transform.parent = null;
        }
    }
}
