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
    [SerializeField] Titleplayer titlePlayer;
    [SerializeField] SoundController soundcontroller;

    [SerializeField] UnityEngine.UI.Slider bgmSlider;
    [SerializeField] UnityEngine.UI.Slider seSlider;
    [SerializeField] UnityEngine.UI.Slider sensiSlider;

    public float bgmValue;  // bgmの音量の値
    public float seValue;  // seの音量の値
    public float sensiValue;  // マウス感度の値

    [SerializeField] float bgm;
    [SerializeField] float se;


    void Start()
    {
        bgmSlider.onValueChanged.AddListener(SetBGM);
        seSlider.onValueChanged.AddListener(SetSE);
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
        bgmValue = -80f + (value * 80f);

        PlayerPrefs.SetFloat("BGM", bgmValue);
        PlayerPrefs.Save();

        musicManager.SetBGM();
        */

        bgmValue = value;

        PlayerPrefs.SetFloat("BGM", bgmValue);
        PlayerPrefs.Save();

        try
        {
            titlePlayer.SettingBGMVolume(PlayerPrefs.GetFloat("BGM"));
        }
        catch
        {
            soundcontroller.SettingBGMVolume(PlayerPrefs.GetFloat("BGM"));
        }

        bgm = PlayerPrefs.GetFloat("BGM");
    }

    public void SetSE(float value)
    {
        /*
        seValue = -80f + (value * 80f);

        PlayerPrefs.SetFloat("SE", seValue);
        PlayerPrefs.Save();

        musicManager.SetSE();
        */

        seValue = value;

        PlayerPrefs.SetFloat("SE", seValue);
        PlayerPrefs.Save();

        try
        {
            titlePlayer.SettingSFXVolume(PlayerPrefs.GetFloat("SE"));
        }
        catch
        {
            soundcontroller.SettingSFXVolume(PlayerPrefs.GetFloat("SE"));
        }

        se = PlayerPrefs.GetFloat("SE");
    }

    public void SetSensi(float value)
    {
        if (stageManager.openOption == false)
        {
            sensiValue = value;

            PlayerPrefs.SetFloat("Sensi", sensiValue);
            PlayerPrefs.Save();
        }
    }
}
