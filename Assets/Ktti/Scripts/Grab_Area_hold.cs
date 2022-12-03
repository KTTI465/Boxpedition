using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab_Area_hold : MonoBehaviour
{
    bool hold_flg = false; //�ڐG����t���O

    public GameObject player; //player�I�u�W�F�N�g�ϐ�

    Rigidbody rb;

    GameObject _gameObject;

    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //�ڐG���肪true�̎��Ɏ��s
        if (Input.GetMouseButton(0) && hold_flg)
        {
            //Rigidbody�𖳌������A�͂�ł���I�u�W�F�N�g��e�ɐݒ�
            rb.isKinematic = true;

            player.transform.parent = _gameObject.transform;
        }
        else
        {
            //Rigidbody��L�������A�͂�ł���I�u�W�F�N�g��e����O��
            rb.isKinematic = false;

            player.transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //�ڐG�����true��
        if (other.CompareTag("H_Object"))
        {
            hold_flg = true;
            _gameObject = other.gameObject;
            Debug.Log(hold_flg);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //�ڐG�����false��
        if (other.CompareTag("H_Object"))
        {
            hold_flg = false;
            _gameObject = null;
            Debug.Log(hold_flg);
        }
    }
}
