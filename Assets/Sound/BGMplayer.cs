using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CriWare;
using CriWare.Assets;

public class BGMplayer : MonoBehaviour
{
    public static BGMplayer instance;

    //ACF �A�Z�b�g
    public CriAtomAcfAsset acf;

    //ACB �A�Z�b�g
    public CriAtomAcbAsset acb;

    //�v���C���[
    private CriAtomExPlayer player;

    public List<string> SoundTitles;
    public int NowPlaySoundNumber = 0;

    //private float volume = 1.0f;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    IEnumerator Start()
    {
        while (!CriWareInitializer.IsInitialized())
        {
            yield return null;
        }

        //ACF�̃��[�h
        acf.Register();

        //DSP �o�X�ݒ�̓K��
        CriAtomEx.AttachDspBusSetting("Bus1");

        //ACB �t�@�C���̃��[�h
        acb.LoadImmediate();

        //�v���[���[�̃C���X�^���X�𐶐�
        player = new CriAtomExPlayer();

        //�f�[�^�E�L���[�̎w��
        player.SetCue(acb.Handle, SoundTitles[0]);
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

    public void BGMplay(string SETitle)
    {
        player.SetCue(acb.Handle, SETitle);
        player.Start();

    }
}
