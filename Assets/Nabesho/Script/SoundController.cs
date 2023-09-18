using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoxState;
using CharacterState;
using TrampolineState;
using DrawerState;
using CriWare;
using System;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using ClimbState;
using RopeState;
using RopeSwingState;
using MonsterState;

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

    [SerializeField]
    private List<drawer> drawers;

    [SerializeField]
    private plug plug;

    [SerializeField]
    private rope rope;

    [SerializeField]
    private blockWayMonster monster;


    private SoundControllerBase SEPlayer;
    private SoundControllerBase BGMPlayer;
    private SoundControllerBase WalkPlayer;
    private SoundControllerBase JumpPlayer;
    private SoundControllerBase ClimbPlayer;


    /*State*/
    public BoxStateProcessor boxStateProcessor = new BoxStateProcessor();
    public CharacterStateProcessor charactorStateProcessor = new CharacterStateProcessor();
    public TrampolineStateProcessor trampolineStateProcessor = new TrampolineStateProcessor();
    public DrawerStateProcessor drawerStateProcessor = new DrawerStateProcessor();
    public ClimbStateProcessor climbStateProcessor = new ClimbStateProcessor();
    public RopeStateProcessor ropeStateProcessor = new RopeStateProcessor();
    public RopeSwingStateProcessor RopeSwingStateProcessor = new RopeSwingStateProcessor();
    public MonsterStateProcessor MonsterStateProcessor = new MonsterStateProcessor();

    private String BeforeStateName, BeforeStateName2, BeforeStateName3, BeforeSateName4, BeforeStateName5, 
        BeforeStateName6, BeforeStateName7, BeforeStateName8;
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

        /*SEPlayer.SetAcb(atomLoader.acbAssets[0].Handle);
        BGMPlayer.SetAcb(atomLoader.acbAssets[1].Handle);
        WalkPlayer.SetCueName("Walk");

        BGMPlayer.SetCueName("BGM_Roop");
        BGMPlayer.Play();*/

        StartCoroutine(Loader());

        if (player != null)
        {
            chara = player.GetComponent<CharacterController>();
        }

        try
        {
            tp = GameObject.Find("boundArea").GetComponent<trampolineRemake>();
            trampolineStateProcessor = tp.StateProcessor;
        }
        catch
        {
            UnityEngine.Debug.LogWarning("'boundArea' is not found");
        }

        try
        {
            ropeAct = GameObject.Find("warpRopeManager").GetComponent<RopeActAnim>();
            climbStateProcessor = ropeAct.climbStateProcessor;
        }
        catch
        {
            UnityEngine.Debug.LogWarning("'warpRopeManager' is not found");
        }

        try
        {
            ropeStateProcessor = plug.StateProcessor;
        }
        catch 
        {
            UnityEngine.Debug.LogWarning("'plug' is not found");
        }

        try
        {
            RopeSwingStateProcessor = rope.RopeSwingStateProcessor;
        }
        catch
        {
            UnityEngine.Debug.LogWarning("'rope' is not found");
        }

        try
        {
            MonsterStateProcessor = monster.MonsterStateProcessor;
        }
        catch
        {
            UnityEngine.Debug.LogWarning("'blockWayMonster' is not found");
        }
    }

    IEnumerator Loader()
    {
        while (!atomLoader.isLoaded) yield return null;

        while (atomLoader.acbAssets[0].Handle == null) yield return null;

        while (atomLoader.acbAssets[1].Handle == null) yield return null;

        SEPlayer.SetAcb(atomLoader.acbAssets[0].Handle);
        BGMPlayer.SetAcb(atomLoader.acbAssets[1].Handle);
        WalkPlayer.SetCueName("Walk");

        BGMPlayer.SetCueName("BGM_Roop");
        BGMPlayer.Play();

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
        try
        {
            if (player != null)
            {
                if (box == null)
                {
                    //Debug.Log("SoundplayerReturn:State=null");
                    box = player.transform.Find("Box(Clone)").gameObject.GetComponent<Box>();


                    boxStateProcessor = box.StateProcessor;

                }

                charactorStateProcessor = chara.StateProcessor;

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
                    else if (charactorStateProcessor.State.GetStateName() == "State:MoveMat")
                    {
                        UnityEngine.Debug.Log("SoundController:MoveMat");
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
            }
        }
        catch
        {
            //例外虫
        }

        try
        {
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
        }
        catch
        {
            //例外無視
        }

        try
        {
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
        }
        catch
        {
            //例外無視
        }

        try
        {
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
        }
        catch
        {
            //例外無視
        }

        try
        {
            if (ropeStateProcessor.State.GetStateName() != BeforeStateName6)
            {
                BeforeStateName6 = ropeStateProcessor.State.GetStateName();

                if (BeforeStateName6 == "State:Slide")
                {
                    UnityEngine.Debug.Log("SoundController:Slide");
                    SEPlayer.SetCueName("Slide");
                    SEPlayer.Play();
                }

            }
        }
        catch
        {
            //例外無視
        }

        if (drawers.Count > 0)
        {
            foreach (var drawer in drawers)
            {
                drawerStateProcessor = drawer.StateProcessor;

                var statename = drawerStateProcessor.State.GetStateName();

                if (statename == "State:DrawerPull")
                    break;
            }

            if (drawerStateProcessor.State.GetStateName() != BeforeStateName5)
            {
                BeforeStateName5 = drawerStateProcessor.State.GetStateName();

                if (BeforeStateName5 == "State:DrawerPull")
                {
                    UnityEngine.Debug.Log("SoundController:Pull");
                    SEPlayer.SetCueName("Pull");
                    SEPlayer.Play();
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            SEPlayer.SetCueName("Click");
            SEPlayer.Play();
        }

        try
        {
            BeforeSateName4 = ropeAct.climbStateProcessor.State.GetStateName();

            if (ClimbMoveStateCheck(BeforeSateName4))
            {
                if (ropeAct.climbStateProcessor.State.GetStateName() == "State:Move" && !MoveFlag2)
                {
                    UnityEngine.Debug.Log("SoundController:Move(Climb)");
                    ClimbPlayer.SetCueName("Walk");
                    ClimbPlayer.Play();
                    WalkPlayer.Stop();
                    MoveFlag2 = true;
                }


                if (ropeAct.climbStateProcessor.State.GetStateName() == "State:Climb" && !MoveFlag2)
                {
                    UnityEngine.Debug.Log("SoundController:Climb");
                    //保留
                    ClimbPlayer.SetCueName("Walk");
                    ClimbPlayer.Play();
                    WalkPlayer.Stop();
                    MoveFlag2 = true;
                }
            }
            else
            {
                ClimbPlayer.Stop();
                MoveFlag2 = false;
            }
        }
        catch
        {
            //例外無視
        }

        try
        {
            if (RopeSwingStateProcessor.State.GetStateName() != BeforeStateName7)
            {
                BeforeStateName7 = RopeSwingStateProcessor.State.GetStateName();

                if (BeforeStateName7 == "State:RopeSwingSwing")
                {
                    UnityEngine.Debug.Log("SoundController:RopeSwingSwing");
                    SEPlayer.SetCueName("Ropeswing");
                    SEPlayer.Play();
                }

                if (BeforeStateName7 == "State:RopeSwingJump")
                {
                    UnityEngine.Debug.Log("SoundController:RopeSwingJump");
                    SEPlayer.SetCueName("Ropejump");
                    SEPlayer.Play();
                }
            }
        }
        catch
        {
            //例外無視
        }

        try
        {
            if (MonsterStateProcessor.State.GetStateName() != BeforeStateName8)
            {
                BeforeStateName8 = MonsterStateProcessor.State.GetStateName();

                if (BeforeStateName8 == "State:MonsterRun")
                {
                    UnityEngine.Debug.Log("SoundController:MonsterRun");
                    SEPlayer.SetCueName("Monsrun");
                    SEPlayer.Play();
                }

                if (BeforeStateName8 == "State:MonsterJump")
                {
                    UnityEngine.Debug.Log("SoundController:MonsterJump");
                    SEPlayer.SetCueName("Monsjump");
                    SEPlayer.Play();
                }
            }
        }
        catch
        {
            //例外無視
        }

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
        else if (charactorStateProcessor.State.GetStateName() == "State:MoveMat")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool ClimbMoveStateCheck(string state)
    {
        if (state == "State:Idle")
        {
            return false;
        }

        return true;
    }

    bool SwingStateChecker(string state)
    {
        if (state == "State:RopeSwingIdle")
        {
            return false;
        }

        return true;
    }

    public void KeitoPlay()
    {
        SEPlayer.SetCueName("Keito");
        SEPlayer.Play();
    }

    public void Click()
    {
        SEPlayer.SetCueName("Click");
        SEPlayer.Play();
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