using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MonsterState
{

    //ステートの実行を管理するクラス
    public class MonsterStateProcessor
    {
        //ステート本体
        private MonsterState _State;
        public MonsterState State
        {
            set { _State = value; }
            get { return _State; }
        }

        //実行ブリッジ
        public void Execute() => State.Execute();
    }


    //ステートのクラス
    public abstract class MonsterState
    {
        //デリゲート
        public Action ExecAction { get; set; }

        //実行処理
        public virtual void Execute()
        {
            if (ExecAction != null) ExecAction();
        }

        //ステート名を取得するメソッド
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