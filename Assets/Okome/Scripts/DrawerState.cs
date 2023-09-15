using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// キャラクターの状態（ステート）

namespace DrawerState
{
    //ステートの実行を管理するクラス
    public class DrawerStateProcessor
    {
        //ステート本体
        private DrawerState _State;
        public DrawerState State
        {
            set { _State = value; }
            get { return _State; }
        }

        //実行ブリッジ
        public void Execute() => State.Execute();
    }


    //ステートのクラス
    public abstract class DrawerState
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
    public class DrawerStateIdle : DrawerState
    {
        public override string GetStateName()
        {
            return "State:DrawerIdle";
        }
    }

    //トランポリンが小ジャンプの状態
    public class DrawerStatePull : DrawerState
    {
        public override string GetStateName()
        {
            return "State:DrawerPull";
        }
    }

}
