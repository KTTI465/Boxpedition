using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// キャラクターの状態（ステート）

namespace TrampolineState
{
    //ステートの実行を管理するクラス
    public class TrampolineStateProcessor
    {
        //ステート本体
        private TrampolineState _State;
        public TrampolineState State
        {
            set { _State = value; }
            get { return _State; }
        }

        //実行ブリッジ
        public void Execute() => State.Execute();
    }


    //ステートのクラス
    public abstract class TrampolineState
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

    //トランポリンが待機状態
    public class TrampolineStateIdle : TrampolineState
    {
        public override string GetStateName()
        {
            return "State:TrampolineIdle";
        }
    }

    //トランポリンが小ジャンプの状態
    public class TrampolineStateSmallJump : TrampolineState
    {
        public override string GetStateName()
        {
            return "State:TrampolineSmallJump";
        }
    }

    //トランポリンが大ジャンプの状態
    public class TrampolineStateBigJump : TrampolineState
    {
        public override string GetStateName()
        {
            return "State:TrampolineBigJump";
        }
    }
}
