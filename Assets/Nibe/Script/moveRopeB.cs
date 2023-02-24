using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class moveRopeB : MonoBehaviour
{
    private float xMovement, zMovement;
    private float movementSpeed = 0.05f;  //相殺用

    //接触したかどうかの判定
    private bool moveOn = false;

    //プレイヤーのrigidbody格納用変数
    new Rigidbody rigidbody;
    GameObject player;

    private float speed = 5.0f;


    void Start()
    {
        //プレイヤーを見つける
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = player.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveOn == true && Input.GetKey(KeyCode.Space))  //登る
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, this.transform.position, speed * Time.deltaTime);
            CharacterMovement();  //相殺
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
}