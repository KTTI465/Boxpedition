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
        //�X�e�[�g�̒l���ύX���ꂽ����s�������s��
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
        //Debug.Log("BoxState��Idle�ɏ�ԑJ�ڂ��܂����B");
    }
    public void Repop()
    {
        //Debug.Log("BoxState��Repop�ɏ�ԑJ�ڂ��܂����B");
    }
    public void Crash()
    {
        //Debug.Log("BoxState��Crash�ɏ�ԑJ�ڂ��܂����B");
    }

    private void CheckMove()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            boxAnimator.SetBool("walk", false); // �A�j���[�V�����؂�ւ�
        }
        else
        {
            boxAnimator.SetBool("walk", true); // �A�j���[�V�����؂�ւ�
        }
    }

    public Animator GetAnim()
    {
        return boxAnimator;
    }
}
