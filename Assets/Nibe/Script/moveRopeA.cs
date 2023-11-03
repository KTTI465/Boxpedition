using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;


// ターザン用のロープ

public class moveRopeA : MonoBehaviour
{
    private float xMovement, zMovement;
    private float movementSpeed = 0.1f;  // 相殺用

    // 軸の角度
    private float angle = 0f;
    // 動き始める時の時間
    private float startTime;
    // 振り子をする角度
    [SerializeField]
    private float limitAngle = 90f;

    // 進んでいる方向
    private int direction = 1;

    // ロープが動くかどうかの判定
    private bool moveOn = false;

    // 補間間隔
    [SerializeField]
    private float duration = 5f;

    public float angleX;
    public float angleY;
    public float angleZ;

    // プレイヤーのrigidbody格納用変数
    new Rigidbody rigidbody;
    GameObject player;

    [SerializeField] Transform target;
    private float speed = 5.0f;

    // ×ボタンが押されているかどうかを取得する
    bool ps4X = false;


    void Start()
    {
        startTime = Time.time;

        //プレイヤーを見つける
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = player.gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetPS4X();

        if (moveOn == true && (Input.GetKey(KeyCode.Space) || ps4X))  //動く
        {
            // 経過時間に合わせた割合を計算
            float t = (Time.time - startTime) / duration;

            // スムーズに角度を計算
            angle = Mathf.SmoothStep(angle, direction * limitAngle, t);

            // ロープの下のほうに移動する
            player.transform.position = Vector3.MoveTowards(player.transform.position, target.position, speed * Time.deltaTime);

            //CharacterMovement();  //相殺
        }
        else  //止まる
        {
            if(moveOn == true)
            {
                moveOn = false;

                //重力を復活させる
                rigidbody.isKinematic = false;

                //親子関係を解除
                player.gameObject.transform.parent = null;
            }

            if(angle >= -1f && angle <= 1f)
            {
                angle = 0f;
                direction = 1;
            }
            else
            {
                if (angle > 0f)
                {
                    angle -= 0.1f;  //元の位置に戻す
                }

                if (angle < 0f)
                {
                    angle += 0.1f;  //元の位置に戻す
                }
            }
        }


        //　角度を変更
        transform.localEulerAngles = new Vector3(angleX * angle, angleY * angle, angleZ * angle);
        //　角度が指定した角度と1度の差になったら反転
        if (Mathf.Abs(Mathf.DeltaAngle(angle, direction * limitAngle)) < 1f)
        {
            direction *= -1;
            startTime = Time.time;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            GetPS4X();

            if (Input.GetKey(KeyCode.Space) || ps4X)
            {
                moveOn = true;

                startTime = Time.time;

                //Rigidbodyを停止
                rigidbody.velocity = Vector3.zero;

                //重力を停止させる
                rigidbody.isKinematic = true;

                //親子関係にする
                player.gameObject.transform.parent = this.gameObject.transform;
            }
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