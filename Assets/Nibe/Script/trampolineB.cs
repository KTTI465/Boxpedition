using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampolineB : MonoBehaviour
{
    Rigidbody rigidBody;
    Rigidbody playerRigidBody;
    GameObject player;

    public float jumpPower;  //ジャンプ力


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
            if (Input.GetKey(KeyCode.Space))  //スペースキーが押してあるとき
            {
                playerRigidBody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);  //上に飛ぶ
            }
        }
    }
}