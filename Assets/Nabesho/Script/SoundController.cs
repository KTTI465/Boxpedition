using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoxState;
using CharacterState;
using TrampolineState;
using CriWare;
using System;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using ClimbState;
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
    private trampolineRemake tp;

    [SerializeField]
    private RopeActAnim ropeAct;

    private SoundControllerBase SEPlayer;
    private SoundControllerBase BGMPlayer;
    private SoundControllerBase WalkPlayer;
    private SoundControllerBase JumpPlayer;
    private SoundControllerBase ClimbPlayer;


    /*State*/
    public BoxStateProcessor boxStateProcessor = new BoxStateProcessor();
    public CharacterStateProcessor charactorStateProcessor = new CharacterStateProcessor();
    public TrampolineStateProcessor trampolineStateProcessor = new TrampolineStateProcessor();
    public ClimbStateProcessor climbStateProcessor = new ClimbStateProcessor();

    private String BeforeStateName, BeforeStateName2, BeforeStateName3, BeforeSateName4;
    private bool StartFlag = false;
    private bool MoveFlag = false;
    private bool MoveFlag2 = false;
    public Slider BGMSlider, SFXSlider; //here

    void Start()
    {
        SEPlayer = new SoundControllerBase();
        BGMPlayer = new SoundControllerBase();
        WalkPlayer = new SoundControllerBase();
        JumpPlayer = new SoundControllerBase();
        ClimbPlayer = new SoundControllerBase();

        SEPlayer.SetAcb(atomLoader.acbAssets[0].Handle);



        BGMPlayer.SetAcb(atomLoader.acbAssets[1].Handle);
        WalkPlayer.SetCueName("Walk");

        BGMPlayer.SetCueName("BGM_Roop");
        BGMPlayer.Play();

        chara = player.GetComponent<CharacterController>();


        tp = GameObject.Find("boundArea").GetComponent<trampolineRemake>();
        trampolineStateProcessor = tp.StateProcessor;

        ropeAct = GameObject.Find("warpRopeManager").GetComponent<RopeActAnim>();
        climbStateProcessor = ropeAct.climbStateProcessor;

        //here 
        BGMSlider.maxValue = SFXSlider.maxValue = 1.0f;
        BGMSlider.minValue = SFXSlider.minValue = 0.0f;
        //BGMSlider.value = SliderValueSave.BGMvalue;
        //SFXSlider.value = SliderValueSave.SEvalue;

        if (BGMSlider != null && SFXSlider != null)
        {
            SettingBGMVolume(BGMSlider.value); //here
            SettingSFXVolume(SFXSlider.value); //here  
        }
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
            //cueName = Repop
            if (BeforeStateName == "State:BoxRepop" && StartFlag)
            {
                UnityEngine.Debug.Log("SoundController:Repop");
                SEPlayer.SetCueName("Repop");
                SEPlayer.Play();
            }

            //cueName = Crash
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


        if (climbStateProcessor.State.GetStateName() != BeforeSateName4 &&
            MoveFlag2)
        {
            MoveFlag2 = false;
            ClimbPlayer.Stop();
        }
        if (!MoveFlag2)
        {
            if (climbStateProcessor.State.GetStateName() == "State:Move")
            {
                UnityEngine.Debug.Log("SoundController:Move(Climb)");
                ClimbPlayer.SetCueName("Walk");
                ClimbPlayer.Play();
                WalkPlayer.Stop();
                MoveFlag2 = true;
            }
            else if (climbStateProcessor.State.GetStateName() == "State:Climb")
            {
                UnityEngine.Debug.Log("SoundController:Climb");
                //保留
                ClimbPlayer.SetCueName("Walk");
                ClimbPlayer.Play();
                WalkPlayer.Stop();
                MoveFlag2 = true;
            }
        }
        BeforeSateName4 = climbStateProcessor.State.GetStateName();

        //cueName = Bound_Small

        //cueName = Bound_Big

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

    //aaaa
    //bbbb
    public void SettingBGMVolume(float vol)
    {
        BGMPlayer.SetVolume(vol);
    }

    public void SettingSFXVolume(float vol)
    {
        SEPlayer.SetVolume(vol);
        WalkPlayer.SetVolume(vol);
        JumpPlayer.SetVolume(vol);
    }
}