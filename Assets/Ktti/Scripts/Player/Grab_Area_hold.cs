using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab_Area_hold : MonoBehaviour
{
    bool hold_flg = false; //接触判定フラグ

    public GameObject player; //playerオブジェクト変数

    Rigidbody rb;

    GameObject _gameObject;

    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        //接触判定がtrueの時に実行
        if (Input.GetMouseButton(0) && hold_flg)
        {
            //Rigidbodyを無効化し、掴んでいるオブジェクトを親に設定
            rb.isKinematic = true;

            player.transform.parent = _gameObject.transform;
        }
        else
        {
            //Rigidbodyを有効化し、掴んでいるオブジェクトを親から外す
            rb.isKinematic = false;

            player.transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //接触判定をtrueに
        if (other.CompareTag("H_Object"))
        {
            hold_flg = true;
            _gameObject = other.gameObject;
            Debug.Log(hold_flg);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //接触判定をfalseに
        if (other.CompareTag("H_Object"))
        {
            hold_flg = false;
            _gameObject = null;
            Debug.Log(hold_flg);
        }
    }
}
