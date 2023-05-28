using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoxState;
using CriWare;
using System;
using System.Runtime.CompilerServices;
//using System.Media;

public class SoundController : MonoBehaviour
{
    [SerializeField]
    private AtomLoader atomLoader;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Box box;

    private SoundControllerBase SEPlayer;
    private SoundControllerBase BGMPlayer;

    /*State�ϐ�*/
    public BoxStateProcessor boxStateProcessor = new BoxStateProcessor();

    private String BeforeStateName;

    void Start()
    {
        SEPlayer = new SoundControllerBase();
        BGMPlayer = new SoundControllerBase();

        SEPlayer.SetAcb(atomLoader.acbAssets[0].Handle);
        SEPlayer.SetCueName("Repop");

        BGMPlayer.SetAcb(atomLoader.acbAssets[1].Handle);
        BGMPlayer.SetCueName("BGM_Roop");


    }

    void Update()
    {
        if (box == null)
        {
            //Debug.Log("SoundplayerReturn:State=null");

            box = player.transform.Find("Box(Clone)").gameObject.GetComponent<Box>();

            boxStateProcessor = box.StateProcessor;
        }



        if (boxStateProcessor.State.GetStateName() != BeforeStateName)
        {
            //Debug.Log("test");
            BeforeStateName = boxStateProcessor.State.GetStateName();
            //�����ďo������Ƃ�:cueName = Repop
            if (BeforeStateName == "State:BoxRepop")
            {
                Debug.Log("SoundController:Repop");
                SEPlayer.SetCueName("Repop");
                SEPlayer.Play();

            }

            //������ꂽ�Ƃ�:cueName = Crash
            if (BeforeStateName == "State:Crash")
            {
                Debug.Log("SoundController:Crash");
                SEPlayer.SetCueName("Crash");
                SEPlayer.Play();
            }
        }

        //�g�����|�������W�����v:cueName = Bound_Small

        //�g�����|������W�����v:cueName = Bound_Big

        //BGM
        if (Input.GetKeyDown(KeyCode.F6))
        {
            //Debug.Log("SoundController:BGM_Roop");
            BGMPlayer.Stop();
            BGMPlayer.SetCueName("BGM_Roop");
            BGMPlayer.Play();
        }

        
    }
}
