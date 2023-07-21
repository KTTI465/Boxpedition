﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoxState;
using CharacterState;
using TrampolineState;
using CriWare;
using System;
using System.Runtime.CompilerServices;
using System.Diagnostics;
//using System.Media;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private AtomLoader atomLoader;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Box box;

    [SerializeField]
    private CharacterController chara;

    [SerializeField]
    private trampolineC tp;

    private SoundControllerBase SEPlayer;
    private SoundControllerBase BGMPlayer;
    private SoundControllerBase WalkPlayer;
    private SoundControllerBase JumpPlayer;


    /*State�ϐ�*/
    public BoxStateProcessor boxStateProcessor = new BoxStateProcessor();
    public CharacterStateProcessor charactorStateProcessor = new CharacterStateProcessor();
    public TrampolineStateProcessor trampolineStateProcessor = new TrampolineStateProcessor();

    private String BeforeStateName, BeforeStateName2, BeforeStateName3;
    private bool StartFlag = false;
    private bool MoveFlag = false;

    void Start()
    {
        SEPlayer = new SoundControllerBase();
        BGMPlayer = new SoundControllerBase();
        WalkPlayer = new SoundControllerBase();
        JumpPlayer = new SoundControllerBase();

        SEPlayer.SetAcb(atomLoader.acbAssets[0].Handle);


        BGMPlayer.SetAcb(atomLoader.acbAssets[1].Handle);
        WalkPlayer.SetCueName("Walk");

        BGMPlayer.SetCueName("BGM_Roop");
        BGMPlayer.Play();

        chara = player.GetComponent<CharacterController>();


        tp = GameObject.Find("trampArea").GetComponent<trampolineC>();
        trampolineStateProcessor = tp.StateProcessor;
    }

    void Update()
    {
        if (box == null)
        {
            //Debug.Log("SoundplayerReturn:State=null");
            box = player.transform.Find("Box(Clone)").gameObject.GetComponent<Box>();


            boxStateProcessor = box.StateProcessor;

        }

        charactorStateProcessor = chara.StateProcessor;

        if (boxStateProcessor.State.GetStateName() != BeforeStateName)
        {
            //Debug.Log("test");
            BeforeStateName = boxStateProcessor.State.GetStateName();
            //�����ďo������Ƃ�:cueName = Repop
            if (BeforeStateName == "State:BoxRepop" && StartFlag)
            {
                UnityEngine.Debug.Log("SoundController:Repop");
                SEPlayer.SetCueName("Repop");
                SEPlayer.Play();
            }

            //������ꂽ�Ƃ�:cueName = Crash
            if (BeforeStateName == "State:Crash")
            {
                UnityEngine.Debug.Log("SoundController:Crash");
                SEPlayer.SetCueName("Crash");
                SEPlayer.Play();
            }
        }
        else
        {
            StartFlag = true;
        }

        if (charactorStateProcessor.State.GetStateName() != BeforeStateName2)
        {
            BeforeStateName2 = charactorStateProcessor.State.GetStateName();

            if (BeforeStateName2 == "State:Jump1")
            {
                UnityEngine.Debug.Log("SoundController:Jump1");
                JumpPlayer.SetCueName("Jump");
                JumpPlayer.Play();
            }

            if (BeforeStateName2 == "State:Jump2")
            {
                UnityEngine.Debug.Log("SoundController:Jump2");
                JumpPlayer.SetCueName("Cut");
                JumpPlayer.Play();
            }
        }

        if (trampolineStateProcessor.State.GetStateName() != BeforeStateName3)
        {
            BeforeStateName3 = trampolineStateProcessor.State.GetStateName();

            if (BeforeStateName3 == "State:TrampolineSmallJump")
            {
                UnityEngine.Debug.Log("SoundController:Bound_S");
                SEPlayer.SetCueName("Bound_Small");
                SEPlayer.Play();
            }

            if (BeforeStateName3 == "State:TrampolineBigJump")
            {
                UnityEngine.Debug.Log("SoundController:Bound_Big");
                SEPlayer.SetCueName("Bound_Big");
                SEPlayer.Play();
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            SEPlayer.SetCueName("Click");
            SEPlayer.Play();
        }



        if (!MoveFlag)
        {
            if (charactorStateProcessor.State.GetStateName() == "State:Move")
            {
                UnityEngine.Debug.Log("SoundController:Move");
                WalkPlayer.SetCueName("Walk");
                WalkPlayer.Play();
                MoveFlag = true;
            }
            else if (charactorStateProcessor.State.GetStateName() == "State:MoveGlass")
            {
                UnityEngine.Debug.Log("SoundController:MoveGlass");
                WalkPlayer.SetCueName("Walk_Glass");
                WalkPlayer.Play();
                MoveFlag = true;
            }
            else if (charactorStateProcessor.State.GetStateName() == "State:MoveBook")
            {
                UnityEngine.Debug.Log("SoundController:MoveBook");
                WalkPlayer.SetCueName("Walk_Book");
                WalkPlayer.Play();
                MoveFlag = true;
            }
        }
        if (MoveStateCheck() == false || !chara.isGround && MoveFlag)
        {
            WalkPlayer.Stop();
            MoveFlag = false;
        }



        //�g�����|�������W�����v:cueName = Bound_Small

        //�g�����|������W�����v:cueName = Bound_Big

        //BGM        
    }

    bool MoveStateCheck()
    {
        if (charactorStateProcessor.State.GetStateName() == "State:Move")
        {
            return true;
        }
        else if (charactorStateProcessor.State.GetStateName() == "State:MoveGlass")
        {
            return true;
        }
        else if (charactorStateProcessor.State.GetStateName() == "State:MoveBook")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}