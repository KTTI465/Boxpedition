using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RopeSwingState
{
    
    //�X�e�[�g�̎��s���Ǘ�����N���X
    public class RopeSwingStateProcessor
    {
        //�X�e�[�g�{��
        private RopeSwingState _State;
        public RopeSwingState State
        {
            set { _State = value; }
            get { return _State; }
        }

        //���s�u���b�W
        public void Execute() => State.Execute();
    }


    //�X�e�[�g�̃N���X
    public abstract class RopeSwingState
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

    public class RopeSwingStateIdle : RopeSwingState
    {
        public override string GetStateName()
        {
            return "State:RopeSwingIdle";
        }
    }

    public class RopeSwingStateSwing : RopeSwingState
    {
        public override string GetStateName()
        {
            return "State:RopeSwingSwing";
        }
    }

    public class RopeSwingStateJump : RopeSwingState
    {
        public override string GetStateName()
        {
            return "State:RopeSwingJump";
        }
    }
}