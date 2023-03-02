using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    Slider slider;

    public Setting setting;

    float maxValue;

    void Start()
    {
        slider = GetComponent<Slider>();

        if (Setting.mouseSpeed == setting)
        {
            maxValue = Settings.Get().maxSpeed;
        }
        else if (Setting.bgmVolume == setting)
        {
            maxValue = Settings.Get().maxBgm;
        }
        else if (Setting.seVolume == setting)
        {
            maxValue = Settings.Get().maxSe;
        }
    }

    public void ChengeValue()
    {
        if (Setting.mouseSpeed == setting)
        {
            Settings.Get().SetSpeed(slider.value * maxValue);
        }
        else if (Setting.bgmVolume == setting)
        {
            Settings.Get().SetBgm(slider.value * maxValue);
        }
        else if (Setting.seVolume == setting)
        {
            Settings.Get().SetSe(slider.value * maxValue);
        }
    }
}
public enum Setting
{
    mouseSpeed,
    bgmVolume,
    seVolume
}
