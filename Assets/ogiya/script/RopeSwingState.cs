using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace RopeSwingState
{
    
    //ステートの実行を管理するクラス
    public class RopeSwingStateProcessor
    {
        //ステート本体
        private RopeSwingState _State;
        public RopeSwingState State
        {
            set { _State = value; }
            get { return _State; }
        }

        //実行ブリッジ
        public void Execute() => State.Execute();
    }


    //ステートのクラス
    public abstract class RopeSwingState
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