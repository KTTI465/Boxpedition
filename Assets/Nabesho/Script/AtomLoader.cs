using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* (1) ���O��Ԃ̐ݒ� */
using CriWare;
using CriWare.Assets;


public class AtomLoader : MonoBehaviour
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

    }
}
