using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CriWare;
using CriWare.Assets;

public class SEplayer : MonoBehaviour
{

    /* (9) �S�f�[�^�����[�h�ς݂��ǂ��� */

    public bool isLoaded
    {
        /* (11) �e�f�[�^�����[�h�ς݂��ǂ������`�F�b�N */
        get
        {
            bool value = false;
            foreach (var acbAsset in acbAssets)
            {
                value |= acbAsset.Loaded;
            }
            return value;
        }
    }

    /* (2) ACF �A�Z�b�g */
    public CriAtomAcfAsset acfAsset;

    /* (3) ACB �A�Z�b�g�̔z�� */
    /* Note: ACB �͕����t�@�C���ɂȂ��Ă���\�������邽�� */
    public CriAtomAcbAsset[] acbAssets;

    /* (10) ���W�X�g�ς݂��ǂ����̃t���O */
    private bool acfIsRegisterd = false;

    private CriAtomExPlayer player;


    /* (4) �R���[�`���� */
    IEnumerator Start()
    {
        /* (5) ���C�u�����̏������ς݃`�F�b�N */
        while (!CriWareInitializer.IsInitialized())
        {
            yield return null;

        }

        /* (6) ACF �f�[�^�̓o�^ */
        acfIsRegisterd = acfAsset.Register();

        /* (7) �f�t�H���g�� DSP �o�X�ݒ��K�p */
        CriAtomEx.AttachDspBusSetting(CriAtomExAcf.GetDspSettingNameByIndex(0));

        /* (8) ACB �f�[�^�̃��[�h */
        foreach (var acbAsset in acbAssets)
        {
            acbAsset.LoadImmediate();
        }

        player = new CriAtomExPlayer();

        player.SetCue(acbAssets[0].Handle, "crash");

    }


    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.F5))
        {
            //�v���[���[���Đ�
            player.Start();
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            //�v���[���[���~
            player.Stop();
        }

        if (Input.GetKeyDown(KeyCode.F7))
        {
            //�v���[���[���ꎞ��~����
            player.Pause(true);
        }
        if (Input.GetKeyDown(KeyCode.F8))
        {
            //�v���[���[���ĊJ����
            player.Pause(false);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            //AISAC �R���g���[���l��0�ɐݒ肷��
            player.SetAisacControl("Any", 0.0f);

            //�Đ����Ă���L���[�ɑ΂��ăp�����[�^�[��K�p����
            player.UpdateAll();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            //AISAC �R���g���[���l��1�ɐݒ肷��
            player.SetAisacControl("Any", 1.0f);

            //�Đ����Ă���L���[�ɑ΂��ăp�����[�^�[��K�p����
            player.UpdateAll();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //�{�����[����������
            volume = Mathf.Max(volume - 0.1f, 0.0f);
            player.SetVolume(volume);
            player.SetPitch(-600.0f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //�{�����[�����グ��
            volume = Mathf.Min(volume + 0.1f, 1.0f);
            player.SetVolume(volume);
            player.SetPitch(0.0f);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //�{�����[�������ɖ߂�
            volume = 1.0f;
            player.SetVolume(volume);

            //�s�b�`�����ɖ߂�
            player.SetPitch(0.0f);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //�Đ�����L���[������ɂ���
            NowPlaySoundNumber = Mathf.Min(NowPlaySoundNumber + 1, SoundTitles.Count);
            player.SetCue(acb.Handle, SoundTitles[NowPlaySoundNumber]);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            //�Đ�����L���[�ЂƂO�ɂ���
            NowPlaySoundNumber = Mathf.Max(NowPlaySoundNumber - 1, 0);
            player.SetCue(acb.Handle, SoundTitles[NowPlaySoundNumber]);
        }
        */
    }

    public void SFXplay(string SETitle)
    {
        player.SetCue(acbAssets[0].Handle, SETitle);
        player.Start();

    }
}
