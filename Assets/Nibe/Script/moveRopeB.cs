using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using static UnityEngine.GraphicsBuffer;
using static UnityEditor.PlayerSettings;

public class moveRopeB : MonoBehaviour
{
    private float xMovement, zMovement;
    private float movementSpeed = 0.1f;  //相殺用

    //接触したかどうかの判定
    private bool moveOn = false;

    //プレイヤーのrigidbody格納用変数
    new Rigidbody rigidbody;
    GameObject player;

    private float speed = 5.0f;

    // ×ボタンが押されているかどうかを取得する
    bool ps4X = false;


    void Start()
    {
        //プレイヤーを見つける
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = player.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetPS4X();

        if (moveOn == true && (Input.GetKey(KeyCode.Space) || ps4X))  //登る
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, this.transform.position, speed * Time.deltaTime);
            //CharacterMovement();  //相殺
        }
        else
        {
            if (moveOn == true)
            {
                moveOn = false;

                //重力を復活させる
                rigidbody.isKinematic = false;

                //親子関係を解除
                player.gameObject.transform.parent = null;
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            moveOn = true;

            //Rigidbodyを停止
            rigidbody.velocity = Vector3.zero;

            //重力を停止させる
            rigidbody.isKinematic = true;

            //親子関係にする
            player.gameObject.transform.parent = this.gameObject.transform;
        }
    }

    private void CharacterMovement()
    {
        xMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        zMovement = Input.GetAxisRaw("Vertical") * movementSpeed;

        player.transform.Translate(-xMovement, 0, -zMovement);  //相殺するために逆向きに力加える
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