using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// �L�����N�^�[�̏�ԁi�X�e�[�g�j

namespace DrawerState
{
    //�X�e�[�g�̎��s���Ǘ�����N���X
    public class DrawerStateProcessor
    {
        //�X�e�[�g�{��
        private DrawerState _State;
        public DrawerState State
        {
            set { _State = value; }
            get { return _State; }
        }

        //���s�u���b�W
        public void Execute() => State.Execute();
    }


    //�X�e�[�g�̃N���X
    public abstract class DrawerState
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
    public class DrawerStateIdle : DrawerState
    {
        public override string GetStateName()
        {
            return "State:DrawerIdle";
        }
    }

    //�g�����|���������W�����v�̏��
    public class DrawerStatePull : DrawerState
    {
        public override string GetStateName()
        {
            return "State:DrawerPull";
        }
    }

}
