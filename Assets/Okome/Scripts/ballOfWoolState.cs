using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// キャラクターの状態（ステート）

namespace ballOfWoolState
{
    /// <summary>
    /// ステートの実行を管理するクラス
    /// </summary>
    public class ballOfWoolStateProcessor
    {
        //ステート本体
        private ballOfWoolState _State;
        public ballOfWoolState State
        {
            set { _State = value; }
            get { return _State; }
        }

        //実行ブリッジ
        public void Execute() => State.Execute();
    }

    //ステートのクラス
    public abstract class ballOfWoolState
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

    //毛糸玉の待機の状態
    public class ballOfWoolStateIdle : ballOfWoolState
    {
        public override string GetStateName()
        {
            return "State:ballOfWoolIdle";
        }
    }

    //箱の生成の状態
    public class ballOfWoolStateAnimation : ballOfWoolState
    {
        public override string GetStateName()
        {
            return "State:ballOfWoolAnimation";
        }
    }
}

