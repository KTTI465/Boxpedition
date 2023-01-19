using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampolineC : MonoBehaviour
{
    Rigidbody rigidBody;
    Rigidbody playerRigidBody;
    GameObject player;

    public float bigJumpPower;  //大ジャンプ
    public float smallJumpPower;  //小ジャンプ


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
                playerRigidBody.AddForce(Vector3.up * bigJumpPower, ForceMode.Impulse);  //大ジャンプ
            }
            else
            {
                playerRigidBody.AddForce(Vector3.up * smallJumpPower, ForceMode.Impulse);  //小ジャンプ
            }
        }
    }
}