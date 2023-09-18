using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MonsterState
{

    //�X�e�[�g�̎��s���Ǘ�����N���X
    public class MonsterStateProcessor
    {
        //�X�e�[�g�{��
        private MonsterState _State;
        public MonsterState State
        {
            set { _State = value; }
            get { return _State; }
        }

        //���s�u���b�W
        public void Execute() => State.Execute();
    }


    //�X�e�[�g�̃N���X
    public abstract class MonsterState
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

    public class MonsterStateIdle : MonsterState
    {
        public override string GetStateName()
        {
            return "State:MonsterIdle";
        }
    }

    public class MonsterStateRun : MonsterState
    {
        public override string GetStateName()
        {
            return "State:MonsterRun";
        }
    }

    public class MonsterStateJump : MonsterState
    {
        public override string GetStateName()
        {
            return "State:MonsterJump";
        }
    }
}