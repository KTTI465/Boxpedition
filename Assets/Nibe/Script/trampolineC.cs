using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using CharacterState;
using static UnityEditor.PlayerSettings;
using TrampolineState;

public class trampolineC : MonoBehaviour
{
    Rigidbody rigidBody;
    Rigidbody playerRigidBody;
    GameObject player;

    private string _preStateName;

    public TrampolineStateProcessor StateProcessor { get; set; } = new TrampolineStateProcessor();
    public TrampolineStateSmallJump StateSmallJump { get; set; } = new TrampolineStateSmallJump();
    public TrampolineStateBigJump StateBigJump { get; set; } = new TrampolineStateBigJump();
    public TrampolineStateIdle StateIdle { get; set; } = new TrampolineStateIdle();

    public float bigJumpPower;  //大ジャンプ
    public float smallJumpPower;  //小ジャンプ

    // ×ボタンが押されているかどうかを取得する
    bool ps4X = false;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        StateSmallJump.ExecAction = SmallJump;
        StateBigJump.ExecAction = BigJump;
        StateIdle.ExecAction = Idle;

        StateProcessor.State = StateIdle;

        //プレイヤーをタグで検索し、Rigidbodyを取得
        player = GameObject.FindGameObjectWithTag("Player");
        playerRigidBody = player.GetComponent<Rigidbody>();
    }

    void Update()
    {

        //ステートの値が変更されたら実行処理を行う
        if (StateProcessor.State.GetStateName() != _preStateName)
        {
            _preStateName = StateProcessor.State.GetStateName();
            StateProcessor.Execute();
        }
    }

    void OnTriggerStay(Collider other)
    {
        //　衝突したゲームオブジェクトのタグがPlayerのとき処理を行う
        if (other.gameObject.tag == "Player")
        {
            GetPS4X();

            if (Input.GetKey(KeyCode.Space) || ps4X)  //スペースキーが押してあるとき
            {
                //Debug.Log("大");
                StateProcessor.State = StateBigJump;
                playerRigidBody.AddForce(Vector3.up * bigJumpPower, ForceMode.Impulse);  //大ジャンプ
            }
            else
            {
                //Debug.Log("小");
                StateProcessor.State = StateSmallJump;
                playerRigidBody.AddForce(Vector3.up * smallJumpPower, ForceMode.Impulse);  //小ジャンプ
            }

        }
    }

    void OnTriggerExit(Collider other)
    {
        //　衝突したゲームオブジェクトのタグがPlayerのとき処理を行う
        if (other.gameObject.tag == "Player")
        {
                StateProcessor.State = StateIdle;   
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

    public void SmallJump()
    {
        Debug.Log("TrampolineStateがSmallJumpに状態遷移しました。");
    }
    public void BigJump()
    {
        Debug.Log("TrampolineStateがBigJumpに状態遷移しました。");
    }
    public void Idle()
    {
        Debug.Log("TrampolineStateがIdleに状態遷移しました。");
    }
}