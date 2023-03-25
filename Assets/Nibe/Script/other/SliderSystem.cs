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
        //BGM�X���C�_�[�𓮂��������̏�����o�^
        bgmSlider.onValueChanged.AddListener(SetBGM);

        //SE�X���C�_�[�𓮂��������̏�����o�^
        seSlider.onValueChanged.AddListener(SetSE);


        if (PlayerPrefs.HasKey("BGM"))
        {
            var bgmCal = (PlayerPrefs.GetFloat("BGM") + 20f) / 20f;
            bgmSlider.value = bgmCal;  //�X���C�_�[�ɒl��K��
        }

        if (PlayerPrefs.HasKey("SE"))
        {
            var seCal = (PlayerPrefs.GetFloat("SE") + 20f) / 20f;
            seSlider.value = seCal;  //�X���C�_�[�ɒl��K��
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBGM(float value)
    {
        //-20�`0�ɕϊ�
        bgmValue = -20f + (value * 20f);

        // �ۑ�
        PlayerPrefs.SetFloat("BGM", bgmValue);
        PlayerPrefs.Save();
    }

    public void SetSE(float value)
    {
        //-20�`0�ɕϊ�
        seValue = -20f + (value * 20f);

        // �ۑ�
        PlayerPrefs.SetFloat("SE", seValue);
        PlayerPrefs.Save();
    }
}
