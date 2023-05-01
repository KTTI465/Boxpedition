using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// キャラクターの状態（ステート）

namespace BoxState
{
    //ステートの実行を管理するクラス
    public class BoxStateProcessor
    {
        //ステート本体
        private BoxState _State;
        public BoxState State
        {
            set { _State = value; }
            get { return _State; }
        }

        //実行ブリッジ
        public void Execute() => State.Execute();
    }


    //ステートのクラス
    public abstract class BoxState
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

    //箱の待機の状態
    public class BoxStateIdle : BoxState
    {
        public override string GetStateName()
        {
            return "State:BoxIdle";
        }
    }

    //箱の生成の状態
    public class BoxStateRepop : BoxState
    {
        public override string GetStateName()
        {
            return "State:BoxRepop";
        }
    }
        
    //箱の爆発の状態
    public class BoxStateCrash : BoxState
    {
        public override string GetStateName()
        {
            return "State:Crash";
        }
    }
}
