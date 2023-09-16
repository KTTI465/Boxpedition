using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// �L�����N�^�[�̏�ԁi�X�e�[�g�j

namespace RopeState
{
    //�X�e�[�g�̎��s���Ǘ�����N���X
    public class RopeStateProcessor
    {
        //�X�e�[�g�{��
        private RopeState _State;
        public RopeState State
        {
            set { _State = value; }
            get { return _State; }
        }

        //���s�u���b�W
        public void Execute() => State.Execute();
    }


    //�X�e�[�g�̃N���X
    public abstract class RopeState
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
    public class RopeStateIdle : RopeState
    {
        public override string GetStateName()
        {
            return "State:RopeIdle";
        }
    }
    public class RopeStateSlide : RopeState
    {
        public override string GetStateName()
        {
            return "State:Slide";
        }
    }
    //�g�����|���������W�����v�̏��
}
