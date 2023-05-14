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
    private bool isGrabRope = false;

    //上っているかどうかの判定
    private bool isClimbing = false;

    //プレイヤーのrigidbody格納用変数
    new Rigidbody rigidbody;
    GameObject player;

    //プレイヤーがロープを上るスピード
    public  float moveSpeed = 5.0f;

    //プレイヤーがロープを上る距離
    public float climbDistance = 10f;

    private CharacterController characterController;
    private Vector3 climbPos;

    // ×ボタンが押されているかどうかを取得する
    bool ps4X = false;

    void Start()
    {
        //プレイヤーを見つける
        player = GameObject.FindGameObjectWithTag("Player");
        rigidbody = player.gameObject.GetComponent<Rigidbody>();
        characterController = player.gameObject.GetComponent<CharacterController>();

        //for(int i = 0; dist < distace)
    }

    // Update is called once per frame
    void Update()
    {
        GetPS4X();
        if (moveOn == true)  //登る
        {
            GameObject _rayHitObject = characterController.rayHitObject.collider.gameObject;
            if (_rayHitObject == gameObject && (Input.GetKeyDown(KeyCode.Space) || ps4X))
            {

                if (isGrabRope == false)
                {
                    isGrabRope = true;

                    //Rigidbodyを停止
                    rigidbody.velocity = Vector3.zero;
                }
            }
        }

        if (isClimbing == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) || ps4X)
            {
                climbPos = new Vector3(player.transform.position.x, player.transform.position.y + climbDistance, player.transform.position.z);
                isClimbing = true;
            }
        }



        if (isGrabRope == true)
        {
            //重力を停止させる
            rigidbody.isKinematic = true;

            characterController.enabled = false;
            //CharacterMovement();  //相殺

            if (player.transform.position.y > transform.position.y)
            {
                climbPos = player.transform.position;
                isClimbing = false;
                characterController.enabled = true;
            }

            if (isClimbing)
            {
                if (player.transform.position == climbPos)
                {
                    isClimbing = false;
                }
                else
                {
                    player.transform.position = Vector3.MoveTowards(player.transform.position, climbPos, moveSpeed * Time.deltaTime);
                }
            }
        }
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
            if (isGrabRope && player.transform.position.y > transform.position.y)
            {
                //重力を復活させる
                rigidbody.isKinematic = false;
                isGrabRope = false;
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