using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoxState;

public class Box : MonoBehaviour
{
    private string _preStateName;
    public BoxStateProcessor StateProcessor { get; set; } = new BoxStateProcessor();
    public BoxStateIdle StateIdle { get; set; } = new BoxStateIdle();
    public BoxStateRepop StateRepop { get; set; } = new BoxStateRepop();

    public BoxStateCrash StateCrash { get; set; } = new BoxStateCrash();

    private Animator boxAnimator;

    void Start()
    {
        StateIdle.ExecAction = Idle;
        StateRepop.ExecAction = Repop;
        StateCrash.ExecAction = Crash;
        StateProcessor.State = StateRepop;

        boxAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        //ステートの値が変更されたら実行処理を行う
        if (StateProcessor.State.GetStateName() != _preStateName)
        {
            _preStateName = StateProcessor.State.GetStateName();
            StateProcessor.Execute();
        }

        if (StateProcessor.State == StateRepop)
        {
            StateProcessor.State = StateIdle;
        }

        CheckMove();
    }

    public IEnumerator DestroyBox()
    {
        yield return new WaitForSeconds(1);
        StateProcessor.State = StateCrash;
        yield return new WaitForSeconds(0.01f);
        Destroy(gameObject);
    }
    public void Idle()
    {
        //Debug.Log("BoxStateがIdleに状態遷移しました。");
    }
    public void Repop()
    {
        //Debug.Log("BoxStateがRepopに状態遷移しました。");
    }
    public void Crash()
    {
        //Debug.Log("BoxStateがCrashに状態遷移しました。");
    }

    private void CheckMove()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            boxAnimator.SetBool("walk", false); // アニメーション切り替え
        }
        else
        {
            boxAnimator.SetBool("walk", true); // アニメーション切り替え
        }
    }

    public Animator GetAnim()
    {
        return boxAnimator;
    }
}
