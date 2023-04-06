using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using static UnityEditor.PlayerSettings;

public class trampolineC : MonoBehaviour
{
    Rigidbody rigidBody;
    Rigidbody playerRigidBody;
    GameObject player;

    public float bigJumpPower;  //大ジャンプ
    public float smallJumpPower;  //小ジャンプ

    // ×ボタンが押されているかどうかを取得する
    bool ps4X = false;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        //プレイヤーをタグで検索し、Rigidbodyを取得
        player = GameObject.FindGameObjectWithTag("Player");
        playerRigidBody = player.GetComponent<Rigidbody>();
    }

    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        //　衝突したゲームオブジェクトのタグがPlayerのとき処理を行う
        if (other.gameObject.tag == "Player")
        {
            GetPS4X();

            if (Input.GetKey(KeyCode.Space) || ps4X)  //スペースキーが押してあるとき
            {
                playerRigidBody.AddForce(Vector3.up * bigJumpPower, ForceMode.Impulse);  //大ジャンプ
            }
            else
            {
                playerRigidBody.AddForce(Vector3.up * smallJumpPower, ForceMode.Impulse);  //小ジャンプ
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