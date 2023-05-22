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

    //ロープを掴んでいるかどうかの判定
    private bool grabbingRope = false;

    //上っているかどうかの判定
    private bool climbing = false;
    //下っているかどうかの判定
    private bool climbingDown = false;

    //プレイヤーのrigidbody格納用変数
    new Rigidbody rigidbody;
    GameObject player;

    //プレイヤーがロープを上るスピード
    public float moveSpeed = 5.0f;

    //プレイヤーがロープを移動する距離
    public float ropeMoveDistance = 10f;

    //PlayerのCharacterControllerを格納
    private CharacterController characterController;

    //ロープをうごく先の位置
    private Vector3 climbPos;

    //キャラクターの位置を補正する値
    private float positionCorrection = 1.2f;

    //ロープの上から下までの距離
    private float ropeDistance;

    //Rayが当たるものが無いときのロープの長さ
    public float length;

    //下の位置
    private Vector3 underPosition;

    //ロープを掴んでいる判定をするColliderを格納
    public CapsuleCollider grabRopeCollider;

    //ロープにつかめる判定をするColliderを格納
    public CapsuleCollider moveOnCollider;

    //Playerのジャンプ用のrayをとめるオブジェクトを格納
    public GameObject rayStopper;

    private GameObject _rayHitObject;

    // ×ボタンが押されているかどうかを取得する
    bool ps4X = false;
    // yボタンが押されているかどうかを取得する
    bool ps4Y = false;

    void Start()
    {
        //プレイヤーを見つける
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = player.gameObject.GetComponent<Rigidbody>();
        characterController = player.gameObject.GetComponent<CharacterController>();

        //下の位置と下までの距離を設定
        if (Physics.Raycast(transform.position, Vector3.up * -1f, out RaycastHit hit))
        {
            underPosition = hit.point;
        }
        else
        {
            //Rayが当たるものが無いときのunderPositionの指定
            underPosition = transform.position - (transform.up * length) + (transform.up * positionCorrection);
        }
        ropeDistance = Vector3.Distance(transform.position, underPosition);

        //Colliderの大きさや位置を指定
        moveOnCollider.center = -(transform.position - new Vector3(0, transform.up.y * (ropeDistance / 2), 0));
        grabRopeCollider.center = -(transform.position - new Vector3(0, transform.up.y * (ropeDistance / 2), 0));
        moveOnCollider.height = ropeDistance + moveOnCollider.radius * 1.5f;
        grabRopeCollider.height = ropeDistance;

        //レイヤ―の変更
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    // Update is called once per frame
    void Update()
    {
        GetPS4X();
        GetPS4Y();
        if (moveOn == true)  //登る
        {
            if (characterController.rayHitObject != null &&
                characterController.rayHitObject == gameObject && 
                (Input.GetKeyDown(KeyCode.Space) || ps4X))
            {
                if (grabbingRope == false)
                {
                    grabbingRope = true;

                    //Rigidbodyを停止
                    rigidbody.velocity = Vector3.zero;
                }
            }
        }
        else if (moveOn == false && grabbingRope)
        {
            //重力を復活させる
            rigidbody.isKinematic = false;
            grabbingRope = false;
        }

        if (grabbingRope == true)
        {
            //重力を停止させる
            rigidbody.isKinematic = true;

            rayStopper.transform.position = player.transform.position - player.transform.up * 1.3f;

            //上
            if (player.transform.position.y < transform.position.y + positionCorrection)
            {
                if (climbing == false && climbingDown == false &&
                    Input.GetKeyDown(KeyCode.Space) || ps4X)
                {
                    climbPos = new Vector3(player.transform.position.x, player.transform.position.y + ropeMoveDistance, player.transform.position.z);
                    climbing = true;
                }
            }
            else if (player.transform.position.y >= transform.position.y + positionCorrection &&
                climbing == true)
            {
                climbPos = player.transform.position;
                characterController.enabled = true;
                climbing = false;
            }

            //下
            if (player.transform.position.y > underPosition.y + positionCorrection)
            {
                if (climbing == false && climbingDown == false &&
                    Input.GetKeyDown(KeyCode.LeftShift) || ps4Y)
                {
                    climbPos = new Vector3(player.transform.position.x, player.transform.position.y - ropeMoveDistance, player.transform.position.z);
                    climbingDown = true;
                }
            }
            else if (player.transform.position.y <= underPosition.y + positionCorrection &&
                climbingDown == true)
            {
                climbPos = player.transform.position;
                characterController.enabled = true;
                climbingDown = false;
            }

            if (climbing || climbingDown)
            {
                characterController.enabled = false;
                if (player.transform.position == climbPos)
                {
                    climbing = false;
                    climbingDown = false;
                }
                else
                {
                    player.transform.position = Vector3.MoveTowards(player.transform.position, climbPos, moveSpeed * Time.deltaTime);
                }
            }
            else
            {
                characterController.enabled = true;
            }
        }
        else
        {
            rayStopper.transform.position = new Vector3(0.0f, transform.position.y - 0.1f, 0.0f);
        }
        //CharacterMovement();  //相殺
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            moveOn = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            moveOn = false;
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

    void GetPS4Y()
    {
        if(Gamepad.current != null)
        {
            if(Gamepad.current.buttonWest.isPressed)
            {
                ps4Y = true;
            }
            else
            {
                ps4Y = false;
            }
        }           
    }
}