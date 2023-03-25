using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class TitleSlider : MonoBehaviour
{
    [SerializeField] MusicManager musicManager;

    [SerializeField] UnityEngine.UI.Slider bgmSlider;
    [SerializeField] UnityEngine.UI.Slider seSlider;
    [SerializeField] UnityEngine.UI.Slider sensiSlider;

    public float bgmValue;
    public float seValue;
    public float sensiValue;


    void Start()
    {
        //BGMスライダーを動かした時の処理を登録
        bgmSlider.onValueChanged.AddListener(SetBGM);

        //SEスライダーを動かした時の処理を登録
        seSlider.onValueChanged.AddListener(SetSE);

        //感度スライダーを動かした時の処理を登録
        sensiSlider.onValueChanged.AddListener(SetSensi);
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
        sensiSlider.value += 0.2f; ;
    }

    public void downSensi()
    {
        sensiSlider.value -= 0.2f; ;
    }

    public void SetBGM(float value)
    {
        //-20～0に変換
        bgmValue = -20f + (value * 20f);

        // 保存
        PlayerPrefs.SetFloat("BGM", bgmValue);
        PlayerPrefs.Save();

        musicManager.SetBGM();
    }

    public void SetSE(float value)
    {
        //-20～0に変換
        seValue = -20f + (value * 20f);

        // 保存
        PlayerPrefs.SetFloat("SE", seValue);
        PlayerPrefs.Save();

        musicManager.SetSE();
    }

    public void SetSensi(float value)
    {
        sensiValue = value;

        // 保存
        PlayerPrefs.SetFloat("Sensi", sensiValue);
        PlayerPrefs.Save();
    }
}
