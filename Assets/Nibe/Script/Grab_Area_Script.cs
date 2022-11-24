using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grab_Area_Script : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
        }
        else
        {
            //���܂ɔ���o�O�Őe�q�֌W��������Ȃ����߁A�����G�ɐe�q�֌W��������
            //(���̏ꍇ�d�͕͂������Ȃ������H)
            this.gameObject.transform.DetachChildren();
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        //P (push & pull) _ Object �̏���

        if (collision.gameObject.CompareTag("P_Object"))
        {
            Rigidbody rigidbody = collision.gameObject.GetComponent<Rigidbody>();

            // ���{�^����������Ă����畨�̂�e�q�֌W�ɂ���
            if (Input.GetMouseButton(0))
            {
                //Rigidbody���~
                rigidbody.velocity = Vector3.zero;

                //�d�͂��~������
                rigidbody.isKinematic = true;

                //�e�q�֌W�ɂ���
                collision.gameObject.transform.parent = this.gameObject.transform;
            }
            else
            {
                //�d�͂𕜊�������
                rigidbody.isKinematic = false;

                //�e�q�֌W������
                collision.gameObject.transform.parent = null;
            }
        }
    }
}
