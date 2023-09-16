using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// キャラクターの状態（ステート）

namespace RopeState
{
    //ステートの実行を管理するクラス
    public class RopeStateProcessor
    {
        //ステート本体
        private RopeState _State;
        public RopeState State
        {
            set { _State = value; }
            get { return _State; }
        }

        //実行ブリッジ
        public void Execute() => State.Execute();
    }


    //ステートのクラス
    public abstract class RopeState
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
    //トランポリンが小ジャンプの状態
}
