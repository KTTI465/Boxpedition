using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// キャラクターの状態（ステート）

namespace ClimbState
{
    //ステートの実行を管理するクラス
    public class ClimbStateProcessor
    {
        //ステート本体
        private ClimbState _State;
        public ClimbState State
        {
            set { _State = value; }
            get { return _State; }
        }

        //実行ブリッジ
        public void Execute() => State.Execute();
    }


    //ステートのクラス
    public abstract class ClimbState
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

    public class ClimbStateIdle : ClimbState
    {
        public override string GetStateName()
        {
            return "State:Idle";
        }
    }

    //移動状態
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
