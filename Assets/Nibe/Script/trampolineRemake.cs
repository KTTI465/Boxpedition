using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using CharacterState;
using TrampolineState;

public class trampolineRemake : MonoBehaviour
{
    public float bounciness = 0.8f; // 反発係数の値
    private PhysicMaterialCombine frictionCombine = PhysicMaterialCombine.Maximum; // 摩擦結合モードの値
    private PhysicMaterialCombine bounceCombine = PhysicMaterialCombine.Maximum; // 反発結合モードの値

    private new Collider collider;
    private new Rigidbody rigidbody;


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


    // Start is called before the first frame update
    void Start()
    {
        this.collider = this.GetComponent<Collider>();
        this.rigidbody = this.GetComponent<Rigidbody>();

        var newMaterial = new PhysicMaterial();

        newMaterial.bounciness = bounciness;
        newMaterial.frictionCombine = frictionCombine;
        newMaterial.bounceCombine = bounceCombine;

        this.collider.sharedMaterial = newMaterial;



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

    void OnCollisionEnter(Collision other)
    {
        //　衝突したゲームオブジェクトのタグがPlayerのとき処理を行う
        if (other.gameObject.tag == "Player")
        {
            StateProcessor.State = StateSmallJump;
        }
    }

    void OnCollisionExit(Collision other)
    {
        //　衝突したゲームオブジェクトのタグがPlayerのとき処理を行う
        if (other.gameObject.tag == "Player")
        {
            StateProcessor.State = StateIdle;
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
