using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoxState;
using CriWare;
using System;
using System.Runtime.CompilerServices;
//using System.Diagnostics;



public class SoundPlayer : MonoBehaviour
{
    [SerializeField]
    private AtomLoader atomLoader;

    //[SerializeField]
    //private CharacterController characterController;

    /* (2) プレーヤー */
    private CriAtomExPlayer Player;

    /* (12) ACB 情報 */
    private CriAtomExAcb acb;

    /* (16) キュー名 */
    private string cueName;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Box box;

    /*State変数*/
    public BoxStateProcessor boxStateProcessor = new BoxStateProcessor();

    private String BeforeStateName;

    /* (3) コルーチン化する */
    IEnumerator Start()
    {
        //Debug.Log("Start");
       


        /* (4) ライブラリの初期化済みチェック */
        while (!CriWareInitializer.IsInitialized())
        {
            yield return null;
        }

        //Debug.Log("iketa");

        /* (5) プレーヤーの作成 */
        Player = new CriAtomExPlayer();

     
        
    }

    // Update is called once per frame
    void Update()
    {
        if (boxStateProcessor.State == null) 
        {
            //Debug.Log("SoundplayerReturn:State=null");

            box = player.transform.Find("Box(Clone)").gameObject.GetComponent<Box>();

            boxStateProcessor = box.StateProcessor;

            return;

        }

       

        if (boxStateProcessor.State.GetStateName() != BeforeStateName) 
        {
            //Debug.Log("test");
            BeforeStateName = boxStateProcessor.State.GetStateName();
            //箱が再出現するとき:cueName = Repop
            if (BeforeStateName == "State:BoxRepop")
            {
                //Debug.Log("Repop");
                SetAcb(atomLoader.acbAssets[0].Handle);
                SetCueName("Repop");
                Play();

            }
            //箱が壊れたとき:cueName = Crash
            if (BeforeStateName == "State:Crash")
            {
                //Debug.Log("Crash");
                SetAcb(atomLoader.acbAssets[0].Handle);
                SetCueName("Crash");
                Play();

            }
        }

      
        

        //トランポリン小ジャンプ:cueName = Bound_Small

        //トランポリン大ジャンプ:cueName = Bound_Big

        //BGM
    }

    public void Play()
    {
        /* (18) キュー情報をプレーヤー設定*/
        Player.SetCue(acb, cueName);

        if (Player.IsPaused())
        {
            Player.Pause(false);
        }
        else
        {
            /* (7) プレーヤーの再生 */
            Player.Start();
        }

    }

    /* (8) プレーヤーの停止 */
    public void Stop()
    {
        /* (8) プレーヤーの停止 */
        Player.Stop();
    }

    /* (9) プレーヤーの一時停止 */
    public void Pause()
    {
        /* (9) プレーヤーの一時停止 */
        Player.Pause(true);
    }

    /* (12) ACB の指定 */
    public void SetAcb(CriAtomExAcb acb)
    {
        /* (14) ACB の保存 */
        this.acb = acb;
    }

    /* (15) キュー名の指定 */
    public void SetCueName(string name)
    {
        /* (17) キュー名の保存 */
        cueName = name;
    }

    /* (19) ボリュームの設定 */
    public void SetVolume(float vol)
    {
        /* (19) ボリュームの設定 */
        Player.SetVolume(vol);

        /* (20) パラメーターの更新 */
        Player.UpdateAll();
    }

    /* (21) AISAC コントロール値の設定 */
    public void SetAisacControl(float value)
    {
        /* (21) AISAC コントロール値の設定 */
        Player.SetAisacControl("Any", value);

        /* (22) パラメーターの更新 */
        Player.UpdateAll();
    }
}
