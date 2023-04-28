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
        //�X�e�[�g�̒l���ύX���ꂽ����s�������s��
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
        Debug.Log("State��Idle�ɏ�ԑJ�ڂ��܂����B");
    }
    public void Repop()
    {
        Debug.Log("State��Repop�ɏ�ԑJ�ڂ��܂����B");
    }
    public void Crash()
    {
        Debug.Log("State��Crash�ɏ�ԑJ�ڂ��܂����B");
    }
}
