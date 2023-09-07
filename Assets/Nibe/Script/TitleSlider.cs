using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class TitleSlider : MonoBehaviour
{
    [SerializeField] stageManager stageManager;
    [SerializeField] MusicManager musicManager;
    [SerializeField] SoundController soundcontroller;

    [SerializeField] UnityEngine.UI.Slider bgmSlider;
    [SerializeField] UnityEngine.UI.Slider seSlider;
    [SerializeField] UnityEngine.UI.Slider sensiSlider;

    public float bgmValue;
    public float seValue;
    public float sensiValue;

    [SerializeField] float bgm;
    [SerializeField] float se;


    void Start()
    {
        //BGM�X���C�_�[�𓮂��������̏�����o�^
        bgmSlider.onValueChanged.AddListener(SetBGM);

        //SE�X���C�_�[�𓮂��������̏�����o�^
        seSlider.onValueChanged.AddListener(SetSE);

        //���x�X���C�_�[�𓮂��������̏�����o�^
        //sensiSlider.onValueChanged.AddListener(SetSensi);


        bgmSlider.value = PlayerPrefs.GetFloat("BGM");
        seSlider.value = PlayerPrefs.GetFloat("SE");

        //bgmSlider.value = 1.0f;
        //seSlider.value = 1.0f;
        //sensiSlider.value = 1.0f;
    }


    public void upBGM()
    {
        bgmSlider.value += 0.2f; ;
    }

    public void downBGM()
    {
        bgmSlider.value -= 0.2f; ;
    }

    public void upSE()
    {
        seSlider.value += 0.2f; ;
    }

    public void downSE()
    {
        seSlider.value -= 0.2f; ;
    }

    public void upSensi()
    {
        //sensiSlider.value += 0.2f; ;
    }

    public void downSensi()
    {
        //sensiSlider.value -= 0.2f; ;
    }

    public void SetBGM(float value)
    {
        /*
        //-80�`0�ɕϊ�
        bgmValue = -80f + (value * 80f);

        // �ۑ�
        PlayerPrefs.SetFloat("BGM", bgmValue);
        PlayerPrefs.Save();

        musicManager.SetBGM();
        */

        bgmValue = value;

        // �ۑ�
        PlayerPrefs.SetFloat("BGM", bgmValue);
        PlayerPrefs.Save();


        try
        {
            soundcontroller.SettingBGMVolume(PlayerPrefs.GetFloat("BGM"));
        }
        catch
        {

        }

        bgm = PlayerPrefs.GetFloat("BGM");
    }

    public void SetSE(float value)
    {
        /*
        //-80�`0�ɕϊ�
        seValue = -80f + (value * 80f);

        // �ۑ�
        PlayerPrefs.SetFloat("SE", seValue);
        PlayerPrefs.Save();

        musicManager.SetSE();
        */

        seValue = value;

        // �ۑ�
        PlayerPrefs.SetFloat("SE", seValue);
        PlayerPrefs.Save();

        try
        {
            soundcontroller.SettingSFXVolume(PlayerPrefs.GetFloat("SE"));
        }
        catch
        {

        }

        se = PlayerPrefs.GetFloat("SE");
    }

    public void SetSensi(float value)
    {
        if (stageManager.openOption == false)
        {
            sensiValue = value;

            // �ۑ�
            PlayerPrefs.SetFloat("Sensi", sensiValue);
            PlayerPrefs.Save();
        }
    }
}
