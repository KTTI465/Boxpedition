using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// �L�����N�^�[�̏�ԁi�X�e�[�g�j

namespace TrampolineState
{
    //�X�e�[�g�̎��s���Ǘ�����N���X
    public class TrampolineStateProcessor
    {
        //�X�e�[�g�{��
        private TrampolineState _State;
        public TrampolineState State
        {
            set { _State = value; }
            get { return _State; }
        }

        //���s�u���b�W
        public void Execute() => State.Execute();
    }


    //�X�e�[�g�̃N���X
    public abstract class TrampolineState
    {
        //�f���Q�[�g
        public Action ExecAction { get; set; }

        //���s����
        public virtual void Execute()
        {
            if (ExecAction != null) ExecAction();
        }

        //�X�e�[�g�����擾���郁�\�b�h
        public abstract string GetStateName();
    }

    //�g�����|�������ҋ@���
    public class TrampolineStateIdle : TrampolineState
    {
        public override string GetStateName()
        {
            return "State:TrampolineIdle";
        }
    }

    //�g�����|���������W�����v�̏��
    public class TrampolineStateSmallJump : TrampolineState
    {
        public override string GetStateName()
        {
            return "State:TrampolineSmallJump";
        }
    }

    //�g�����|��������W�����v�̏��
    public class TrampolineStateBigJump : TrampolineState
    {
        public override string GetStateName()
        {
            return "State:TrampolineBigJump";
        }
    }
}
