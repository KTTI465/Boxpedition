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
        //BGMƒXƒ‰ƒCƒ_[‚ğ“®‚©‚µ‚½‚Ìˆ—‚ğ“o˜^
        bgmSlider.onValueChanged.AddListener(SetBGM);

        //SEƒXƒ‰ƒCƒ_[‚ğ“®‚©‚µ‚½‚Ìˆ—‚ğ“o˜^
        seSlider.onValueChanged.AddListener(SetSE);

        //Š´“xƒXƒ‰ƒCƒ_[‚ğ“®‚©‚µ‚½‚Ìˆ—‚ğ“o˜^
        sensiSlider.onValueChanged.AddListener(SetSensi);


        bgmSlider.value = (PlayerPrefs.GetFloat("BGM") + 20f) / 20f;
        seSlider.value = (PlayerPrefs.GetFloat("SE") + 20f) / 20f;
        sensiSlider.value = PlayerPrefs.GetFloat("Sensi");
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
        //-20`0‚É•ÏŠ·
        bgmValue = -20f + (value * 20f);

        // •Û‘¶
        PlayerPrefs.SetFloat("BGM", bgmValue);
        PlayerPrefs.Save();

        musicManager.SetBGM();
    }

    public void SetSE(float value)
    {
        //-20`0‚É•ÏŠ·
        seValue = -20f + (value * 20f);

        // •Û‘¶
        PlayerPrefs.SetFloat("SE", seValue);
        PlayerPrefs.Save();

        musicManager.SetSE();
    }

    public void SetSensi(float value)
    {
        sensiValue = value;

        // •Û‘¶
        PlayerPrefs.SetFloat("Sensi", sensiValue);
        PlayerPrefs.Save();
    }
}
