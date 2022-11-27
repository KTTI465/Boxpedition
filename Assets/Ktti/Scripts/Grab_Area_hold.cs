using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab_Area_hold : MonoBehaviour
{
    bool hold_flg = false;

    public GameObject player;

    Rigidbody rb;

    GameObject _gameObject;

    void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && hold_flg)
        {
            rb.isKinematic = true;

            player.transform.parent = _gameObject.transform;
        }
        else
        {
            rb.isKinematic = false;

            player.transform.parent = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //オブジェクトの接触フラグ
        if (other.CompareTag("H_Object"))
        {
            hold_flg = true;
            _gameObject = other.gameObject;
            Debug.Log(hold_flg);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("H_Object"))
        {
            hold_flg = false;
            _gameObject = null;
            Debug.Log(hold_flg);
        }
    }
}
