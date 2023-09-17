using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.UI;

public class stageManager : MonoBehaviour
{
    [SerializeField] MusicManager musicManager;
    [SerializeField] TitleSlider titleSlider;
    [SerializeField] SoundController soundcontroller;

    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject TitleButton;

    [SerializeField] Text bgmText;
    [SerializeField] Text seText;
    [SerializeField] Text sensiText;
    [SerializeField] Text guideText;
    [SerializeField] Text guideOnText;
    [SerializeField] Text guideOffText;
    [SerializeField] Text titleText;

    [SerializeField] UnityEngine.UI.Slider sensiSlider;

    [SerializeField] Button guideOnButton;
    [SerializeField] Button guideOffButton;

    [SerializeField] GameObject guideArrow;
    [SerializeField] GameObject offScreenGuideArrow;

    public bool openOption = false;
    private int optionNum = 0;
    private int ps4Count = 0;
    public int buttonIntervalTime = 50;

    private float r1 = 1.0f;
    private float g1 = 1.0f;
    private float b1 = 1.0f;
    private float a1 = 1.0f;
    private float r2 = 1.0f;
    private float g2 = 1.0f;
    private float b2 = 0.0f;
    private float a2 = 1.0f;

    private bool right = false;
    private bool left = false;
    private bool up = false;
    private bool down = false;

    // Start is called before the first frame update
    void Start()
    {
        soundcontroller.SettingBGMVolume(PlayerPrefs.GetFloat("BGM"));
        soundcontroller.SettingSFXVolume(PlayerPrefs.GetFloat("SE"));

        PlayerPrefs.SetFloat("Sensi", 1.0f);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            GuideArrow();
        }
        catch
        {

        }

        // ゲームパッドが接続されていないとnullになる。
        if (Gamepad.current == null) return;

        getPS4();

        if (openOption)
        {
            if (up == true)
            {
                if (optionNum == 0)
                {
                    up = false;
                }
                else
                {
                    optionNum--;
                    up = false;
                    PlaySelectSE();
                }
            }
            else if (down == true)
            {
                if (optionNum == 3)
                {
                    down = false;
                }
                else
                {
                    optionNum++;
                    down = false;
                    PlaySelectSE();
                }
            }

            if (right == true)
            {
                if (optionNum == 0)
                {
                    titleSlider.upBGM();
                }
                else if (optionNum == 1)
                {
                    titleSlider.upSE();
                }
                else if (optionNum == 2)
                {
                    GuideOffButton();
                    PlaySelectSE();
                }

                right = false;
            }
            else if (left == true)
            {
                if (optionNum == 0)
                {
                    titleSlider.downBGM();
                }
                else if (optionNum == 1)
                {
                    titleSlider.downSE();
                }
                else if (optionNum == 2)
                {
                    GuideOnButton();
                    PlaySelectSE();
                }

                left = false;
            }

            if (Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                if (optionNum == 3)
                {
                    openOption = false;
                    Time.timeScale = 1;
                    titleSlider.SetSensi(sensiSlider.value);

                    SceneManager.LoadScene("titleScene");
                }
            }
        }
        else
        {
            optionNum = 0;
        }


        // 見た目
        if(openOption)
        {
            if (optionNum == 0)
            {
                bgmText.color = new Color(r2, g2, b2, a2);
                seText.color = new Color(r1, g1, b1, a1);
                //sensiText.color = new Color(r1, g1, b1, a1);
                guideText.color = new Color(r1, g1, b1, a1);
                titleText.color = new Color(r1, g1, b1, a1);
            }
            else if (optionNum == 1)
            {
                bgmText.color = new Color(r1, g1, b1, a1);
                seText.color = new Color(r2, g2, b2, a2);
                //sensiText.color = new Color(r1, g1, b1, a1);
                guideText.color = new Color(r1, g1, b1, a1);
                titleText.color = new Color(r1, g1, b1, a1);
            }
            else if (optionNum == 2)
            {
                bgmText.color = new Color(r1, g1, b1, a1);
                seText.color = new Color(r1, g1, b1, a1);
                //sensiText.color = new Color(r2, g2, b2, a2);
                guideText.color = new Color(r2, g2, b2, a2);
                titleText.color = new Color(r1, g1, b1, a1);
            }
            else if (optionNum == 3)
            {
                bgmText.color = new Color(r1, g1, b1, a1);
                seText.color = new Color(r1, g1, b1, a1);
                //sensiText.color = new Color(r2, g2, b2, a2);
                guideText.color = new Color(r1, g1, b1, a1);
                titleText.color = new Color(r2, g2, b2, a2);
            }

            if (optionNum == 2)
            {
                if (PlayerPrefs.GetString("Guide") == "true")
                {
                    guideOnText.color = new Color(r2, g2, b2, a2);
                    guideOffText.color = new Color(r1, g1, b1, a1);
                }
                else if (PlayerPrefs.GetString("Guide") == "false")
                {
                    guideOnText.color = new Color(r1, g1, b1, a1);
                    guideOffText.color = new Color(r2, g2, b2, a2);
                }
            }
            else
            {
                guideOnText.color = new Color(r1, g1, b1, a1);
                guideOffText.color = new Color(r1, g1, b1, a1);
            }


            Image guideOnImage = guideOnButton.GetComponent<Image>();
            Image guideOffImage = guideOffButton.GetComponent<Image>();

            Color guideOnColor = guideOffImage.color;
            Color guideOffColor = guideOffImage.color;

            if (PlayerPrefs.GetString("Guide") == "true")
            {
                // aは透明度を表す
                guideOnColor.a = 1.0f;
                guideOffColor.a = 0.3f;
            }
            else if (PlayerPrefs.GetString("Guide") == "false")
            {
                // aは透明度を表す
                guideOnColor.a = 0.3f;
                guideOffColor.a = 1.0f;
            }

            guideOnImage.color = guideOnColor;
            guideOffImage.color = guideOffColor;
        }
    }

    public void OptionButton()
    {
        if (openOption)
        {
            optionPanel.SetActive(false);
            TitleButton.SetActive(false);
            openOption = false;
            Time.timeScale = 1;

            titleSlider.SetSensi(sensiSlider.value);
        }
        else
        {
            titleSlider.SetSensi(0);

            optionPanel.SetActive(true);
            TitleButton.SetActive(true);
            openOption = true;
            Time.timeScale = 0;         
        }

        musicManager.PlaySE1();
    }

    public void GuideOnButton()
    {
        PlayerPrefs.SetString("Guide", "true");
        PlayerPrefs.Save();
    }

    public void GuideOffButton()
    {
        PlayerPrefs.SetString("Guide", "false");
        PlayerPrefs.Save();
    }

    public void GuideArrow()
    {
        if (PlayerPrefs.GetString("Guide") == "true")
        {
            guideArrow.SetActive(true);
            offScreenGuideArrow.SetActive(true);
        }
        else if (PlayerPrefs.GetString("Guide") == "false")
        {
            guideArrow.SetActive(false);
            offScreenGuideArrow.SetActive(false);
        }
    }

    void getPS4()
    {
        if (Gamepad.current.startButton.wasPressedThisFrame)
        {
            OptionButton();
        }

        // スティックの入力を受け取る
        var v = Gamepad.current.leftStick.ReadValue();

        if (ps4Count >= buttonIntervalTime)
        {
            if (v.x >= 0.75)
            {
                right = true;
                ps4Count = 0;
            }
            else if (v.x <= -0.75)
            {
                left = true;
                ps4Count = 0;
            }
            else if (v.y >= 0.75)
            {
                up = true;
                ps4Count = 0;
            }
            else if (v.y <= -0.75)
            {
                down = true;
                ps4Count = 0;
            }
        }

        ps4Count++;
    }


    public void PlaySelectSE()
    {
        //ここに音が鳴る処理を書く
    }
}
