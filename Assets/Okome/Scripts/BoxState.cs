using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// �L�����N�^�[�̏�ԁi�X�e�[�g�j

namespace BoxState
{
    /// <summary>
    /// �X�e�[�g�̎��s���Ǘ�����N���X
    /// </summary>
    public class BoxStateProcessor
    {
        //�X�e�[�g�{��
        private BoxState _State;
        public BoxState State
        {
            set { _State = value; }
            get { return _State; }
        }

        //���s�u���b�W
        public void Execute() => State.Execute();
    }

    /// <summary>
    /// �X�e�[�g�̃N���X
    /// </summary>
    public abstract class BoxState
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

    //���̑ҋ@�̏��
    public class BoxStateIdle : BoxState
    {
        public override string GetStateName()
        {
            return "State:BoxIdle";
        }
    }

    //���̐����̏��
    public class BoxStateRepop : BoxState
    {
        public override string GetStateName()
        {
            return "State:BoxRepop";
        }
    }
        
    //���̔����̏��
    public class BoxStateCrash : BoxState
    {
        public override string GetStateName()
        {
            return "State:Crash";
        }
    }
}
