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

    //ステートのクラス
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
    public class CharacterStateMoveGlass : CharacterState
    {
        public override string GetStateName()
        {
            return "State:MoveGlass";
        }
    }

    public class CharacterStateMoveMat : CharacterState
    {
        public override string GetStateName()
        {
            return "State:MoveMat";
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

    //ミス状態
    public class CharacterStateMiss : CharacterState
    {
        public override string GetStateName()
        {
            return "State:Miss";
        }
    }

    //ミス状態
    public class CharacterStateCatch : CharacterState
    {
        public override string GetStateName()
        {
            return "State:Catch";
        }
    }

    //トランポリン小ジャンプ
    public class CharacterStateTrampolineSmallJump : CharacterState
    {
        public override string GetStateName()
        {
            return "State:TrampolineSmallJump";
        }
    }

    //トランポリン大ジャンプ
    public class CharacterStateTrampolineBigJump : CharacterState
    {
        public override string GetStateName()
        {
            return "State:TrampolineBigJump";
        }
    }
}
