using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BoxState;
using CriWare;
using System;
using System.Runtime.CompilerServices;
//using System.Diagnostics;

public class SoundControllerBase 
{

    //[SerializeField]
    //private CharacterController characterController;

    /* (2) �v���[���[ */
    private CriAtomExPlayer Player;

    /* (12) ACB ��� */
    private CriAtomExAcb acb;

    /* (16) �L���[�� */
    private string cueName;

    public SoundControllerBase()
    {
        while (!CriWareInitializer.IsInitialized())
        {

        }

        /* (5) �v���[���[�̍쐬 */
        Player = new CriAtomExPlayer();
    }


    public void Play()
    {
        /* (18) �L���[�����v���[���[�ݒ�*/
        Player.SetCue(acb, cueName);

        if (Player.IsPaused())
        {
            Player.Pause(false);
        }
        else
        {
            /* (7) �v���[���[�̍Đ� */
            Player.Start();
        }

    }

    /* (8) �v���[���[�̒�~ */
    public void Stop()
    {
        /* (8) �v���[���[�̒�~ */
        Player.Stop();
    }

    /* (9) �v���[���[�̈ꎞ��~ */
    public void Pause()
    {
        /* (9) �v���[���[�̈ꎞ��~ */
        Player.Pause(true);
    }

    /* (12) ACB �̎w�� */
    public void SetAcb(CriAtomExAcb acb)
    {
        /* (14) ACB �̕ۑ� */
        this.acb = acb;
    }

    /* (15) �L���[���̎w�� */
    public void SetCueName(string name)
    {
        /* (17) �L���[���̕ۑ� */
        cueName = name;
    }

    /* (19) �{�����[���̐ݒ� */
    public void SetVolume(float vol)
    {
        /* (19) �{�����[���̐ݒ� */
        Player.SetVolume(vol);

        /* (20) �p�����[�^�[�̍X�V */
        Player.UpdateAll();
    }

    /* (21) AISAC �R���g���[���l�̐ݒ� */
    public void SetAisacControl(float value)
    {
        /* (21) AISAC �R���g���[���l�̐ݒ� */
        Player.SetAisacControl("Any", value);

        /* (22) �p�����[�^�[�̍X�V */
        Player.UpdateAll();
    }
}
