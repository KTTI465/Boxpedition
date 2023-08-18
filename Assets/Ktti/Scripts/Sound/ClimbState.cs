using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// �L�����N�^�[�̏�ԁi�X�e�[�g�j

namespace ClimbState
{
    //�X�e�[�g�̎��s���Ǘ�����N���X
    public class ClimbStateProcessor
    {
        //�X�e�[�g�{��
        private ClimbState _State;
        public ClimbState State
        {
            set { _State = value; }
            get { return _State; }
        }

        //���s�u���b�W
        public void Execute() => State.Execute();
    }


    //�X�e�[�g�̃N���X
    public abstract class ClimbState
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

    public class ClimbStateIdle : ClimbState
    {
        public override string GetStateName()
        {
            return "State:Idle";
        }
    }

    //�ړ����
    public class ClimbStateMove : ClimbState
    {
        public override string GetStateName()
        {
            return "State:Move";
        }
    }

    public class ClimbStateClimb : ClimbState
    {
        public override string GetStateName()
        {
            return "State:Climb";
        }
    }
}
