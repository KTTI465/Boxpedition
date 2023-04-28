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
    void Start()
    {
        StateProcessor.State = StateIdle;
        StateIdle.ExecAction = Idle;
        StateRepop.ExecAction = Repop;
        StateCrash.ExecAction = Crash;

    }
    void Update()
    {
        //ステートの値が変更されたら実行処理を行う
        if (StateProcessor.State.GetStateName() != _preStateName)
        {
            Debug.Log(" Now State:" + StateProcessor.State.GetStateName());
            _preStateName = StateProcessor.State.GetStateName();
            StateProcessor.Execute();
        }
    }
    public IEnumerator DestroyBox()
    {
        yield return new WaitForSeconds(1);

        Destroy(gameObject);
    }
    public void Idle()
    {
        Debug.Log("StateがIdleに状態遷移しました。");
    }
    public void Repop()
    {
        Debug.Log("StateがRepopに状態遷移しました。");
    }
    public void Crash()
    {
        Debug.Log("StateがCrashに状態遷移しました。");
    }
}
