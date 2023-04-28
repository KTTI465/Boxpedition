using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// キャラクターの状態（ステート）

namespace CharacterState
{
    /// <summary>
    /// ステートの実行を管理するクラス
    /// </summary>
    public class CharacterStateProcessor
    {
        //ステート本体
        private CharacterState _State;
        public CharacterState State
        {
            set { _State = value; }
            get { return _State; }
        }

        //実行ブリッジ
        public void Execute() => State.Execute();
    }

    /// <summary>
    /// ステートのクラス
    /// </summary>
    public abstract class CharacterState
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

//待機状態
    public class CharacterStateIdle : CharacterState
    {
        public override string GetStateName()
        {
            return "State:Idle";
        }
    }

　//移動状態
    public class CharacterStateMove : CharacterState
    {
        public override string GetStateName()
        {
            return "State:Move";
        }
    }

    //ジャンプ状態
    public class CharacterStateJump1 : CharacterState
    {
        public override string GetStateName()
        {
            return "State:Jump1";
        }
    }

    //ジャンプ状態
    public class CharacterStateJump2 : CharacterState
    {
        public override string GetStateName()
        {
            return "State:Jump2";
        }
    }



}
