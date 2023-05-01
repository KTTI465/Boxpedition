using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// �L�����N�^�[�̏�ԁi�X�e�[�g�j

namespace ballOfWoolState
{
    /// <summary>
    /// �X�e�[�g�̎��s���Ǘ�����N���X
    /// </summary>
    public class ballOfWoolStateProcessor
    {
        //�X�e�[�g�{��
        private ballOfWoolState _State;
        public ballOfWoolState State
        {
            set { _State = value; }
            get { return _State; }
        }

        //���s�u���b�W
        public void Execute() => State.Execute();
    }

    //�X�e�[�g�̃N���X
    public abstract class ballOfWoolState
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

    //�ю��ʂ̑ҋ@�̏��
    public class ballOfWoolStateIdle : ballOfWoolState
    {
        public override string GetStateName()
        {
            return "State:ballOfWoolIdle";
        }
    }

    //���̐����̏��
    public class ballOfWoolStateAnimation : ballOfWoolState
    {
        public override string GetStateName()
        {
            return "State:ballOfWoolAnimation";
        }
    }
}

