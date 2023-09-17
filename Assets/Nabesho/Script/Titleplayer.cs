using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoxState;
using CharacterState;
using TrampolineState;
using CriWare;
using CriWare.Assets;
using System;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using UnityEngine.UI;
//using System.Media;

public class Titleplayer : MonoBehaviour
{
    //[SerializeField]
    //private AtomLoader atomLoader;

    //[SerializeField]
    //private GameObject player;

    public CriAtomAcfAsset acf;
    public CriAtomAcbAsset acb1, acb2;

    private CriAtomExPlayer TitleBGM, Click;

    IEnumerator Start()
    {
        // ���C�u�����̏������ς݃`�F�b�N /
        while (!CriWareInitializer.IsInitialized()) { yield return null; }
        //ACF�̃��[�h
        acf.Register();
        //DSP�o�X�̐ݒ�̓K�p
        CriAtomEx.AttachDspBusSetting(CriAtomExAcf.GetDspSettingNameByIndex(0));
        //ACB�t�@�C���̃��[�h
        if (!acb1.LoadRequested)
        {
            acb1.LoadImmediate();
        }
        if (!acb2.LoadRequested)
        {
            acb2.LoadImmediate();
        }

        TitleBGM = new CriAtomExPlayer();
        TitleBGM.SetCue(acb1.Handle, "Title");
        TitleBGM.Start();

        Click = new CriAtomExPlayer();
        Click.SetCue(acb2.Handle, "Click");

        SettingBGMVolume(PlayerPrefs.GetFloat("BGM"));
        SettingSFXVolume(PlayerPrefs.GetFloat("SE"));
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Click.Start();
        }

    }

    public void ClickPlay()
    {
        Click.Start();
    }


    public void SettingBGMVolume(float vol)
    {
        TitleBGM.SetVolume(vol); TitleBGM.UpdateAll();
    }

    public void SettingSFXVolume(float vol)
    {
        Click.SetVolume(vol); Click.UpdateAll();
    }
}