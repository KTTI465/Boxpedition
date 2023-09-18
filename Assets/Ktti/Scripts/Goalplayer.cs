using System.Collections;
using UnityEngine;
using CriWare;
using CriWare.Assets;

public class Goalplayer : MonoBehaviour
{
    public CriAtomAcfAsset acf;
    public CriAtomAcbAsset acb1;

    private CriAtomExPlayer GoalBGM;

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

        GoalBGM = new CriAtomExPlayer();
        GoalBGM.SetCue(acb1.Handle, "Clear");
        GoalBGM.Start();

        SettingBGMVolume(PlayerPrefs.GetFloat("BGM"));
    }
    public void SettingBGMVolume(float vol)
    {
        GoalBGM.SetVolume(vol); GoalBGM.UpdateAll();
    }
}