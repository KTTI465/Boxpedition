using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// �L�����N�^�[�̏�ԁi�X�e�[�g�j

namespace CharacterState
{
    /// <summary>
    /// �X�e�[�g�̎��s���Ǘ�����N���X
    /// </summary>
    public class CharacterStateProcessor
    {
        //�X�e�[�g�{��
        private CharacterState _State;
        public CharacterState State
        {
            set { _State = value; }
            get { return _State; }
        }

        //���s�u���b�W
        public void Execute() => State.Execute();
    }

    //�X�e�[�g�̃N���X
    public abstract class CharacterState
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

    //�ҋ@���
    public class CharacterStateIdle : CharacterState
    {
        public override string GetStateName()
        {
            return "State:Idle";
        }
    }

    //�ړ����
    public class CharacterStateMove : CharacterState
    {
        public override string GetStateName()
        {
            return "State:Move";
        }
    }
    public class CharacterStateMoveGlass : CharacterState
    {
        public override string GetStateName()
        {
            return "State:MoveGlass";
        }
    }

    public class CharacterStateMoveMat : CharacterState
    {
        public override string GetStateName()
        {
            return "State:MoveMat";
        }
    }

    //�W�����v���
    public class CharacterStateJump1 : CharacterState
    {
        public override string GetStateName()
        {
            return "State:Jump1";
        }
    }

    //�W�����v���
    public class CharacterStateJump2 : CharacterState
    {
        public override string GetStateName()
        {
            return "State:Jump2";
        }
    }

    //�~�X���
    public class CharacterStateMiss : CharacterState
    {
        public override string GetStateName()
        {
            return "State:Miss";
        }
    }

    //�~�X���
    public class CharacterStateCatch : CharacterState
    {
        public override string GetStateName()
        {
            return "State:Catch";
        }
    }

    //�g�����|�������W�����v
    public class CharacterStateTrampolineSmallJump : CharacterState
    {
        public override string GetStateName()
        {
            return "State:TrampolineSmallJump";
        }
    }

    //�g�����|������W�����v
    public class CharacterStateTrampolineBigJump : CharacterState
    {
        public override string GetStateName()
        {
            return "State:TrampolineBigJump";
        }
    }
}
