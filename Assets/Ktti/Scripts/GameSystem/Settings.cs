using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public float maxSpeed = 10;
    public float maxBgm = 1;
    public float maxSe = 1;

    [SerializeField, NonEditable]
    float mouseSpeed;
    [SerializeField, NonEditable]
    float bgmVolume;
    [SerializeField, NonEditable]
    float seVolume;

    static Settings _manage;

    public static Settings Get()
    {
        if (_manage == null)
        {
            _manage = FindObjectOfType<Settings>();
            if (_manage == null)
            {
                return null;
            }
        }
        return _manage;
    }

    public float GetSpeed()
    {
        return maxSpeed;
    }

    public float GetBgm()
    {
        return maxBgm;
    }

    public float GetSe()
    {
        return seVolume;
    }

    public void SetSpeed(float value)
    {
        mouseSpeed = value;
    }
    public void SetBgm(float value)
    {
        bgmVolume = value;
    }
    public void SetSe(float value)
    {
        seVolume = value;
    }
}

