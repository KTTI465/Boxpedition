using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SliderSystem : MonoBehaviour
{
    [SerializeField] Slider bgmSlider;
    [SerializeField] Slider seSlider;

    public float bgmValue;
    public float seValue;


    // Start is called before the first frame update
    void Start()
    {
        //BGMスライダーを動かした時の処理を登録
        bgmSlider.onValueChanged.AddListener(SetBGM);

        //SEスライダーを動かした時の処理を登録
        seSlider.onValueChanged.AddListener(SetSE);


        if (PlayerPrefs.HasKey("BGM"))
        {
            var bgmCal = (PlayerPrefs.GetFloat("BGM") + 20f) / 20f;
            bgmSlider.value = bgmCal;  //スライダーに値を適応
        }

        if (PlayerPrefs.HasKey("SE"))
        {
            var seCal = (PlayerPrefs.GetFloat("SE") + 20f) / 20f;
            seSlider.value = seCal;  //スライダーに値を適応
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBGM(float value)
    {
        //-20～0に変換
        bgmValue = -20f + (value * 20f);

        // 保存
        PlayerPrefs.SetFloat("BGM", bgmValue);
        PlayerPrefs.Save();
    }

    public void SetSE(float value)
    {
        //-20～0に変換
        seValue = -20f + (value * 20f);

        // 保存
        PlayerPrefs.SetFloat("SE", seValue);
        PlayerPrefs.Save();
    }
}
